using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using HoTroKhachHang.Application.fRepository.Functions;
using HoTroKhachHang.Application.fRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace HoTroKhachHang.Application.MQuangCao
{
    public class QuangCaoService : IQuangCaoService
    {
        private readonly CSKHContext _CSKHContext;
        public QuangCaoService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string QuangCaoStatus(DateTime ngayhethan)
        {
            if (ngayhethan < DateTime.Now)
            {
                return "Đã hết hạn";
            }
            if (DateTime.Now.AddDays(5) >= ngayhethan)
            {
                return "Sắp hết hạn";
            }
            return "Đang hoạt động";
        }
        private string themMaQuangCao()
        {
            var check = from QC in _CSKHContext.QuangCaos
                        select QC;
            if (check.ToList().Count == 0)
            {
                return "QC0_00000001";
                //HD00000001
            }
            var query = (from MQC in _CSKHContext.QuangCaos
                         orderby MQC.MaQuangCao descending
                         select MQC.MaQuangCao).First();
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
        private DateTime? checkInputInvaild(string check)
        {
            if (check != "")
            {
                string input = (DateTime.ParseExact(check, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
                return DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else return null;
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
            if (!string.IsNullOrEmpty(check))
            {
                return (DateTime.ParseExact(fullDatetime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm");
            }
            else return null;
        }
        private DateTime? InPutDateTime(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(fullDatetime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
            }
            else return null;
        }
        public async Task<ResponseOutput> addQuangCaoBienTap(QuangCaoRequest request)
        {
            //string dt_NgayHetHan = (DateTime.ParseExact(request.NgayHetHan, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            string dt_NgayThang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var qc = new Data.Entities.QuangCao()
                {
                    MaQuangCao = themMaQuangCao(),
                    TenQuangCao = request.TenQuangCao,
                    MaNganhHang = request.MaNganhHang,
                    MoTaQuangCao = request.MoTaQuangCao,
                    MaNsx = request.MaNSX,
                    GiaCa = request.GiaCa,
                    DiaDiem = request.DiaDiem,
                    NgayHetHan = DateTime.ParseExact(request.NgayHetHan, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    NgayDang = null,
                    NguoiDeNghi = request.NguoiDeNghi,
                    Sdt = request.Sdt,
                    Email = request.Email,
                    MaTrangThai = "TT002",
                    NgayNhan = DateTime.Now
                };
                _CSKHContext.QuangCaos.Add(qc);
                foo.change = await _CSKHContext.SaveChangesAsync();
                var anhqc = new AnhQuangCao();
                foreach (var item in request.DuongDan)
                {
                    anhqc = new AnhQuangCao()
                    {
                        MaQuangCao = qc.MaQuangCao,
                        TenAnh = item
                    };
                    _CSKHContext.AnhQuangCaos.Add(anhqc);
                }

                var nvqc = new NvQuangCao()
                {
                    NgayThang = DateTime.ParseExact(dt_NgayThang, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Quyen = request.Quyen,
                    NoiDungThucHien = request.NoiDungThucHien,
                    MaNhanVien = request.MaNhanVien,
                    MaQuangcao = qc.MaQuangCao
                };
                _CSKHContext.NvQuangCaos.Add(nvqc);
                foo.change += await _CSKHContext.SaveChangesAsync();
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

        public async Task<ResponseOutput> addQuangCao(QuangCaoRequest request)
        {
            //string dt_NgayHetHan = (DateTime.ParseExact(request.NgayHetHan, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var qc = new Data.Entities.QuangCao()
                {
                    MaQuangCao = themMaQuangCao(),
                    TenQuangCao = request.TenQuangCao,
                    MaNganhHang = request.MaNganhHang,
                    MoTaQuangCao = request.MoTaQuangCao,
                    MaNsx = request.MaNSX,
                    GiaCa = request.GiaCa,
                    DiaDiem = request.DiaDiem,
                    NgayHetHan = DateTime.ParseExact(request.NgayHetHan, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    NgayDang = null,
                    NguoiDeNghi = request.NguoiDeNghi,
                    Sdt = request.Sdt,
                    Email = request.Email,
                    MaTrangThai = "TT001",
                    NgayNhan = DateTime.Now
                };
                _CSKHContext.QuangCaos.Add(qc);
                foo.change = await _CSKHContext.SaveChangesAsync();

                var anhqc = new AnhQuangCao();
                foreach (var item in request.DuongDan)
                {
                    anhqc = new AnhQuangCao()
                    {
                        MaQuangCao = qc.MaQuangCao,
                        TenAnh = item
                    };
                    _CSKHContext.AnhQuangCaos.Add(anhqc);
                }
                foo.change += await _CSKHContext.SaveChangesAsync();
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
        public async Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen)
        {
            List<DanhSachSoLuongTheoTrangThai> ds = new List<DanhSachSoLuongTheoTrangThai>();
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var thongke = from QC in _CSKHContext.QuangCaos
                                      where (QC.MaTrangThai.Contains("TT001") || QC.MaTrangThai.Contains("TT002") || QC.MaTrangThai.Contains("TT004") || QC.MaTrangThai.Contains("TT005") || QC.MaTrangThai.Contains("TT007"))
                                      orderby QC.MaTrangThai
                                      group QC by QC.MaTrangThai into grp
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
                        var thongke = from QC in _CSKHContext.QuangCaos
                                      where (QC.MaTrangThai.Contains("TT001") || QC.MaTrangThai.Contains("TT002") || QC.MaTrangThai.Contains("TT003") || QC.MaTrangThai.Contains("TT004"))
                                      orderby QC.MaTrangThai
                                      group QC by QC.MaTrangThai into grp
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
                        var thongke = from QC in _CSKHContext.QuangCaos
                                      where (QC.MaTrangThai.Contains("TT001") || QC.MaTrangThai.Contains("TT003") || QC.MaTrangThai.Contains("TT005") || QC.MaTrangThai.Contains("TT004") || QC.MaTrangThai.Contains("TT007"))
                                      orderby QC.MaTrangThai
                                      group QC by QC.MaTrangThai into grp
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
        public async Task<List<QuangCaoExpand>> danhSachQuangCaoTheoQuyen(TenQuyenPaging tenQuyen)
        {
            List<QuangCaoExpand> list = new List<QuangCaoExpand>();
            List<QuangCaoExpand> list_res = new List<QuangCaoExpand>();
            RemoveSpace rm = new RemoveSpace();
            var quangcao_chuabientap = from QC in _CSKHContext.QuangCaos
                                       join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                       join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                       join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                       where QC.MaTrangThai.Contains("TT001")
                                       select new { QC, TT, NH, NSX };
            foreach (var item in quangcao_chuabientap)
            {
                list.Add(new QuangCaoExpand()
                {
                    DiaDiem = item.QC.DiaDiem,
                    Email = item.QC.Email,
                    GiaCa = (item.QC.GiaCa),
                    MoTaQuangCao = item.QC.MoTaQuangCao,
                    NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                    NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                    NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                    NguoiDeNghi = item.QC.NguoiDeNghi,
                    Sdt = item.QC.Sdt,
                    TenNganhHang = item.NH.TenNganhHang,
                    TenQuangCao = item.QC.TenQuangCao,
                    TenTrangThai = item.TT.TenTrangThai,
                    TraVeDuyet = item.QC.TraVeDuyet,
                    TraVeXuatBan = item.QC.TraVeXuatBan,
                    XuatXu = item.NSX.TenNsx,
                    MaQuangCao = item.QC.MaQuangCao,
                    MaNganhHang = item.QC.MaNganhHang,
                    TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)
                });
            }
            try
            {
                foreach (var items in tenQuyen.tenQuyen)
                {
                    if (items == "Biên tập")
                    {
                        var query = from QC in _CSKHContext.QuangCaos
                                    join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                    join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                    join NVQC in _CSKHContext.NvQuangCaos on QC.MaQuangCao equals NVQC.MaQuangcao
                                    join NV in _CSKHContext.NhanViens on NVQC.MaNhanVien equals NV.MaNhanVien
                                    join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                    where (QC.MaTrangThai.Contains("TT002") || QC.MaTrangThai.Contains("TT004") || QC.MaTrangThai.Contains("TT005") || QC.MaTrangThai.Contains("TT007"))
                                    select new { QC, NH, TT, NV, NVQC, NSX };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaQuangCao == item.QC.MaQuangCao);
                            if (check == false)
                            {
                                list.Add(new QuangCaoExpand
                                {
                                    DiaDiem = item.QC.DiaDiem,
                                    Email = item.QC.Email,
                                    GiaCa = (item.QC.GiaCa),
                                    MoTaQuangCao = item.QC.MoTaQuangCao,
                                    NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                                    NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                                    NguoiDeNghi = item.QC.NguoiDeNghi,
                                    Sdt = item.QC.Sdt,
                                    TenNganhHang = item.NH.TenNganhHang,
                                    TenQuangCao = item.QC.TenQuangCao,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    TraVeDuyet = item.QC.TraVeDuyet,
                                    TraVeXuatBan = item.QC.TraVeXuatBan,
                                    XuatXu = item.NSX.TenNsx,
                                    MaQuangCao = item.QC.MaQuangCao,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    MaNganhHang = item.QC.MaNganhHang,
                                    MaBienTap = item.NV.MaNhanVien,
                                    NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                                    TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)
                                });
                            }
                        }
                    }
                    else if (items == "Duyệt")
                    {
                        var query = from QC in _CSKHContext.QuangCaos
                                    join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                    join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                    join NVQC in _CSKHContext.NvQuangCaos on QC.MaQuangCao equals NVQC.MaQuangcao
                                    join NV in _CSKHContext.NhanViens on NVQC.MaNhanVien equals NV.MaNhanVien
                                    join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                    where (QC.MaTrangThai.Contains("TT002") || QC.MaTrangThai.Contains("TT003") || QC.MaTrangThai.Contains("TT004"))
                                    select new { QC, NH, TT, NV, NVQC, NSX };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaQuangCao == item.QC.MaQuangCao);
                            if (check == false)
                            {
                                list.Add(new QuangCaoExpand
                                {
                                    DiaDiem = item.QC.DiaDiem,
                                    Email = item.QC.Email,
                                    GiaCa = (item.QC.GiaCa),
                                    MoTaQuangCao = item.QC.MoTaQuangCao,
                                    NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                                    NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                                    NguoiDeNghi = item.QC.NguoiDeNghi,
                                    Sdt = item.QC.Sdt,
                                    TenNganhHang = item.NH.TenNganhHang,
                                    TenQuangCao = item.QC.TenQuangCao,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    TraVeDuyet = item.QC.TraVeDuyet,
                                    TraVeXuatBan = item.QC.TraVeXuatBan,
                                    XuatXu = item.NSX.TenNsx,
                                    MaQuangCao = item.QC.MaQuangCao,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    MaNganhHang = item.QC.MaNganhHang,
                                    MaBienTap = item.NV.MaNhanVien,
                                    NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                                    TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)

                                });
                            }
                        }
                    }
                    else if (items == "Xuất bản")
                    {
                        var query = from QC in _CSKHContext.QuangCaos
                                    join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                    join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                    join NVQC in _CSKHContext.NvQuangCaos on QC.MaQuangCao equals NVQC.MaQuangcao
                                    join NV in _CSKHContext.NhanViens on NVQC.MaNhanVien equals NV.MaNhanVien
                                    join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                    where (QC.MaTrangThai.Contains("TT005") || QC.MaTrangThai.Contains("TT003") || QC.MaTrangThai.Contains("TT004") || QC.MaTrangThai.Contains("TT007"))
                                    select new { QC, NH, TT, NV, NVQC, NSX };
                        foreach (var item in query)
                        {
                            bool check = list.Exists(x => x.MaQuangCao == item.QC.MaQuangCao);
                            if (check == false)
                            {
                                list.Add(new QuangCaoExpand
                                {
                                    DiaDiem = item.QC.DiaDiem,
                                    Email = item.QC.Email,
                                    GiaCa = (item.QC.GiaCa),
                                    MoTaQuangCao = item.QC.MoTaQuangCao,
                                    NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                                    NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                                    NguoiDeNghi = item.QC.NguoiDeNghi,
                                    Sdt = item.QC.Sdt,
                                    TenNganhHang = item.NH.TenNganhHang,
                                    TenQuangCao = item.QC.TenQuangCao,
                                    TenTrangThai = item.TT.TenTrangThai,
                                    TraVeDuyet = item.QC.TraVeDuyet,
                                    TraVeXuatBan = item.QC.TraVeXuatBan,
                                    XuatXu = item.NSX.TenNsx,
                                    MaQuangCao = item.QC.MaQuangCao,
                                    TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                                    MaNganhHang = item.QC.MaNganhHang,
                                    MaBienTap = item.NV.MaNhanVien,
                                    NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                                    TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)
                                });
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(tenQuyen.maBienTap))
                {
                    list = list.Where(S => S.MaBienTap == tenQuyen.maBienTap).ToList();
                }
                if (!string.IsNullOrEmpty(tenQuyen.maNganhHang))
                {
                    list = list.Where(S => S.MaNganhHang == tenQuyen.maNganhHang).ToList();
                }
                if (tenQuyen.status == -1)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Đã hết hạn").ToList();
                }
                if (tenQuyen.status == 0)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Sắp hết hạn").ToList();
                }
                if (tenQuyen.status == 1)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Đang hoạt động").ToList();
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }

            }
            catch (Exception ex)
            {
                return new List<QuangCaoExpand>();
            }
            return list;
        }

        public async Task<List<QuangCaoResponse>> getById(string maQuangCao)
        {
            var list = new List<QuangCaoResponse>();
            try
            {
                var query = from QC in _CSKHContext.QuangCaos
                            join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                            join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                            join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                            where QC.MaQuangCao == maQuangCao
                            select new { QC, TT, NH, NSX };
                foreach (var item in query)
                {
                    string tenBienTap = "";
                    if (item.QC.MaTrangThai != "TT001")
                    {
                        RemoveSpace rm = new RemoveSpace();
                        var bientap = await (from NV in _CSKHContext.NhanViens
                                             join NVQC in _CSKHContext.NvQuangCaos on NV.MaNhanVien equals NVQC.MaNhanVien
                                             where NVQC.MaQuangcao == maQuangCao
                                             select new { NV }).ToListAsync();
                        tenBienTap = rm.loaiBoKhoangTrang(bientap[0].NV.Ho) + " " + rm.loaiBoKhoangTrang(bientap[0].NV.Ten);
                    }
                    var duongdan = new List<string>();
                    var anhqc = _CSKHContext.AnhQuangCaos.Where(s => s.MaQuangCao == item.QC.MaQuangCao).ToList();
                    foreach (var jtem in anhqc)
                    {
                        duongdan.Add(jtem.TenAnh);
                    }
                    list.Add(new QuangCaoResponse()
                    {
                        DiaDiem = item.QC.DiaDiem,
                        DuongDan = duongdan,
                        Email = item.QC.Email,
                        GiaCa = (item.QC.GiaCa),
                        MaNganhHang = item.QC.MaNganhHang,
                        MaTrangThai = item.QC.MaTrangThai,
                        MoTaQuangCao = item.QC.MoTaQuangCao,
                        NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                        NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                        NguoiDeNghi = item.QC.NguoiDeNghi,
                        Sdt = item.QC.Sdt,
                        TenNganhHang = item.NH.TenNganhHang,
                        TenQuangCao = item.QC.TenQuangCao,
                        TenTrangThai = item.TT.TenTrangThai,
                        TraVeDuyet = item.QC.TraVeDuyet,
                        TraVeXuatBan = item.QC.TraVeXuatBan,
                        XuatXu = item.NSX.TenNsx,
                        MaQuangCao = item.QC.MaQuangCao,
                        NgayNhan = getDatetime(item.QC.NgayNhan.ToString()),
                        TinhTrang = QuangCaoStatus(item.QC.NgayHetHan),
                        TenNguoiBienTap = tenBienTap,
                        MaNSX = item.NSX.MaNsx

                    });
                }
            }
            catch (Exception ex)
            {
                return new List<QuangCaoResponse>();
            }
            return list;

        }
        public async Task<List<QuangCaoResponse>> getByIdNganhHang(string MaNganhHang)
        {
            var list = new List<QuangCaoResponse>();
            try
            {
                var query = from QC in _CSKHContext.QuangCaos
                            join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                            join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                            join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                            where QC.MaNganhHang.Contains(MaNganhHang) && QC.MaTrangThai == "TT005" && DateTime.Compare(QC.NgayHetHan, DateTime.Now) > 0
                            select new { QC, TT, NH, NSX };
                foreach (var item in query)
                {
                    var duongdan = new List<string>();
                    var anhqc = _CSKHContext.AnhQuangCaos.Where(s => s.MaQuangCao == item.QC.MaQuangCao).ToList();
                    foreach (var jtem in anhqc)
                    {
                        duongdan.Add(jtem.TenAnh);
                    }
                    list.Add(new QuangCaoResponse()
                    {
                        DiaDiem = item.QC.DiaDiem,
                        DuongDan = duongdan,
                        Email = item.QC.Email,
                        GiaCa = (item.QC.GiaCa),
                        MaNganhHang = item.QC.MaNganhHang,
                        MaTrangThai = item.QC.MaTrangThai,
                        MoTaQuangCao = item.QC.MoTaQuangCao,
                        NgayDang = (DateTime.ParseExact(fullDatetime(item.QC.NgayDang.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                        NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                        NguoiDeNghi = item.QC.NguoiDeNghi,
                        Sdt = item.QC.Sdt,
                        TenNganhHang = item.NH.TenNganhHang,
                        TenQuangCao = item.QC.TenQuangCao,
                        TenTrangThai = item.TT.TenTrangThai,
                        TraVeDuyet = item.QC.TraVeDuyet,
                        TraVeXuatBan = item.QC.TraVeXuatBan,
                        XuatXu = item.NSX.TenNsx,
                        MaQuangCao = item.QC.MaQuangCao,
                        NgayNhan = getDatetime(item.QC.NgayNhan.ToString()),
                        TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)
                    }); ;
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayDang = item.NgayDang.Substring(0, 16);
                }
            }
            catch (Exception ex)
            {
                return new List<QuangCaoResponse>();
            }
            return list;

        }

        public async Task<List<QuangCaoExpand>> getQuangCaoTrangThai(string maTrangThai, string maNganhHang, string maBienTap, int status)
        {
            List<QuangCaoExpand> list = new List<QuangCaoExpand>();
            List<QuangCaoExpand> list_res = new List<QuangCaoExpand>();
            RemoveSpace rm = new RemoveSpace();
            try
            {
                if (maTrangThai == "TT001")
                {
                    var quangcao_chuabientap = from QC in _CSKHContext.QuangCaos
                                               join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                               join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                               join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                               where QC.MaTrangThai.Contains("TT001")
                                               select new { QC, TT, NH, NSX };
                    foreach (var item in quangcao_chuabientap)
                    {

                        list.Add(new QuangCaoExpand()
                        {
                            DiaDiem = item.QC.DiaDiem,
                            Email = item.QC.Email,
                            GiaCa = (item.QC.GiaCa),
                            MoTaQuangCao = item.QC.MoTaQuangCao,
                            NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                            NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                            NguoiDeNghi = item.QC.NguoiDeNghi,
                            Sdt = item.QC.Sdt,
                            TenNganhHang = item.NH.TenNganhHang,
                            TenQuangCao = item.QC.TenQuangCao,
                            TenTrangThai = item.TT.TenTrangThai,
                            TraVeDuyet = item.QC.TraVeDuyet,
                            TraVeXuatBan = item.QC.TraVeXuatBan,
                            XuatXu = item.NSX.TenNsx,
                            MaQuangCao = item.QC.MaQuangCao,
                            MaNganhHang = item.QC.MaNganhHang,
                            NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                            TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)

                        });
                    }
                }
                else
                {
                    var query = from QC in _CSKHContext.QuangCaos
                                join TT in _CSKHContext.TrangThais on QC.MaTrangThai equals TT.MaTrangThai
                                join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                                join NVQC in _CSKHContext.NvQuangCaos on QC.MaQuangCao equals NVQC.MaQuangcao
                                join NV in _CSKHContext.NhanViens on NVQC.MaNhanVien equals NV.MaNhanVien
                                join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                where QC.MaTrangThai == maTrangThai
                                select new { QC, TT, NH, NV, NVQC, NSX };
                    foreach (var item in query)
                    {
                        if (list.Exists(x => x.MaQuangCao == item.QC.MaQuangCao))
                        {
                            continue;
                        }
                        list.Add(new QuangCaoExpand()
                        {
                            DiaDiem = item.QC.DiaDiem,
                            Email = item.QC.Email,
                            GiaCa = (item.QC.GiaCa),
                            MoTaQuangCao = item.QC.MoTaQuangCao,
                            NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                            NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                            NguoiDeNghi = item.QC.NguoiDeNghi,
                            Sdt = item.QC.Sdt,
                            TenNganhHang = item.NH.TenNganhHang,
                            TenQuangCao = item.QC.TenQuangCao,
                            TenTrangThai = item.TT.TenTrangThai,
                            TraVeDuyet = item.QC.TraVeDuyet,
                            TraVeXuatBan = item.QC.TraVeXuatBan,
                            XuatXu = item.NSX.TenNsx,
                            MaQuangCao = item.QC.MaQuangCao,
                            TenNguoiBienTap = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten),
                            MaNganhHang = item.QC.MaNganhHang,
                            MaBienTap = item.NV.MaNhanVien,
                            NgayNhan = (DateTime.ParseExact(fullDatetime(item.QC.NgayNhan.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                            TinhTrang = QuangCaoStatus(item.QC.NgayHetHan)

                        });
                    }
                }
                if (!string.IsNullOrEmpty(maBienTap))
                {
                    list = list.Where(S => S.MaBienTap == maBienTap).ToList();
                }
                if (!string.IsNullOrEmpty(maNganhHang))
                {
                    list = list.Where(S => S.MaNganhHang == maNganhHang).ToList();
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayNhan, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayNhan = item.NgayNhan.Substring(0, 16);
                }
                if (status == -1)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Đã hết hạn").ToList();
                }
                if (status == 0)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Sắp hết hạn").ToList();
                }
                if (status == 1)
                {
                    list = list.Where(s => QuangCaoStatus(DateTime.ParseExact(s.NgayHetHan.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture)) == "Đang hoạt động").ToList();
                }

            }
            catch (Exception ex)
            {
                return new List<QuangCaoExpand>();
            }
            return list;
        }

        public async Task<List<QuangCaoResponse>> getHienThiQuangCao()
        {
            var list = new List<QuangCaoResponse>();

            try
            {
                var query = from QC in _CSKHContext.QuangCaos
                            where QC.MaTrangThai == "TT005" && QC.NgayHetHan > DateTime.Now
                            select new { QC };
                var temp = new List<QuangCaoResponse>();
                foreach (var item in query)
                {
                    var tenanh = _CSKHContext.AnhQuangCaos.FirstOrDefault(a => a.MaQuangCao == item.QC.MaQuangCao).TenAnh;
                    temp.Add(new QuangCaoResponse()
                    {
                        MaQuangCao = item.QC.MaQuangCao,
                        NgayDang = (DateTime.ParseExact(fullDatetime(item.QC.NgayDang.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                        DuongDan = new List<string>() { tenanh }

                    });
                }
                temp.Sort((a, b) => DateTime.ParseExact(b.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                var count = temp.Count >= 3 ? 3 : temp.Count;
                for (int i = 0; i < count; i++)
                {
                    list.Add(temp[i]);
                }
            }
            catch (Exception ex)
            {
                return new List<QuangCaoResponse>();
            }
            return list;
        }
        public async Task<ResponseOutput> xuLyQuangCao(QuangCaoRequest request)
        {
            var a = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            //string b = (DateTime.ParseExact(a, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            string dt_ngaythang = (DateTime.ParseExact(request.NgayThang, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            //string dt_ngayhethan = (DateTime.ParseExact(request.NgayHetHan, "dd-MM-yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd");
            var update_quangcao = await _CSKHContext.QuangCaos.FindAsync(request.MaQuangCao);
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var nv_qc = new NvQuangCao()
                {
                    MaNhanVien = request.MaNhanVien,
                    MaQuangcao = request.MaQuangCao,
                    NgayThang = DateTime.ParseExact(dt_ngaythang, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    NoiDungThucHien = request.NoiDungThucHien,
                    Quyen = request.Quyen
                };
                _CSKHContext.NvQuangCaos.Add(nv_qc);
                update_quangcao.DiaDiem = request.DiaDiem;
                update_quangcao.Email = request.Email;
                update_quangcao.GiaCa = request.GiaCa;
                update_quangcao.MaNganhHang = request.MaNganhHang;
                update_quangcao.MaTrangThai = request.MaTrangThai;
                update_quangcao.MoTaQuangCao = request.MoTaQuangCao;
                update_quangcao.NgayHetHan = DateTime.ParseExact(request.NgayHetHan, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                update_quangcao.TenQuangCao = request.TenQuangCao;
                update_quangcao.TraVeDuyet = request.TraVeDuyet;
                update_quangcao.TraVeXuatBan = request.TraVeXuatBan;
                update_quangcao.MaNsx = request.MaNSX;
                update_quangcao.NguoiDeNghi = request.NguoiDeNghi;

                if (request.MaTrangThai == "TT005")
                {
                    update_quangcao.NgayDang = DateTime.Now;
                }
                else if (update_quangcao.NgayDang != null)
                {

                }
                else
                    update_quangcao.NgayDang = null;
                if (request.DuongDan != null)
                {
                    var anhQuangcao = _CSKHContext.AnhQuangCaos.Where(s => s.MaQuangCao == update_quangcao.MaQuangCao);
                    foreach (var item in anhQuangcao)
                    {
                        _CSKHContext.AnhQuangCaos.Remove(_CSKHContext.AnhQuangCaos.Single(s => s.Id == item.Id));
                    }
                    foreach (var item in request.DuongDan)
                    {
                        var anhqc = new AnhQuangCao()
                        {
                            MaQuangCao = request.MaQuangCao,
                            TenAnh = item
                        };
                        _CSKHContext.Add(anhqc);
                    }
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
        public async Task<List<QuangCaoClient>> SameNganhHang(string maQuangCao)
        {
            var list = new List<QuangCaoClient>();
            try
            {
                var quangcao = (from QC in _CSKHContext.QuangCaos
                                where QC.MaQuangCao == maQuangCao
                                select new { QC.MaNganhHang }).FirstOrDefault();
                if (string.IsNullOrEmpty(quangcao.MaNganhHang))
                {
                    return new List<QuangCaoClient>();
                }
                var query = from QC in _CSKHContext.QuangCaos
                            where QC.MaNganhHang == quangcao.MaNganhHang && QC.MaTrangThai == "TT005" && QC.NgayHetHan > DateTime.Now
                            select new { QC };
                foreach (var item in query)
                {
                    if (item.QC.MaQuangCao != maQuangCao)
                    {
                        var anh = _CSKHContext.AnhQuangCaos.FirstOrDefault(a => a.MaQuangCao == item.QC.MaQuangCao).TenAnh;
                        list.Add(new QuangCaoClient()
                        {
                            DuongDan = new List<string>() { anh },
                            MaQuangCao = item.QC.MaQuangCao,
                            NgayDang = (DateTime.ParseExact(fullDatetime(item.QC.NgayDang.ToString()), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt"),
                            NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                            TenQuangCao = item.QC.TenQuangCao,
                            GiaCa = item.QC.GiaCa

                        });
                    }
                }
                list.Sort((a, b) => DateTime.ParseExact(b.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                foreach (var item in list)
                {
                    item.NgayDang = item.NgayDang.Substring(0, 16);
                }
            }
            catch { return new List<QuangCaoClient>(); }
            return list;
        }
        public async Task<List<QuangCaoResponse>> getQuangCaoClient(string maQuangCao)
        {
            var list = new List<QuangCaoResponse>();
            try
            {
                var query = from QC in _CSKHContext.QuangCaos
                            join NH in _CSKHContext.NganhHangs on QC.MaNganhHang equals NH.MaNganhHang
                            join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                            where QC.MaQuangCao == maQuangCao && QC.MaTrangThai == "TT005" && QC.NgayHetHan > DateTime.Now
                            select new { QC, NH, NSX };
                if (query.ToList().Count == 0)
                {
                    return new List<QuangCaoResponse>();
                }
                foreach (var item in query)
                {
                    var duongdan = new List<string>();
                    var anhqc = _CSKHContext.AnhQuangCaos.Where(s => s.MaQuangCao == item.QC.MaQuangCao).ToList();
                    foreach (var jtem in anhqc)
                    {
                        duongdan.Add(jtem.TenAnh);
                    }
                    list.Add(new QuangCaoResponse()
                    {
                        DiaDiem = item.QC.DiaDiem,
                        DuongDan = duongdan,
                        Email = item.QC.Email,
                        GiaCa = (item.QC.GiaCa),
                        MoTaQuangCao = item.QC.MoTaQuangCao,
                        NgayDang = getDatetime(item.QC.NgayDang.ToString()),
                        NgayHetHan = checkInvailDateTime(item.QC.NgayHetHan.ToString()),
                        NguoiDeNghi = item.QC.NguoiDeNghi,
                        Sdt = item.QC.Sdt,
                        TenNganhHang = item.NH.TenNganhHang,
                        TenQuangCao = item.QC.TenQuangCao,
                        XuatXu = item.NSX.TenNsx,
                        MaQuangCao = item.QC.MaQuangCao,
                        MaNganhHang = item.NH.MaNganhHang
                    });
                }
                list = list.OrderByDescending(s => s.NgayDang).ToList();
            }
            catch (Exception ex)
            {
                return new List<QuangCaoResponse>();
            }
            return list;
        }

        public async Task<ResponseOutput> deleteQuangCao(string maQuangCao)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_nvquangcao = (from NVQC in _CSKHContext.NvQuangCaos
                                        where NVQC.MaQuangcao == maQuangCao
                                        select NVQC).ToList();
                if (check_nvquangcao.Count() != 0)
                {
                    foreach (var items in check_nvquangcao)
                    {
                        _CSKHContext.NvQuangCaos.Remove(items);
                    }
                }
                var check_anhquangcao = (from AQC in _CSKHContext.AnhQuangCaos
                                         where AQC.MaQuangCao == maQuangCao
                                         select AQC).ToList();
                if (check_anhquangcao.Count() != 0)
                {
                    foreach (var items in check_anhquangcao)
                    {
                        _CSKHContext.AnhQuangCaos.Remove(items);
                    }
                }
                var quangcao = await _CSKHContext.QuangCaos.FindAsync(maQuangCao);
                _CSKHContext.QuangCaos.Remove(quangcao);
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
