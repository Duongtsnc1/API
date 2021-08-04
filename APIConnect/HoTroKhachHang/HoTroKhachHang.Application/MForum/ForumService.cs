using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.ResponsesForum;
using HoTroKhachHang.Application.MForum;
using HoTroKhachHang.Data.Entities;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Application.fRepository.Functions;
using System.Globalization;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using HoTroKhachHang.Application.fRepository.ModelsForum;

namespace HoTroKhachHang.Application.MForum
{
    public class ForumService : IForumService
    {
        private readonly CSKHContext _CSKHContext;
        public ForumService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string convertStringDateTime(string dt)
        {
            //------12/1/2020 12:00:00 AM - 6/12/2020 12:00:00 PM - 9/2/2021 12:00:00 AM
            string res = "";
            if (dt.Substring(1, 1) == "/" && dt.Length == 20)
            {
                dt = dt.PadLeft(21, '0');
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(2, 1) == "/" && dt.Length == 21)
            {
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(1, 1) == "/" && dt.Length == 21)
            {
                res = dt.PadLeft(22, '0');
            }
            else
            {
                res = dt;
            }
            return res;
        }
        private string checkInvailDateTime(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(convertStringDateTime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            }
            else return "";
        }
        private string themMaBaiDang()
        {
            var check = from HD in _CSKHContext.BaiDangs
                        select HD;
            if (check.ToList().Count == 0)
            {
                return "BD0_00000001";
                //HD00000001
            }

            var query = (from MHD in _CSKHContext.BaiDangs
                         orderby MHD.MaBaiDang descending
                         select MHD.MaBaiDang).First();

            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2, 1));
            if (tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }

            string tempmiddle = middle.ToString();

            join_lastrecord = chu + tempmiddle + "_" + tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        private string themMaChuDe()
        {
            var check = from HD in _CSKHContext.ChuDes
                        select HD;
            if (check.ToList().Count == 0)
            {
                return "CD0_00000001";
                //HD00000001
            }

            var query = (from MHD in _CSKHContext.ChuDes
                         orderby MHD.MaChuDe descending
                         select MHD.MaChuDe).First();

            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2, 1));
            if (tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }

            string tempmiddle = middle.ToString();

            join_lastrecord = chu + tempmiddle + "_" + tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        private string themMaNhomChuDe()
        {
            var check = from HD in _CSKHContext.NhomChuDes
                        select HD;
            if (check.ToList().Count == 0)
            {
                return "ND0_00000001";
                //HD00000001
            }

            var query = (from MHD in _CSKHContext.NhomChuDes
                         orderby MHD.MaNhomChuDe descending
                         select MHD.MaNhomChuDe).First();

            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2, 1));
            if (tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }

            string tempmiddle = middle.ToString();

            join_lastrecord = chu + tempmiddle + "_" + tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> addBinhLuan(ForumBinhLuanMoi request)
        {
            string dt_ngaytao = (DateTime.ParseExact(request.NgayTao, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var bl = new BinhLuan()
                {
                    NoiDung = request.NoiDung,
                    NgayTao = DateTime.ParseExact(dt_ngaytao, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    MaBaiDang = request.MaBaiDang,
                    MaKhachHang = request.MaKhachHang
                };
                var check_baidang = await _CSKHContext.BaiDangs.FindAsync(request.MaBaiDang);
                check_baidang.LanBinhLuanCuoi = DateTime.ParseExact(dt_ngaytao, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                check_baidang.MaNguoiBinhLuanCuoi = request.MaKhachHang;
                check_baidang.SoLuongBinhLuan += 1;
                _CSKHContext.BinhLuans.Add(bl);
                foo.change = await _CSKHContext.SaveChangesAsync();

                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<ResponseOutput> deleteBaiDang(string maBaiDang)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_baidang = await _CSKHContext.BaiDangs.FindAsync(maBaiDang);
                if (check_baidang == null)
                {
                    output.isSuccess = false;
                    output.message = "Dữ liệu không tồn tại";
                    return output;
                }
                var query = (from BL in _CSKHContext.BinhLuans
                            where BL.MaBaiDang.Contains(maBaiDang)
                            select BL.Id).ToList();
                foreach(var items in query)
                {
                    var delete = await _CSKHContext.BinhLuans.FindAsync(items);
                    _CSKHContext.BinhLuans.Remove(delete);
                }
                _CSKHContext.BaiDangs.Remove(check_baidang);
                foo.change = await _CSKHContext.SaveChangesAsync();
                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<ResponseOutput> deleteBinhLuan(int id)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var deleteBinhLuan = await _CSKHContext.BinhLuans.FindAsync(id);
                if (deleteBinhLuan == null)
                {
                    output.isSuccess = false;
                    output.message = "Không có dữ liệu muốn xóa";
                    return output;
                }
                _CSKHContext.BinhLuans.Remove(deleteBinhLuan);
                var baiDang = await _CSKHContext.BaiDangs.FindAsync(deleteBinhLuan.MaBaiDang);
                if (baiDang == null)
                {
                    output.isSuccess = false;
                    output.message = "Không có dữ liệu muốn xóa";
                    return output;
                }
                baiDang.SoLuongBinhLuan -= 1;
                if (baiDang.SoLuongBinhLuan == 0)
                {
                    baiDang.MaNguoiBinhLuanCuoi = null;
                    baiDang.LanBinhLuanCuoi = null;
                }
                foo.change = await _CSKHContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.message = "";
            output.data = foo;
            return output;
        }

        public async Task<ResponseOutput> deleteChuDe(ForumDeleteChuDe request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var trangthai_chude = await _CSKHContext.ChuDes.FindAsync(request.MaChuDe);
                if(trangthai_chude == null)
                {
                    output.isSuccess = false;
                    output.message = "Dữ liệu không tồn tại";
                    return output;
                }
                trangthai_chude.TrangThai = request.TrangThai;
                foo.change = await _CSKHContext.SaveChangesAsync();
                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<List<ForumBaiDang>> getAllBaiDang()
        {
            List<ForumBaiDang> list = new List<ForumBaiDang>();
            RemoveSpace rm = new RemoveSpace();
            var query = from BD in _CSKHContext.BaiDangs
                         join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                         join KH2 in _CSKHContext.KhachHangs on BD.MaNguoiBinhLuanCuoi equals KH2.MaKhachHang
                         join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                         orderby BD.MaBaiDang descending
                         select new { BD, KH1, KH2, CD };
            if (query == null)
            {
                return new List<ForumBaiDang>();
            }
            var temp = await query.Select(x => new ForumBaiDang()
            {
                MaBaiDang = x.BD.MaBaiDang,
                TieuDe = x.BD.TieuDe,
                NoiDung = x.BD.NoiDung,
                NgayDang = x.BD.NgayDang.ToString(),
                LanBinhLuanCuoi = x.BD.LanBinhLuanCuoi.ToString(),
                MaNguoiBinhLuanCuoi = x.BD.MaNguoiBinhLuanCuoi,
                MaChuDe = x.BD.MaChuDe,
                SoLuongBinhLuan = x.BD.SoLuongBinhLuan,
                Hot = x.BD.Hot,
                MaKhachHang = x.BD.MaKhachHang,
                SoLuongXem = x.BD.SoLuongXem,
                TenChuDe = x.CD.TenChuDe,
                TenKhachHang = rm.loaiBoKhoangTrang(x.KH1.Ho) + " " + rm.loaiBoKhoangTrang(x.KH1.Ten),
                TenNguoiBinhLuanCuoi = rm.loaiBoKhoangTrang(x.KH2.Ho) + " " + rm.loaiBoKhoangTrang(x.KH2.Ten)
            }).ToListAsync();
            var nullbinhluancuoi = from BD in _CSKHContext.BaiDangs
                                   join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                                   join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                                   where BD.MaNguoiBinhLuanCuoi==null
                                   orderby BD.MaBaiDang descending
                                   select new { BD, KH1, CD };
            foreach(var item in nullbinhluancuoi)
            {
                temp.Add(new ForumBaiDang()
                {
                    MaBaiDang = item.BD.MaBaiDang,
                    TieuDe = item.BD.TieuDe,
                    NoiDung = item.BD.NoiDung,
                    NgayDang =checkInvailDateTime(  item.BD.NgayDang.ToString()),                    
                    MaChuDe = item.BD.MaChuDe,
                    SoLuongBinhLuan = item.BD.SoLuongBinhLuan,
                    Hot = item.BD.Hot,
                    MaKhachHang = item.BD.MaKhachHang,
                    SoLuongXem = item.BD.SoLuongXem,
                    TenChuDe = item.CD.TenChuDe,
                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH1.Ho) + " " + rm.loaiBoKhoangTrang(item.KH1.Ten)
                });
            }            
            return temp.OrderByDescending(s=>s.MaBaiDang).ToList();      
        }

        public async Task<List<ForumBaiDang>> getBaiDangKhachHang(string maKhachHang)
        {
            List<ForumBaiDang> list = new List<ForumBaiDang>();
            RemoveSpace rm = new RemoveSpace();
            var query = from BD in _CSKHContext.BaiDangs
                        join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                        join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                        where KH1.MaKhachHang.Contains(maKhachHang)
                        orderby BD.MaBaiDang descending
                        select new { BD, KH1, CD };
            if(query == null)
            {
                return new List<ForumBaiDang>();
            }
            foreach (var item in query)
            {
                var baidang = new ForumBaiDang()
                {
                    MaBaiDang = item.BD.MaBaiDang,
                    TieuDe = item.BD.TieuDe,
                    NoiDung = item.BD.NoiDung,
                    NgayDang = checkInvailDateTime(item.BD.NgayDang.ToString()),
                    LanBinhLuanCuoi = checkInvailDateTime(item.BD.LanBinhLuanCuoi.ToString()),
                    MaNguoiBinhLuanCuoi = item.BD.MaNguoiBinhLuanCuoi,
                    MaChuDe = item.BD.MaChuDe,
                    SoLuongBinhLuan = item.BD.SoLuongBinhLuan,
                    Hot = item.BD.Hot,
                    MaKhachHang = item.BD.MaKhachHang,
                    SoLuongXem = item.BD.SoLuongXem,
                    TenChuDe = item.CD.TenChuDe,
                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH1.Ho) + " " + rm.loaiBoKhoangTrang(item.KH1.Ten)
                };
                if (!string.IsNullOrEmpty(item.BD.MaNguoiBinhLuanCuoi))
                {
                    var KH = await _CSKHContext.KhachHangs.FirstOrDefaultAsync(s => s.MaKhachHang == item.BD.MaNguoiBinhLuanCuoi);
                    baidang.TenNguoiBinhLuanCuoi = rm.loaiBoKhoangTrang(KH.Ho) + " " + rm.loaiBoKhoangTrang(KH.Ten);
                }
                list.Add(baidang);
            }
            return list;
        }

        public async Task<List<ForumBaiDang>> getBaiDangTieuDe(string tieuDe)
        {
            List<ForumBaiDang> list = new List<ForumBaiDang>();
            RemoveSpace rm = new RemoveSpace();
            var query = from BD in _CSKHContext.BaiDangs
                        join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                        join KH2 in _CSKHContext.KhachHangs on BD.MaNguoiBinhLuanCuoi equals KH2.MaKhachHang
                        join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                        where BD.TieuDe.Contains(tieuDe)
                        orderby BD.MaBaiDang descending
                        select new { BD, KH1, KH2, CD };
            if (query == null)
            {
                return new List<ForumBaiDang>();
            }
            var temp = await query.Select(x => new ForumBaiDang()
            {
                MaBaiDang = x.BD.MaBaiDang,
                TieuDe = x.BD.TieuDe,
                NoiDung = x.BD.NoiDung,
                NgayDang = x.BD.NgayDang.ToString(),
                LanBinhLuanCuoi = x.BD.LanBinhLuanCuoi.ToString(),
                MaNguoiBinhLuanCuoi = x.BD.MaNguoiBinhLuanCuoi,
                MaChuDe = x.BD.MaChuDe,
                SoLuongBinhLuan = x.BD.SoLuongBinhLuan,
                Hot = x.BD.Hot,
                MaKhachHang = x.BD.MaKhachHang,
                SoLuongXem = x.BD.SoLuongXem,
                TenChuDe = x.CD.TenChuDe,
                TenKhachHang = rm.loaiBoKhoangTrang(x.KH1.Ho) + " " + rm.loaiBoKhoangTrang(x.KH1.Ten),
                TenNguoiBinhLuanCuoi = rm.loaiBoKhoangTrang(x.KH2.Ho) + " " + rm.loaiBoKhoangTrang(x.KH2.Ten)
            }).ToListAsync();
            return temp;
        }

        public async Task<List<ForumResponse>> getChuDe()
        {
            var query = from CD in _CSKHContext.ChuDes
                        where CD.TrangThai == true
                        select new { CD };
            var temp = await query.Select(x => new ForumResponse()
            {
                MaChuDe = x.CD.MaChuDe,
                TenChuDe = x.CD.TenChuDe
            }).ToListAsync();
            return temp;
        }

        public async Task<List<ForumBaiDangComment>> getCommentBaiDang(string maBaiDang)
        {
            var rm = new RemoveSpace();
            var query = from BL in _CSKHContext.BinhLuans
                        join KH in _CSKHContext.KhachHangs on BL.MaKhachHang equals KH.MaKhachHang
                        where BL.MaBaiDang.Contains(maBaiDang)
                        orderby BL.Id descending
                        select new { KH, BL };
            var temp = await query.Select(x => new ForumBaiDangComment()
            {
                Id = x.BL.Id,
                NoiDung = x.BL.NoiDung,
                NgayTao = x.BL.NgayTao.ToString(),
                TenKhachHang = rm.loaiBoKhoangTrang(x.KH.Ho)+" "+rm.loaiBoKhoangTrang(x.KH.Ten),
                MaKhachHang=x.BL.MaKhachHang,
                MaBaiDang = x.BL.MaBaiDang
            }).ToListAsync();
            return temp;
        }

        public async Task<List<ForumNhomChuDe>> getNhomChuDe()
        {
            List<ForumNhomChuDe> list = new List<ForumNhomChuDe>();
            var query = (from NCD in _CSKHContext.NhomChuDes
                        select NCD.MaNhomChuDe).ToList();
            foreach(var items in query)
            {
                ForumNhomChuDe ds = (from NCD in _CSKHContext.NhomChuDes
                                     where NCD.MaNhomChuDe.Contains(items)
                                     select new ForumNhomChuDe
                                     {
                                         MaNhomChuDe = NCD.MaNhomChuDe,
                                         TenNhomChuDe = NCD.TenNhomChuDe,
                                         MoTa = NCD.MoTa,
                                         NgayTao = NCD.NgayTao.ToString()
                                     }).FirstOrDefault();
                ds.ChuDe = await (from CD in _CSKHContext.ChuDes
                           where CD.MaNhomChuDe.Contains(items)
                           where CD.TrangThai == true
                           select new ForumChuDe 
                           {
                               MaChuDe = CD.MaChuDe,
                               NgayTao = (DateTime.ParseExact(CD.NgayTao.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                               MoTa = CD.MoTa,
                               TenChuDe = CD.TenChuDe,
                               MaNhomChuDe = CD.MaNhomChuDe,
                               TrangThai = CD.TrangThai
                           }).ToListAsync();
                list.Add(ds);
            }
            return list;
        }

        public async Task<List<ForumNhomChuDe>> getNhomChuDeA()
        {
            List<ForumNhomChuDe> list = new List<ForumNhomChuDe>();
            var query = (from NCD in _CSKHContext.NhomChuDes
                         select NCD.MaNhomChuDe).ToList();
            foreach (var items in query)
            {
                ForumNhomChuDe ds = (from NCD in _CSKHContext.NhomChuDes
                                     where NCD.MaNhomChuDe.Contains(items)
                                     select new ForumNhomChuDe
                                     {
                                         MaNhomChuDe = NCD.MaNhomChuDe,
                                         TenNhomChuDe = NCD.TenNhomChuDe,
                                         MoTa = NCD.MoTa,
                                         NgayTao = NCD.NgayTao.ToString()
                                     }).FirstOrDefault();
                ds.ChuDe = await(from CD in _CSKHContext.ChuDes
                                 where CD.MaNhomChuDe.Contains(items)
                                 select new ForumChuDe
                                 {
                                     MaChuDe = CD.MaChuDe,
                                     NgayTao = (DateTime.ParseExact(CD.NgayTao.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                                     MoTa = CD.MoTa,
                                     TenChuDe = CD.TenChuDe,
                                     MaNhomChuDe = CD.MaNhomChuDe,
                                     TrangThai = CD.TrangThai
                                 }).ToListAsync();
                list.Add(ds);
            }
            return list;
        }

        public async Task<ForumTongQuan> getTongQuan()
        {
            ForumTongQuan tongquan = new ForumTongQuan();
            tongquan.BaiDang = (from BD in _CSKHContext.BaiDangs
                                select BD).Count();
            tongquan.BinhLuan = (from BL in _CSKHContext.BinhLuans
                                   select BL).Count();
            return tongquan;
        }

        public async Task<ResponseOutput> updateHotTrend(ForumHotTrend request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_hottrend = await _CSKHContext.BaiDangs.FindAsync(request.MaBaiDang);
                if (check_hottrend == null)
                {
                    output.isSuccess = false;
                    output.message = "Dữ liệu không tồn tại";
                    return output;
                }
                check_hottrend.Hot = request.Hot;
                foo.change = await _CSKHContext.SaveChangesAsync();
                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<ResponseOutput> updateSoLuong(string maBaiDang)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_soluong = await _CSKHContext.BaiDangs.FindAsync(maBaiDang);
                if (check_soluong == null)
                {
                    output.isSuccess = false;
                    output.message = "Dữ liệu không tồn tại";
                    return output;
                }
                check_soluong.SoLuongXem += 1;
                foo.change = await _CSKHContext.SaveChangesAsync();
                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<ResponseOutput> addBaiDang(ForumBaiDangRequest request)
        {
            string dt_ngaynhan = (DateTime.ParseExact(request.ngayDang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var bd = new BaiDang()
                {
                    MaBaiDang = themMaBaiDang(),
                    TieuDe = request.tieuDe,
                    NoiDung = request.noiDung,
                    MaKhachHang = request.maKhachHang,
                    MaChuDe = request.maChuDe,
                    SoLuongBinhLuan = 0,
                    MaNguoiBinhLuanCuoi = null,
                    SoLuongXem = 0,
                    Hot = false,
                    LanBinhLuanCuoi = null,
                    NgayDang = DateTime.ParseExact(dt_ngaynhan, "yyyy-MM-dd", CultureInfo.InvariantCulture)

                };
                _CSKHContext.BaiDangs.Add(bd);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            output.message = "";
            output.isSuccess = true;
            output.data = foo;
            return output;
        }

        public async Task<ResponseOutput> addChuDe(ForumChuDeRequest request)
        {
            string dt_ngaynhan = (DateTime.ParseExact(request.ngayTao, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var cd = new ChuDe()
                {
                    MaChuDe = themMaChuDe(),
                    MoTa = request.moTa,
                    TenChuDe = request.tenChuDe,
                    NgayTao = DateTime.ParseExact(dt_ngaynhan, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    MaNhomChuDe=request.maNhomChuDe,
                    TrangThai = true
                };
                _CSKHContext.ChuDes.Add(cd);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            output.message = "";
            output.isSuccess = true;
            output.data = foo;
            return output;
        }

        public async Task<ResponseOutput> addNhomChuDe(ForumNhomChuDeRequest request)
        {
            string dt_ngaynhan = (DateTime.ParseExact(request.ngayTao, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var ncd = new NhomChuDe()
                {
                    MaNhomChuDe = themMaNhomChuDe(),
                    MoTa = request.moTa,
                    TenNhomChuDe = request.tenNhomChuDe,
                    NgayTao = DateTime.ParseExact(dt_ngaynhan, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };
                _CSKHContext.NhomChuDes.Add(ncd);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            output.message = "";
            output.isSuccess = true;
            output.data = foo;
            return output;
        }

        public async Task<List<ForumBaiDang>> getBaiDangMaBaiDang(string maBaiDang)
        {
            List<ForumBaiDang> list = new List<ForumBaiDang>();
            RemoveSpace rm = new RemoveSpace();
            var query = from BD in _CSKHContext.BaiDangs
                        join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                      
                        join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                        where BD.MaBaiDang == maBaiDang
                        orderby BD.MaBaiDang descending
                        select new { BD, KH1, CD };
            if (query == null)
            {
                return new List<ForumBaiDang>();
            }
            foreach(var item in query)
            {
                var baidang = new ForumBaiDang()
                {
                    MaBaiDang = item.BD.MaBaiDang,
                    TieuDe = item.BD.TieuDe,
                    NoiDung = item.BD.NoiDung,
                    NgayDang =checkInvailDateTime( item.BD.NgayDang.ToString()),
                    LanBinhLuanCuoi = checkInvailDateTime( item.BD.LanBinhLuanCuoi.ToString()),
                    MaNguoiBinhLuanCuoi = item.BD.MaNguoiBinhLuanCuoi,
                    MaChuDe = item.BD.MaChuDe,
                    SoLuongBinhLuan = item.BD.SoLuongBinhLuan,
                    Hot = item.BD.Hot,
                    MaKhachHang = item.BD.MaKhachHang,
                    SoLuongXem = item.BD.SoLuongXem,
                    TenChuDe = item.CD.TenChuDe,
                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH1.Ho) + " " + rm.loaiBoKhoangTrang(item.KH1.Ten)
                };
                if (!string.IsNullOrEmpty(item.BD.MaNguoiBinhLuanCuoi))
                {
                    var KH = await _CSKHContext.KhachHangs.FirstOrDefaultAsync(s => s.MaKhachHang == item.BD.MaNguoiBinhLuanCuoi);
                    baidang.TenNguoiBinhLuanCuoi = rm.loaiBoKhoangTrang(KH.Ho) + " " + rm.loaiBoKhoangTrang(KH.Ten);
                }
                list.Add(baidang);
            }
            return list;
        }

        public async Task<List<ForumBaiDang>> getBaiDangMaChuDe(string maChuDe)
        {
            List<ForumBaiDang> list = new List<ForumBaiDang>();
            RemoveSpace rm = new RemoveSpace();
            var query = from BD in _CSKHContext.BaiDangs
                        join KH1 in _CSKHContext.KhachHangs on BD.MaKhachHang equals KH1.MaKhachHang
                        join KH2 in _CSKHContext.KhachHangs on BD.MaNguoiBinhLuanCuoi equals KH2.MaKhachHang
                        join CD in _CSKHContext.ChuDes on BD.MaChuDe equals CD.MaChuDe
                        where BD.MaChuDe == maChuDe
                        orderby BD.MaBaiDang descending
                        select new { BD, KH1, KH2, CD };
            if (query == null)
            {
                return new List<ForumBaiDang>();
            }
            var temp = await query.Select(x => new ForumBaiDang()
            {
                MaBaiDang = x.BD.MaBaiDang,
                TieuDe = x.BD.TieuDe,
                NoiDung = x.BD.NoiDung,
                NgayDang = x.BD.NgayDang.ToString(),
                LanBinhLuanCuoi = x.BD.LanBinhLuanCuoi.ToString(),
                MaNguoiBinhLuanCuoi = x.BD.MaNguoiBinhLuanCuoi,
                MaChuDe = x.BD.MaChuDe,
                SoLuongBinhLuan = x.BD.SoLuongBinhLuan,
                Hot = x.BD.Hot,
                MaKhachHang = x.BD.MaKhachHang,
                SoLuongXem = x.BD.SoLuongXem,
                TenChuDe = x.CD.TenChuDe,
                TenKhachHang = rm.loaiBoKhoangTrang(x.KH1.Ho) + " " + rm.loaiBoKhoangTrang(x.KH1.Ten),
                TenNguoiBinhLuanCuoi = rm.loaiBoKhoangTrang(x.KH2.Ho) + " " + rm.loaiBoKhoangTrang(x.KH2.Ten)
            }).ToListAsync();
            return temp;
        }
        public async Task<ResponseOutput> updateChuDe(ForumChuDe request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try {
                var check = await _CSKHContext.ChuDes.FirstOrDefaultAsync(s => s.MaChuDe == request.MaChuDe);
                if (check==null)
                {
                    output.isSuccess = false;
                    output.message = "Dữ liệu không tồn tại";
                    return output;
                }
                var update = await _CSKHContext.ChuDes.FirstOrDefaultAsync(s => s.MaChuDe == request.MaChuDe);
                update.TenChuDe = request.TenChuDe;
                update.MoTa = request.MoTa;
                foo.change = await _CSKHContext.SaveChangesAsync();
                output.isSuccess = true;
                output.message = "";
                output.data = foo;
                return output;
            }
            catch(Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            
            output.data = foo;
            return output;
        }

    }
}
