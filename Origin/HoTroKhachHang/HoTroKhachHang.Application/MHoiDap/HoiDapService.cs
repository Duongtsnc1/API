using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Data.Entities;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using System.Text.RegularExpressions;
using HoTroKhachHang.Email;
using HoTroKhachHang.Application.fRepository.Functions;

namespace HoTroKhachHang.Application.MHoiDap
{
    public class HoiDapService : IHoiDapService
    {
        private readonly CSKHContext _CSKHContext;
        private readonly IEmailSender _EmailSender;
        private readonly HostingConfiguration _hosting;
        public HoiDapService(CSKHContext CSKHContext, IEmailSender EmailSender,HostingConfiguration hosting)
        {
            _CSKHContext = CSKHContext;
            _EmailSender = EmailSender;
            _hosting = hosting;
        }
        private string fullDatetime(string dt)
        {
            //dd/MM/yyyy HH:mm:ss tt

            while (dt.Length != 22)
            {
                if (dt[2] != '/')
                {
                    dt = dt.Insert(0, "0");
                }
                else if (dt[5] != '/')
                {
                    dt = dt.Insert(3, "0");
                }
                else if (dt[13] != ':')
                {
                    dt = dt.Insert(11, "0");
                }
                else if (dt[16] != ':')
                {
                    dt = dt.Insert(14, "0");
                }
                else if (dt[19] != ' ')
                {
                    dt = dt.Insert(17, "0");
                }
            }
            return dt;
        }
        private string getDatetime(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(fullDatetime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt");
            }
            else return null;
        }
        private string themMaCauHoi()
        {
            var check = from HD in _CSKHContext.HoiDaps
                        select HD;
            if(check == null)
            {
                return "HD0_00000001";
                //HD00000001
            }
            var query = (from MHD in _CSKHContext.HoiDaps
                         orderby MHD.MaHoiDap descending
                         select MHD.MaHoiDap).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2,1));
            if(tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }
            string tempmiddle = middle.ToString();
            join_lastrecord = chu +tempmiddle+"_"+tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> addCauHoi(HoiDapRequest request)
        {
            //string dt_ngaynhan = (DateTime.ParseExact(request.NgayNhan, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var hd = new HoiDap()
                {
                    MaHoiDap = themMaCauHoi(),
                    TieuDe = request.TieuDe,
                    NdHoiDap = request.NdHoiDap,
                    MaKhachHang = request.MaKhachHang,
                    MaLoai = request.MaLoai,
                    MaTrangThai = "TT001",
                    MaSanPham = request.MaSanPham,
                    NgayNhan = DateTime.Now,
                    CongKhai = true
                };
                _CSKHContext.HoiDaps.Add(hd);
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

        public async Task<List<TimKiemHoiDap>> getHoiDapKhachHang(string HoTen)
        {
            RemoveSpace rm = new RemoveSpace();
            var query = from KH in _CSKHContext.KhachHangs
                         join HD in _CSKHContext.HoiDaps on KH.MaKhachHang equals HD.MaKhachHang
                         join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                         join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                         where KH.Ten.Contains(HoTen) || KH.Ho.Contains(HoTen)
                         select new {KH,HD,TT,SP};
            var temp = await query.Select(x => new TimKiemHoiDap()
            {
                Ho = rm.loaiBoKhoangTrang(x.KH.Ho),
                Ten = rm.loaiBoKhoangTrang(x.KH.Ten),
                NdHoiDap = x.HD.NdHoiDap,
                NdHoiDapEdit = x.HD.NdHoiDapEdit,
                TenTrangThai = x.TT.TenTrangThai,
                CongKhai = x.HD.CongKhai,
                TenSanPham = x.SP.TenSanPham,
                NdTraLoi = x.HD.NdTraLoi,
                NgayNhan = (DateTime.ParseExact(x.HD.NgayNhan.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm"),
                NgayXuatBan = x.HD.NgayXuatBan.ToString()
            }).ToListAsync();
            return temp;
        }

        public async Task<List<TimKiemHoiDap>> getHoiDapNhanVien(string HoTen)
        {
            RemoveSpace rm = new RemoveSpace();
            var query = from NV in _CSKHContext.NhanViens
                        join NVHD in _CSKHContext.NvHoiDaps on NV.MaNhanVien equals NVHD.MaNhanVien
                        join HD in _CSKHContext.HoiDaps on NVHD.MaHoiDap equals HD.MaHoiDap
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        where NV.Ho.Contains(HoTen) || NV.Ten.Contains(HoTen)
                        select new { NV, HD, TT, SP };
            var temp = await query.Select(x => new TimKiemHoiDap()
            {
                Ho = rm.loaiBoKhoangTrang(x.NV.Ho),
                Ten = rm.loaiBoKhoangTrang(x.NV.Ten),
                NdHoiDap = x.HD.NdHoiDap,
                NdHoiDapEdit = x.HD.NdHoiDapEdit,
                TenTrangThai = x.TT.TenTrangThai,
                CongKhai = x.HD.CongKhai,
                TenSanPham = x.SP.TenSanPham,
                NdTraLoi = x.HD.NdTraLoi,
                NgayNhan = (DateTime.ParseExact(x.HD.NgayNhan.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm"),
                NgayXuatBan = x.HD.NgayXuatBan.ToString()
            }).ToListAsync();
            return temp;
        }

        public async Task<List<TimKiemHoiDap>> getHoiDapSanPham(string TenSanPham)
        {
            RemoveSpace rm = new RemoveSpace();
            var query = from KH in _CSKHContext.KhachHangs
                        join HD in _CSKHContext.HoiDaps on KH.MaKhachHang equals HD.MaKhachHang
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        where SP.TenSanPham.Contains(TenSanPham)
                        select new { KH, HD, TT, SP };
            var temp = await query.Select(x => new TimKiemHoiDap()
            {
                Ho = rm.loaiBoKhoangTrang(x.KH.Ho),
                Ten = rm.loaiBoKhoangTrang(x.KH.Ten),
                NdHoiDap = x.HD.NdHoiDap,
                NdHoiDapEdit = x.HD.NdHoiDapEdit,
                TenTrangThai = x.TT.TenTrangThai,
                CongKhai = x.HD.CongKhai,
                TenSanPham = x.SP.TenSanPham,
                NdTraLoi = x.HD.NdTraLoi,
                NgayNhan =(DateTime.ParseExact(x.HD.NgayNhan.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm"),
                NgayXuatBan = x.HD.NgayXuatBan.ToString()
            }).ToListAsync();
            return temp;
        }

        private DateTime? checkInputInvaild(string check)
        {
            if (check != "")
            {
                string input = (DateTime.ParseExact(check, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
                return DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else return null;
        }
        
        public async Task<ResponseOutput> xuLyCauHoi(HoiDapRequest request)
        {

            string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            var update_hoidap = await _CSKHContext.HoiDaps.FindAsync(request.MaHoiDap);
            var email_khachhang = from HD in _CSKHContext.HoiDaps
                                  join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                  where HD.MaHoiDap.Contains(request.MaHoiDap)
                                  select KH.Email;
            var email = email_khachhang.Select(x => x.ToString()).ToArray();
            var sp_khachhang = from HD in _CSKHContext.HoiDaps
                               where HD.MaHoiDap.Contains(request.MaHoiDap)
                               select HD.MaSanPham;
            var masanpham = sp_khachhang.Select(x => x.ToString()).ToArray();
            var checkKhachHang = from HD in _CSKHContext.HoiDaps
                                 where HD.MaHoiDap.Contains(request.MaHoiDap)
                                 select HD.MaKhachHang;
            var makhachhang = checkKhachHang.Select(x => x.ToString()).ToArray();
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var nv_hd = new NvHoiDap()
                {
                    MaNhanVien = request.MaNhanVien,
                    MaHoiDap = request.MaHoiDap,
                    NgayThang = DateTime.ParseExact(dt_ngaythang, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    NoiDungThucHien = request.NoiDungThucHien,
                    Quyen = request.Quyen
                };
                _CSKHContext.NvHoiDaps.Add(nv_hd);
                update_hoidap.NdHoiDapEdit = request.NdHoiDapEdit;
                update_hoidap.NdTraLoi = request.NdTraLoi;
                update_hoidap.TieuDe = request.TieuDe;
                update_hoidap.TraVeXuatBan = request.TraVeXuatBan;
                update_hoidap.TraVeDuyet = request.TraVeDuyet;
                update_hoidap.MaTrangThai = request.MaTrangThai;
                update_hoidap.NgayXuatBan = checkInputInvaild(request.NgayXuatBan);
                update_hoidap.CongKhai = request.CongKhai;
                update_hoidap.MaLoai = request.MaLoai;

                if(request.MaTrangThai == "TT005")
                {
                    var tb = new ThongBao
                    {
                        MaKhachHang = makhachhang[0],
                        MaLoaiThongBao = "LTB0002",
                        ThoiGian = update_hoidap.NgayXuatBan,
                        Check = true
                    };
                    _CSKHContext.ThongBaos.Add(tb);
                   
                    var message = new Message(new string[] { email[0] }, "Chăm sóc khách hàng VCS Việt Nam",string.Format("Câu hỏi của bạn đã được trả lời, xem câu hỏi tại <a href=\"http://{0}:{1}/HoiDap/ChiTietCauHoi?maCauHoi={2}&maSP={3}\" >đây</a>", _hosting.IpAddress, _hosting.Port, request.MaHoiDap, masanpham[0]));
                    await _EmailSender.SendEmailAsync(message);
                }
                foo.change = await _CSKHContext.SaveChangesAsync();
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

        public async Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen)
        {
            List<DanhSachSoLuongTheoTrangThai> ds = new List<DanhSachSoLuongTheoTrangThai>();
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var thongke = from HD in _CSKHContext.HoiDaps
                                      where (HD.MaTrangThai.Contains("TT001") || HD.MaTrangThai.Contains("TT002") || HD.MaTrangThai.Contains("TT004") || HD.MaTrangThai.Contains("TT005") || HD.MaTrangThai.Contains("TT007"))
                                      orderby HD.MaTrangThai
                                      group HD by HD.MaTrangThai into grp
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
                        var thongke = from HD in _CSKHContext.HoiDaps
                                      where (HD.MaTrangThai.Contains("TT001")||HD.MaTrangThai.Contains("TT002") || HD.MaTrangThai.Contains("TT003") || HD.MaTrangThai.Contains("TT004"))
                                      orderby HD.MaTrangThai
                                      group HD by HD.MaTrangThai into grp
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
                        var thongke = from HD in _CSKHContext.HoiDaps
                                      where (HD.MaTrangThai.Contains("TT001")||HD.MaTrangThai.Contains("TT003") || HD.MaTrangThai.Contains("TT005") || HD.MaTrangThai.Contains("TT004") || HD.MaTrangThai.Contains("TT007"))
                                      orderby HD.MaTrangThai
                                      group HD by HD.MaTrangThai into grp
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
                return (DateTime.ParseExact(convertStringDateTime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
            }
            else return "";
        }
        public async Task<List<HoiDapExpand>> danhSachCauHoiTheoQuyen(TenQuyenPaging tenQuyen)
        {
            List<HoiDapExpand> list = new List<HoiDapExpand>();
            List<HoiDapExpand> list_res = new List<HoiDapExpand>();
            RemoveSpace rm = new RemoveSpace();
            var hoidap_chuabientap = from HD in _CSKHContext.HoiDaps
                                     join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                     join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                     join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                     join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                     where HD.MaTrangThai.Contains("TT001")
                                     select new { HD, LHD, SP, TT, KH };
            foreach(var item in hoidap_chuabientap)
            {
                list.Add(new HoiDapExpand
                {
                    MaHoiDap = item.HD.MaHoiDap,
                    TieuDe = item.HD.TieuDe,
                    NdHoiDap = item.HD.NdHoiDap,
                    NdHoiDapEdit = item.HD.NdHoiDapEdit,
                    NdTraLoi = item.HD.NdTraLoi,
                    CongKhai = item.HD.CongKhai,
                    NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                    NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                    TraVeDuyet = item.HD.TraVeDuyet,
                    TraVeXuatBan = item.HD.TraVeXuatBan,
                    TenLoai = item.LHD.TenLoai,
                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                    TenSanPham = item.SP.TenSanPham,
                    TenTrangThai = item.TT.TenTrangThai,
                    MaLoaiHoiDap=item.HD.MaLoai,
                    MaSanPham=item.HD.MaSanPham,                    
                });
            }
            
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var query = from HD in _CSKHContext.HoiDaps
                                    join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                    join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                    join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                    join NVHD in _CSKHContext.NvHoiDaps on HD.MaHoiDap equals NVHD.MaHoiDap
                                    join NV in _CSKHContext.NhanViens on NVHD.MaNhanVien equals NV.MaNhanVien
                                    where (HD.MaTrangThai.Contains("TT002") || HD.MaTrangThai.Contains("TT004") || HD.MaTrangThai.Contains("TT005") || HD.MaTrangThai.Contains("TT007"))
                                    select new { HD, LHD, SP, TT ,KH,NVHD,NV};
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaHoiDap == item.HD.MaHoiDap);
                            if (check == false)
                            {
                                list.Add(new HoiDapExpand
                                {
                                    MaHoiDap = item.HD.MaHoiDap,
                                    TieuDe = item.HD.TieuDe,
                                    NdHoiDap = item.HD.NdHoiDap,
                                    NdHoiDapEdit = item.HD.NdHoiDapEdit,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " +rm.loaiBoKhoangTrang(item.NV.Ten),
                                    NdTraLoi = item.HD.NdTraLoi,
                                    CongKhai = item.HD.CongKhai,
                                    NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                                    NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                                    TraVeDuyet = item.HD.TraVeDuyet,
                                    TraVeXuatBan = item.HD.TraVeXuatBan,
                                    TenLoai = item.LHD.TenLoai,
                                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaLoaiHoiDap = item.HD.MaLoai,
                                    MaSanPham = item.HD.MaSanPham,
                                    MaBienTap=item.NV.MaNhanVien
                                });
                            }
                        }
                    }
                    else if (items == "Duyệt")
                    {
                        var query = from HD in _CSKHContext.HoiDaps
                                    join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                    join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                    join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                    join NVHD in _CSKHContext.NvHoiDaps on HD.MaHoiDap equals NVHD.MaHoiDap
                                    join NV in _CSKHContext.NhanViens on NVHD.MaNhanVien equals NV.MaNhanVien
                                    where (HD.MaTrangThai.Contains("TT002") || HD.MaTrangThai.Contains("TT003") || HD.MaTrangThai.Contains("TT004"))
                                    select new { HD, LHD, SP, TT, KH ,NV,NVHD};
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaHoiDap == item.HD.MaHoiDap);
                            if (check == false)
                            {
                                list.Add(new HoiDapExpand
                                {
                                    MaHoiDap = item.HD.MaHoiDap,
                                    TieuDe = item.HD.TieuDe,
                                    NdHoiDap = item.HD.NdHoiDap,
                                    NdHoiDapEdit = item.HD.NdHoiDapEdit,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    NdTraLoi = item.HD.NdTraLoi,
                                    CongKhai = item.HD.CongKhai,
                                    NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                                    NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                                    TraVeDuyet = item.HD.TraVeDuyet,
                                    TraVeXuatBan = item.HD.TraVeXuatBan,
                                    TenLoai = item.LHD.TenLoai,
                                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaLoaiHoiDap = item.HD.MaLoai,
                                    MaSanPham = item.HD.MaSanPham,
                                    MaBienTap = item.NV.MaNhanVien
                                });
                            }
                        }
                    }
                    else if (items == "Xuất bản")
                    {
                        var query = from HD in _CSKHContext.HoiDaps
                                    join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                    join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                    join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                    join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                    join NVHD in _CSKHContext.NvHoiDaps on HD.MaHoiDap equals NVHD.MaHoiDap
                                    join NV in _CSKHContext.NhanViens on NVHD.MaNhanVien equals NV.MaNhanVien
                                    where (HD.MaTrangThai.Contains("TT003") || HD.MaTrangThai.Contains("TT005") || HD.MaTrangThai.Contains("TT004") || HD.MaTrangThai.Contains("TT007"))
                                    select new { HD, LHD, SP, TT, KH,NV,NVHD };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaHoiDap == item.HD.MaHoiDap);
                            if (check == false)
                            {
                                list.Add(new HoiDapExpand
                                {
                                    MaHoiDap = item.HD.MaHoiDap,
                                    TieuDe = item.HD.TieuDe,
                                    NdHoiDap = item.HD.NdHoiDap,
                                    NdHoiDapEdit = item.HD.NdHoiDapEdit,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    NdTraLoi = item.HD.NdTraLoi,
                                    CongKhai = item.HD.CongKhai,
                                    NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                                    NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                                    TraVeDuyet = item.HD.TraVeDuyet,
                                    TraVeXuatBan = item.HD.TraVeXuatBan,
                                    TenLoai = item.LHD.TenLoai,
                                    TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                                    TenSanPham = item.SP.TenSanPham,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    MaLoaiHoiDap = item.HD.MaLoai,
                                    MaSanPham = item.HD.MaSanPham,
                                    MaBienTap = item.NV.MaNhanVien
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
                if (!string.IsNullOrEmpty(tenQuyen.maLoaiHoiDap))
                {
                    list = list.Where(S => S.MaLoaiHoiDap == tenQuyen.maLoaiHoiDap).ToList();
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapExpand>();
            }
            return list;
        }

        public async Task<List<HoiDapExpand>> getHoiDapTrangThai(string maTrangThai, string maBienTap, string maLoaiHoiDap, string maSanPham)
        {
            List<HoiDapExpand> list = new List<HoiDapExpand>();
            List<HoiDapExpand> list_res = new List<HoiDapExpand>();
            RemoveSpace rm = new RemoveSpace();
            try
            {
                if (maTrangThai == "TT001")
                {
                    var hoidap_chuabientap = from HD in _CSKHContext.HoiDaps
                                             join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                             join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                             join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                             join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                             where HD.MaTrangThai.Contains("TT001")
                                             select new { HD, LHD, SP, TT, KH };
                    foreach (var item in hoidap_chuabientap)
                    {
                        list.Add(new HoiDapExpand
                        {
                            MaHoiDap = item.HD.MaHoiDap,
                            TieuDe = item.HD.TieuDe,
                            NdHoiDap = item.HD.NdHoiDap,
                            NdHoiDapEdit = item.HD.NdHoiDapEdit,
                            NdTraLoi = item.HD.NdTraLoi,
                            CongKhai = item.HD.CongKhai,
                            NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                            NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                            TraVeDuyet = item.HD.TraVeDuyet,
                            TraVeXuatBan = item.HD.TraVeXuatBan,
                            TenLoai = item.LHD.TenLoai,
                            TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                            TenSanPham = item.SP.TenSanPham,
                            TenTrangThai = item.TT.TenTrangThai,
                            MaLoaiHoiDap = item.HD.MaLoai,
                            MaSanPham=item.HD.MaSanPham                            
                        });
                    }
                }
                else
                {
                    var query = from HD in _CSKHContext.HoiDaps
                                join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                                join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                                join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                                join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                                join NVHD in _CSKHContext.NvHoiDaps on HD.MaHoiDap equals NVHD.MaHoiDap
                                join NV in _CSKHContext.NhanViens on NVHD.MaNhanVien equals NV.MaNhanVien
                                where TT.MaTrangThai == maTrangThai
                                select new { HD, LHD, SP, TT, KH, NV, NVHD };

                    foreach (var item in query)
                    {
                        bool check = list.Exists(x => x.MaHoiDap == item.HD.MaHoiDap);
                        if (check == false)
                        {
                            list.Add(new HoiDapExpand
                            {
                                MaHoiDap = item.HD.MaHoiDap,
                                TieuDe = item.HD.TieuDe,
                                NdHoiDap = item.HD.NdHoiDap,
                                NdHoiDapEdit = item.HD.NdHoiDapEdit,
                                TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                NdTraLoi = item.HD.NdTraLoi,
                                CongKhai = item.HD.CongKhai,
                                NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                                NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                                TraVeDuyet = item.HD.TraVeDuyet,
                                TraVeXuatBan = item.HD.TraVeXuatBan,
                                TenLoai = item.LHD.TenLoai,
                                TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                                TenSanPham = item.SP.TenSanPham,
                                TenTrangThai = item.TT.TenTrangThai,
                                MaLoaiHoiDap = item.HD.MaLoai,
                                MaSanPham = item.HD.MaSanPham,
                                MaBienTap = item.NV.MaNhanVien
                            });
                        }
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
                if (!string.IsNullOrEmpty(maLoaiHoiDap))
                {
                    list = list.Where(S => S.MaLoaiHoiDap == maLoaiHoiDap).ToList();
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch(Exception ex)
            {
                return new List<HoiDapExpand>();
            }
            return list;
        }

        public async Task<List<HoiDapSupport>> getHoiDapMaHoiDap(string maHoiDap)
        {
            List<HoiDapSupport> list = new List<HoiDapSupport>();
            List<HoiDapSupport> list_res = new List<HoiDapSupport>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where HD.MaHoiDap.Contains(maHoiDap)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapSupport
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        MaLoai = item.LHD.MaLoai,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    }); ;
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapSupport>();
            }
            return list;
        }

        public async Task<List<HoiDapResponse>> getHoiDapMaLoaiHoiDap(string maLoaiHoiDap)
        {
            List<HoiDapResponse> list = new List<HoiDapResponse>();
            List<HoiDapResponse> list_res = new List<HoiDapResponse>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where HD.CongKhai == true
                        where LHD.MaLoai.Contains(maLoaiHoiDap)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapResponse
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    });
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapResponse>();
            }
            return list;
        }

        public async Task<List<HoiDapResponse>> getHoiDapMaKhachHang(string maKhachHang)
        {
            List<HoiDapResponse> list = new List<HoiDapResponse>();
            List<HoiDapResponse> list_res = new List<HoiDapResponse>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where TT.MaTrangThai.Contains("TT005")
                        where KH.MaKhachHang.Contains(maKhachHang)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapResponse
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = (DateTime.ParseExact(fullDatetime(item.HD.NgayXuatBan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),                
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    });
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayXuatBan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayXuatBan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayXuatBan = item.NgayXuatBan.Substring(0, 10);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapResponse>();
            }
            return list;
        }

        public async Task<List<HoiDapResponse>> getHoiDapMaSanPhamMaLoaiHoiDap(string maSanPham, string maLoaiHoiDap)
        {
            List<HoiDapResponse> list = new List<HoiDapResponse>();
            List<HoiDapResponse> list_res = new List<HoiDapResponse>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where TT.MaTrangThai.Contains("TT005")
                        where SP.MaSanPham.Contains(maSanPham)
                        where LHD.MaLoai.Contains(maLoaiHoiDap)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapResponse
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    });
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapResponse>();
            }
            return list;
        }

        public async Task<List<HoiDapResponse>> getHoiDapMaSanPham(string maSanPham)
        {
            List<HoiDapResponse> list = new List<HoiDapResponse>();
            List<HoiDapResponse> list_res = new List<HoiDapResponse>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where TT.MaTrangThai.Contains("TT005")
                        where SP.MaSanPham.Contains(maSanPham)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapResponse
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    });
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapResponse>();
            }
            return list;
        }

        public async Task<List<HoiDapResponse>> getHoiDapTieuDe(string tieuDe)
        {
            List<HoiDapResponse> list = new List<HoiDapResponse>();
            List<HoiDapResponse> list_res = new List<HoiDapResponse>();
            RemoveSpace rm = new RemoveSpace();
            var query = from HD in _CSKHContext.HoiDaps
                        join KH in _CSKHContext.KhachHangs on HD.MaKhachHang equals KH.MaKhachHang
                        join LHD in _CSKHContext.LoaiHoiDaps on HD.MaLoai equals LHD.MaLoai
                        join SP in _CSKHContext.SanPhams on HD.MaSanPham equals SP.MaSanPham
                        join TT in _CSKHContext.TrangThais on HD.MaTrangThai equals TT.MaTrangThai
                        where TT.MaTrangThai.Contains("TT005")
                        where HD.TieuDe.Contains(tieuDe)
                        select new { HD, LHD, SP, TT, KH };
            try
            {
                foreach (var item in query)
                {
                    list.Add(new HoiDapResponse
                    {
                        MaHoiDap = item.HD.MaHoiDap,
                        TieuDe = item.HD.TieuDe,
                        NdHoiDap = item.HD.NdHoiDap,
                        NdHoiDapEdit = item.HD.NdHoiDapEdit,
                        NdTraLoi = item.HD.NdTraLoi,
                        CongKhai = item.HD.CongKhai,
                        NgayNhan = getDatetime(item.HD.NgayNhan.ToString()),
                        NgayXuatBan = checkInvailDateTime(item.HD.NgayXuatBan.ToString()),
                        TraVeDuyet = item.HD.TraVeDuyet,
                        TraVeXuatBan = item.HD.TraVeXuatBan,
                        TenLoai = item.LHD.TenLoai,
                        TenKhachHang = rm.loaiBoKhoangTrang(item.KH.Ho) + " " + rm.loaiBoKhoangTrang(item.KH.Ten),
                        TenSanPham = item.SP.TenSanPham,
                        TenTrangThai = item.TT.TenTrangThai
                    });
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                //list = list.OrderByDescending(x => DateTime.Parse((DateTime.ParseExact(x.NgayNhan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"))).ToList();
                //list_res = list.OrderByDescending(x => x.MaHoiDap).ToList();
            }
            catch (Exception ex)
            {
                return new List<HoiDapResponse>();
            }
            return list;
        }

        public async Task<string> getMaSanPhamMaHoiDap(string maHoiDap)
        {
            var masanpham = await _CSKHContext.HoiDaps.FindAsync(maHoiDap);
            return masanpham.MaSanPham;
        }
        public async Task<ResponseOutput> deleteHoiDap(string maHoiDap)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_nvhoidap = (from NVHD in _CSKHContext.NvHoiDaps
                                      where NVHD.MaHoiDap == maHoiDap
                                      select NVHD).ToList();
                if (check_nvhoidap.Count() != 0)
                {
                    foreach (var items in check_nvhoidap)
                    {
                        _CSKHContext.NvHoiDaps.Remove(items);
                    }
                }
                var hoidap = await _CSKHContext.HoiDaps.FindAsync(maHoiDap);
                _CSKHContext.HoiDaps.Remove(hoidap);
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
