using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using System.Text.RegularExpressions;
using HoTroKhachHang.Application.fRepository.Functions;

namespace HoTroKhachHang.Application.MTaiLieu
{
    public class TaiLieuService : ITaiLieuService
    {
        private readonly CSKHContext _CSKHContext;
        ConvertDateTime cv = new ConvertDateTime();
        public TaiLieuService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string themMaTaiLieu()
        {
            var check = from TL in _CSKHContext.TaiLieus
                        select TL;
            if (check == null)
            {
                return "TL000001";
            }
            var query = (from MTL in _CSKHContext.TaiLieus
                        orderby MTL.MaTaiLieu descending
                        select MTL.MaTaiLieu).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(2, join_lastrecord.Length - 2));
            so += 1;
            string tempso = so.ToString();
            join_lastrecord = chu + tempso.PadLeft(6,'0');
            return join_lastrecord;
        }

        public async Task<ResponseOutput> addTaiLieu(TaiLieuRequest request)
        {
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var querycheck = from tl in _CSKHContext.TaiLieus
                             where tl.TenFile == request.TenFile
                             where tl.DuongDan == request.DuongDan
                             where !tl.MaTrangThai.Contains("TT006")
                             select tl;
            try
            {
                if (querycheck.Count() != 0)
                {
                    throw new InvalidOperationException("Tên file đã tồn tại");
                }
                var tl = new TaiLieu()
                {
                    MaTaiLieu = themMaTaiLieu(),
                    TenTaiLieu = request.TenTaiLieu,
                    MoTa = request.MoTa,
                    TenFile = request.TenFile,
                    DuongDan = request.DuongDan,
                    DownLoad = false,
                    MaLoaiTaiLieu = request.MaLoaiTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    MaTrangThai = "TT002",
                    MaSanPham = request.MaSanPham
                };
                var nv_tl = new NvTaiLieu()
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = themMaTaiLieu(),
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Thực hiện biên tập, soạn thảo tài liệu tham khảo",
                    Quyen = "Biên tập"
                };
                _CSKHContext.TaiLieus.Add(tl);
                _CSKHContext.NvTaiLieus.Add(nv_tl);
                foo.count_change = await _CSKHContext.SaveChangesAsync();
            }
            catch(Exception ex)
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

        //public async Task<ResponseOutput> getURL(string tenFile)
        //{
        //    ResponseOutput output = new ResponseOutput();
        //    dynamic foo = new ExpandoObject();
        //    var query = from TL in _CSKHContext.TaiLieus
        //                where TL.TenFile.Contains(tenFile)
        //                select TL.DuongDan;
        //    try
        //    {
        //        var temp = query.Select(x => x.ToString()).ToArray();
        //        string duongdan = string.Join("", temp);
        //        foo.url = duongdan;
        //    }
        //    catch(Exception ex)
        //    {
        //        output.message = ex.ToString();
        //        output.isSuccess = false;
        //        return output;
        //    }

        //    output.data = foo;
        //    output.message = "";
        //    output.isSuccess = true;
        //    return output;
        //}

        public async Task<List<TaiLieuTheoSanPham>> searchLoaiTaiLieuSanPham(string maSanPham, string maLoaiTaiLieu)
        {
            List<TaiLieuTheoSanPham> list = new List<TaiLieuTheoSanPham>();
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from TL in _CSKHContext.TaiLieus
                         join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                         join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                         join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                         where SP.MaSanPham.Contains(maSanPham) && LTL.MaLoaiTaiLieu.Contains(maLoaiTaiLieu)
                         where TT.TenTrangThai.Contains("Đã xuất bản")
                         select new { TL, LTL, TT, SP };
            try
            {
                var temp = await query.Select(x => new TaiLieuTheoSanPham()
                {
                    MaTaiLieu =x.TL.MaTaiLieu,
                    TenTaiLieu = x.TL.TenTaiLieu,
                    MoTa = x.TL.MoTa,
                    TenFile = x.TL.TenFile,
                    DuongDan = x.TL.DuongDan,
                    DownLoad = x.TL.DownLoad,
                    NgayThang = ((x.TL.NgayThang == null) ? DateTime.Now.ToString("dd-MM-yyyy") : ((DateTime)x.TL.NgayThang).ToString("dd-MM-yyyy")),
                    TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu,
                    TenSanPham = x.SP.TenSanPham
                }).ToListAsync();
                return temp;
            }
            catch(Exception ex)
            {
                return new List<TaiLieuTheoSanPham>();
            }      
        }

        public async Task<ResponseOutput> thongKeTheoTrangThai(string maTrangThai)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from TL in _CSKHContext.TaiLieus
                        join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                        where TT.MaTrangThai.Contains(maTrangThai)
                        select TL;
            try
            {
                int soluong = query.Count();
                foo.so_luong = soluong;
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

        public async Task<ResponseOutput> duyetTaiLieu(XuLyTaiLieu request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updatetailieu = await _CSKHContext.TaiLieus.FindAsync(request.MaTaiLieu);
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            try
            {
                var nv_tl = new NvTaiLieu
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = request.MaTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Tài liệu đã được duyệt và xuất bản thành công",
                    Quyen = "Duyệt"
                };
                _CSKHContext.NvTaiLieus.Add(nv_tl);

                updatetailieu.MaTrangThai = "TT005";
                updatetailieu.DownLoad = bool.Parse(request.Download);
                updatetailieu.NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                foo.count_change = await _CSKHContext.SaveChangesAsync();
                output.data = foo;
                output.isSuccess = true;
                output.message = "";
                return output;
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
        }

        public async Task<ResponseOutput> traVeTaiLieu(XuLyTaiLieu request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updatetailieu = await _CSKHContext.TaiLieus.FindAsync(request.MaTaiLieu);
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            try
            {
                var nv_tl = new NvTaiLieu
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = request.MaTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Tài liệu đã được trả về",
                    Quyen = "Trả về"
                };
                _CSKHContext.NvTaiLieus.Add(nv_tl);

                updatetailieu.MaTrangThai = "TT004";
                updatetailieu.LyDoTraVe = request.LyDoTraVe;
                updatetailieu.NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                foo.count_change = await _CSKHContext.SaveChangesAsync();
                output.data = foo;
                output.isSuccess = true;
                output.message = "";
                return output;
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
        }

        public async Task<ResponseOutput> guiDuyetTaiLieu(XuLyTaiLieu request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updatetailieu = await _CSKHContext.TaiLieus.FindAsync(request.MaTaiLieu);
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            try
            {
                var nv_tl = new NvTaiLieu
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = request.MaTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Tài liệu đã được gửi lại để duyệt",
                    Quyen = "Biên tập"
                };
                _CSKHContext.NvTaiLieus.Add(nv_tl);

                updatetailieu.TenTaiLieu = request.TenTaiLieu;
                updatetailieu.MoTa = request.Mota;
                updatetailieu.MaTrangThai = "TT002";
                updatetailieu.LyDoTraVe = null;
                updatetailieu.NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                foo.count_change = await _CSKHContext.SaveChangesAsync();
                output.data = foo;
                output.isSuccess = true;
                output.message = "";
                return output;
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
        }

        public async Task<List<TaiLieuTheoMaTaiLieu>> searchMaTaiLieuTaiLieu(string maTaiLieu)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            List<TaiLieuTheoMaTaiLieu> list = new List<TaiLieuTheoMaTaiLieu>();
            try
            {
                var query =   from TL in _CSKHContext.TaiLieus
                              join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                              join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                              join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                              where TL.MaTaiLieu.Contains(maTaiLieu)
                              where !TL.MaTrangThai.Contains("TT006")
                              select new { TL,LTL,SP,TT};
                var temp = await query.Select(x => new TaiLieuTheoMaTaiLieu()
                {
                    MaTaiLieu = x.TL.MaTaiLieu,
                    TenTaiLieu = x.TL.TenTaiLieu,
                    MoTa = x.TL.MoTa,
                    TenFile = x.TL.TenFile,
                    DuongDan = x.TL.DuongDan,
                    DownLoad = x.TL.DownLoad,
                    LyDoTraVe = x.TL.LyDoTraVe,
                    NgayThang = ((x.TL.NgayThang == null) ? DateTime.Now.ToString("dd-MM-yyyy") : ((DateTime)x.TL.NgayThang).ToString("dd-MM-yyyy")),
                    TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu,
                    TenSanPham = x.SP.TenSanPham,
                    TenTrangThai = x.TT.TenTrangThai
                }).ToListAsync();
                return temp;

            }
            catch(Exception ex)
            {
                return new List<TaiLieuTheoMaTaiLieu>();
            }
        }

        public async Task<List<SPham>> hienThiSanPham()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from SP in _CSKHContext.SanPhams
                        select new { SP };
            var temp = await query.Select(x => new SPham()
            {
                MaSanPham = x.SP.MaSanPham,
                TenSanPham = x.SP.TenSanPham
            }).ToListAsync();
            return temp;
        }

        public async Task<List<LTaiLieu>> hienThiLoaiTaiLieu()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from LTL in _CSKHContext.LoaiTaiLieus
                        select new { LTL };
            var temp = await query.Select(x => new LTaiLieu()
            {
                MaLoaiTaiLieu = x.LTL.MaLoaiTaiLieu,
                TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu
            }).ToListAsync();
            return temp;
        }

        //start test
        public async Task<List<Test_SanPhamTheoKhachHang>> testSanPhamTheoKhachHang(string maKhachHang)
        {
            var query = from KH in _CSKHContext.KhachHangs
                        join KL in _CSKHContext.KeyLicenses on KH.MaKhachHang equals KL.MaKhachHang
                        join SP in _CSKHContext.SanPhams on KL.MaSanPham equals SP.MaSanPham
                        where KH.MaKhachHang.Contains(maKhachHang)
                        select new { KH, KL, SP };
            var temp = await query.Select(x => new Test_SanPhamTheoKhachHang()
            {
                MaSanPham = x.SP.MaSanPham,
                TenSanPham = x.SP.TenSanPham,
                MoTa = x.SP.MoTa,
                Anh = x.SP.Anh
            }).ToListAsync();
            return temp;
        }
        //end test

        public async Task<ResponseOutput> huyTaiLieu(XuLyTaiLieu request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updatetailieu = await _CSKHContext.TaiLieus.FindAsync(request.MaTaiLieu);
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            try
            {
                var nv_tl = new NvTaiLieu
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = request.MaTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Tài liệu đã bị hủy",
                    Quyen = "Biên tập"
                };
                _CSKHContext.NvTaiLieus.Add(nv_tl);

                updatetailieu.MaTrangThai = "TT006";
                updatetailieu.NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                foo.count_change = await _CSKHContext.SaveChangesAsync();
                output.data = foo;
                output.isSuccess = true;
                output.message = "";
                return output;
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
        }

        public async Task<ResponseOutput> thuHoiTaiLieu(XuLyTaiLieu request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updatetailieu = await _CSKHContext.TaiLieus.FindAsync(request.MaTaiLieu);
            //string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            try
            {
                var nv_tl = new NvTaiLieu
                {
                    MaNhanVien = request.MaNhanVien,
                    MaTaiLieu = request.MaTaiLieu,
                    NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                    NoiDungThucHien = "Tài liệu đã bị thu hồi",
                    Quyen = "Duyệt"
                };
                _CSKHContext.NvTaiLieus.Add(nv_tl);

                updatetailieu.MaTrangThai = "TT007";
                updatetailieu.DownLoad = false;
                updatetailieu.NgayThang = DateTime.ParseExact(request.NgayThang, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                foo.count_change = await _CSKHContext.SaveChangesAsync();
                output.data = foo;
                output.isSuccess = true;
                output.message = "";
                return output;
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
        }

        public async Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen)
        {
            List<DanhSachSoLuongTheoTrangThai> ds = new List<DanhSachSoLuongTheoTrangThai>();
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var thongke = from TL in _CSKHContext.TaiLieus
                                      where (TL.MaTrangThai.Contains("TT002") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT005") || TL.MaTrangThai.Contains("TT007"))
                                      orderby TL.MaTrangThai
                                      group TL by TL.MaTrangThai into grp
                                      select new { key = grp.Key, soluong = grp.Count() };
                        foreach (var TT in thongke)
                        {
                            bool check = ds.Any(x => x.matrangthai == TT.key);
                            if (check == false)
                            {
                                ds.Add(new DanhSachSoLuongTheoTrangThai { matrangthai = TT.key, soluong = TT.soluong });
                            }
                        }
                    }
                    else if (items == "Duyệt")
                    {
                        var thongke = from TL in _CSKHContext.TaiLieus
                                      where (TL.MaTrangThai.Contains("TT002") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT007") || TL.MaTrangThai.Contains("TT005"))
                                      orderby TL.MaTrangThai
                                      group TL by TL.MaTrangThai into grp
                                      select new { key = grp.Key, soluong = grp.Count() };
                        foreach (var TT in thongke)
                        {
                            bool check = ds.Any(x => x.matrangthai == TT.key);
                            if (check == false)
                            {
                                ds.Add(new DanhSachSoLuongTheoTrangThai { matrangthai = TT.key, soluong = TT.soluong });
                            }
                        }
                    }
                    else if (items == "Xuất bản")
                    {
                        var thongke = from TL in _CSKHContext.TaiLieus
                                      where (TL.MaTrangThai.Contains("TT003") || TL.MaTrangThai.Contains("TT005") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT007"))
                                      orderby TL.MaTrangThai
                                      group TL by TL.MaTrangThai into grp
                                      select new { key = grp.Key, soluong = grp.Count() };
                        foreach (var TT in thongke)
                        {
                            bool check = ds.Any(x => x.matrangthai == TT.key);
                            if (check == false)
                            {
                                ds.Add(new DanhSachSoLuongTheoTrangThai { matrangthai = TT.key, soluong = TT.soluong });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<DanhSachSoLuongTheoTrangThai>();
            }
            return ds;
        }

        public async Task<List<TaiLieuResponse>> danhSachTaiLieuTheoQuyen(TenQuyenPaging tenQuyen)
        {
            List<TaiLieuResponse> list = new List<TaiLieuResponse>();
            List<TaiLieuResponse> list_res = new List<TaiLieuResponse>();
            RemoveSpace rm = new RemoveSpace();
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var query = from TL in _CSKHContext.TaiLieus
                                    join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                                    join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                                    join NVTL in _CSKHContext.NvTaiLieus on TL.MaTaiLieu equals NVTL.MaTaiLieu
                                    join NV in _CSKHContext.NhanViens on NVTL.MaNhanVien equals NV.MaNhanVien
                                    where (TL.MaTrangThai.Contains("TT002") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT005") || TL.MaTrangThai.Contains("TT007"))
                                    select new { TL, LTL, SP, TT, NV, NVTL };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaTaiLieu == item.TL.MaTaiLieu);
                            if (check == false)
                            {
                                list.Add(new TaiLieuResponse
                                {
                                    MaTaiLieu = item.TL.MaTaiLieu,
                                    TenTaiLieu = item.TL.TenTaiLieu,
                                    TenFile = item.TL.TenFile,
                                    MoTa = item.TL.MoTa,
                                    DuongDan = item.TL.DuongDan,
                                    DownLoad = item.TL.DownLoad,
                                    LyDoTraVe = item.TL.LyDoTraVe,
                                    NgayThang = item.TL.NgayThang.ToString(),
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) +" "+ rm.loaiBoKhoangTrang(item.NV.Ten),
                                    TenLoaiTaiLieu = item.LTL.TenLoaiTaiLieu,
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaBienTap=item.NV.MaNhanVien,
                                    MaSanPham=item.SP.MaSanPham
                                });
                            }
                        }
                    }
                    else if (items == "Duyệt")
                    {
                        var query = from TL in _CSKHContext.TaiLieus
                                    join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                                    join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                                    join NVTL in _CSKHContext.NvTaiLieus on TL.MaTaiLieu equals NVTL.MaTaiLieu
                                    join NV in _CSKHContext.NhanViens on NVTL.MaNhanVien equals NV.MaNhanVien
                                    where (TL.MaTrangThai.Contains("TT002") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT007") || TL.MaTrangThai.Contains("TT005"))
                                    select new { TL, LTL, SP, TT, NV, NVTL };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaTaiLieu == item.TL.MaTaiLieu);
                            if (check == false)
                            {
                                list.Add(new TaiLieuResponse
                                {
                                    MaTaiLieu = item.TL.MaTaiLieu,
                                    TenTaiLieu = item.TL.TenTaiLieu,
                                    TenFile = item.TL.TenFile,
                                    MoTa = item.TL.MoTa,
                                    DuongDan = item.TL.DuongDan,
                                    DownLoad = item.TL.DownLoad,
                                    LyDoTraVe = item.TL.LyDoTraVe,
                                    NgayThang = item.TL.NgayThang.ToString(),
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    TenLoaiTaiLieu = item.LTL.TenLoaiTaiLieu,
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaBienTap = item.NV.MaNhanVien,
                                    MaSanPham = item.SP.MaSanPham
                                });
                            }
                        }
                    }
                    else if (items == "Xuất bản")
                    {
                        var query = from TL in _CSKHContext.TaiLieus
                                    join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                                    join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                                    join NVTL in _CSKHContext.NvTaiLieus on TL.MaTaiLieu equals NVTL.MaTaiLieu
                                    join NV in _CSKHContext.NhanViens on NVTL.MaNhanVien equals NV.MaNhanVien
                                    where (TL.MaTrangThai.Contains("TT003") || TL.MaTrangThai.Contains("TT005") || TL.MaTrangThai.Contains("TT004") || TL.MaTrangThai.Contains("TT007"))
                                    select new { TL, LTL, SP, TT, NV, NVTL };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaTaiLieu == item.TL.MaTaiLieu);
                            if (check == false)
                            {
                                list.Add(new TaiLieuResponse
                                {
                                    MaTaiLieu = item.TL.MaTaiLieu,
                                    TenTaiLieu = item.TL.TenTaiLieu,
                                    TenFile = item.TL.TenFile,
                                    MoTa = item.TL.MoTa,
                                    DuongDan = item.TL.DuongDan,
                                    DownLoad = item.TL.DownLoad,
                                    LyDoTraVe = item.TL.LyDoTraVe,
                                    NgayThang = item.TL.NgayThang.ToString(),
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    TenLoaiTaiLieu = item.LTL.TenLoaiTaiLieu,
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaBienTap = item.NV.MaNhanVien,
                                    MaSanPham = item.SP.MaSanPham
                                });
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(tenQuyen.maBienTap))
                {
                    list = list.Where(S => S.MaBienTap == tenQuyen.maBienTap).ToList();
                }
                if (!string.IsNullOrEmpty(tenQuyen.maSanPham))
                {
                    list = list.Where(S => S.MaSanPham == tenQuyen.maSanPham).ToList();
                }
                list = list.OrderByDescending(x => DateTime.Parse(x.NgayThang)).ToList();
                foreach (var item in list)
                {
                    item.NgayThang = ((item.NgayThang == null) ? DateTime.Now.ToString("dd-MM-yyyy") : (DateTime.Parse(item.NgayThang).ToString("dd-MM-yyyy")));
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayThang.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd HH:mm:ss.fff"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaTaiLieu).ToList();
            }
            catch (Exception ex)
            {
                return new List<TaiLieuResponse>();
            }
            return list;
        }

        public async Task<List<TaiLieuResponse>> getTaiLieuTrangThai(string maTrangThai, string maBienTap , string maSanPham)
        {
            List<TaiLieuResponse> list = new List<TaiLieuResponse>();
            List<TaiLieuResponse> list_res = new List<TaiLieuResponse>();
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            RemoveSpace rm = new RemoveSpace();
            var query = from TL in _CSKHContext.TaiLieus
                        join LTL in _CSKHContext.LoaiTaiLieus on TL.MaLoaiTaiLieu equals LTL.MaLoaiTaiLieu
                        join SP in _CSKHContext.SanPhams on TL.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on TL.MaTrangThai equals TT.MaTrangThai
                        join NVTL in _CSKHContext.NvTaiLieus on TL.MaTaiLieu equals NVTL.MaTaiLieu
                        join NV in _CSKHContext.NhanViens on NVTL.MaNhanVien equals NV.MaNhanVien
                        where TT.MaTrangThai == maTrangThai
                        select new { TL, LTL, SP, TT, NV, NVTL };
            try
            {
                foreach (var item in query)
                {
                    bool check = list.Exists(x => x.MaTaiLieu == item.TL.MaTaiLieu);
                    if (check == false)
                    {
                        list.Add(new TaiLieuResponse
                        {
                            MaTaiLieu = item.TL.MaTaiLieu,
                            TenTaiLieu = item.TL.TenTaiLieu,
                            TenFile = item.TL.TenFile,
                            MoTa = item.TL.MoTa,
                            DuongDan = item.TL.DuongDan,
                            DownLoad = item.TL.DownLoad,
                            LyDoTraVe = item.TL.LyDoTraVe,
                            NgayThang = item.TL.NgayThang.ToString(),
                            TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                            TenLoaiTaiLieu = item.LTL.TenLoaiTaiLieu,
                            TenSanPham = item.SP.TenSanPham,
                            TenTrangThai = item.TT.TenTrangThai,
                            MaBienTap = item.NV.MaNhanVien,
                            MaSanPham=item.SP.MaSanPham
                        }); ; ;
                    }
                }
                if (!string.IsNullOrEmpty(maBienTap))
                {
                    list = list.Where(S => S.MaBienTap == maBienTap).ToList();
                }
                if (!string.IsNullOrEmpty(maSanPham))
                {
                    list = list.Where(S => S.MaSanPham == maSanPham).ToList();
                }
                list = list.OrderByDescending(x => DateTime.Parse(x.NgayThang)).ToList();
                foreach (var item in list)
                {
                    item.NgayThang = ((item.NgayThang == null) ? DateTime.Now.ToString("dd-MM-yyyy") : (DateTime.Parse(item.NgayThang).ToString("dd-MM-yyyy")));
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayThang.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd HH:mm:ss.fff"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaTaiLieu).ToList();
            }
            catch (Exception ex)
            {   
                return new List<TaiLieuResponse>();
            }
            return list;
        }

        public async Task<List<linkDownload>> listLinkDownload(string maSanPham)
        {
            var query = from TL in _CSKHContext.TaiLieus
                        where TL.MaSanPham.Contains(maSanPham)
                        where TL.MaLoaiTaiLieu.Contains("LTL003")
                        where TL.MaTrangThai.Contains("TT005")
                        orderby TL.NgayThang descending
                        select new { TL };
            if (query == null)
            {
                throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
            }
            var temp = await query.Select(x => new linkDownload()
            {
                MaTaiLieu = x.TL.MaTaiLieu,
                TenTaiLieu = x.TL.TenTaiLieu,
                TenFile = x.TL.TenFile,
                NgayThang = ((x.TL.NgayThang == null) ? DateTime.Now.ToString("dd-MM-yyyy") : ((DateTime)x.TL.NgayThang).ToString("dd-MM-yyyy")),
                Download = x.TL.DownLoad,
                DuongDan = x.TL.DuongDan,
                MoTa = x.TL.MoTa
            }).ToListAsync();
            return temp;
        }
        public async Task<ResponseOutput> deleteTaiLieu(string maTaiLieu)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_nvtailieu = (from NVTL in _CSKHContext.NvTaiLieus
                                      where NVTL.MaTaiLieu == maTaiLieu
                                      select NVTL).ToList();
                if (check_nvtailieu.Count() != 0)
                {
                    foreach (var items in check_nvtailieu)
                    {
                        _CSKHContext.NvTaiLieus.Remove(items);
                    }
                }
                var quangcao = await _CSKHContext.TaiLieus.FindAsync(maTaiLieu);
                _CSKHContext.TaiLieus.Remove(quangcao);
                foo.change = await _CSKHContext.SaveChangesAsync();

                output.isSuccess = true;
                output.message = "Xóa thành công, các thông tin có liên quan đến câu hỏi này đều bị xóa";
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

    }
}
