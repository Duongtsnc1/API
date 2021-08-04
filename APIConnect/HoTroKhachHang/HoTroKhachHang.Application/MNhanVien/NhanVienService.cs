using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.MIdentity;
using HoTroKhachHang.Application.fRepository.Functions;
using AccountManageSystem.SessionAPI;

namespace HoTroKhachHang.Application.MNhanVien
{
    public class NhanVienService : INhanVienService
    {
        private readonly CSKHContext _CSKHContext;
        public NhanVienService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        public async Task<List<NhanVienExpand>> getAllNVQuyen(string tenQuyen)
        {
            var result = new List<NhanVienExpand>();
            RemoveSpace rm = new RemoveSpace();
            var query = from NV in _CSKHContext.NhanViens
                        join NVQ in _CSKHContext.NvQuyens on NV.MaNhanVien equals NVQ.MaNhanVien
                        join Q in _CSKHContext.Quyens on NVQ.MaQuyen equals Q.MaQuyen
                        where Q.TenQuyen.Contains(tenQuyen)
                        select new { NV };
            foreach(var item in query)
            {
                result.Add(new NhanVienExpand()
                {
                    MaNhanVien=item.NV.MaNhanVien,
                    TenNV = rm.loaiBoKhoangTrang(item.NV.Ho) + " " + rm.loaiBoKhoangTrang(item.NV.Ten)
                });
            }
            return result;
        }

        public async Task<List<EmployeeDTO>> getAll()
        {
            //var session = new SessionAPIAsync();
            var temp = await IdentityGlobalServiceToken.session.GetAllEmployee(IdentityGlobalServiceToken.Token);
            return temp;
        }

        public async Task<List<NvQuyen>> quyenNhanVien(string maNhanVien)
        {
            var query = from NV in _CSKHContext.NhanViens
                        join NV_Quyen in _CSKHContext.NvQuyens on NV.MaNhanVien equals NV_Quyen.MaNhanVien
                        join Q in _CSKHContext.Quyens on NV_Quyen.MaQuyen equals Q.MaQuyen
                        where NV.MaNhanVien.Contains(maNhanVien)
                        select new { NV_Quyen };
            var temp = await query.Select(x => new NvQuyen()
            {
                MaQuyen = x.NV_Quyen.MaQuyen,
                MaNhanVien = x.NV_Quyen.MaNhanVien,
                TrangThai = x.NV_Quyen.TrangThai
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> addAndUpdateQuyen(XuLyQuyen request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var updateQuyen = await _CSKHContext.NvQuyens.FindAsync(request.MaNhanVien, request.MaQuyen);
            try
            {
                if (updateQuyen != null)
                {
                    updateQuyen.MaNhanVien = request.MaNhanVien;
                    updateQuyen.MaQuyen = request.MaQuyen;
                    updateQuyen.TrangThai = bool.Parse(request.TrangThai);
                    foo.change = await _CSKHContext.SaveChangesAsync();
                }
                else
                {
                    var nv_quyen = new NvQuyen()
                    {
                        MaNhanVien = request.MaNhanVien,
                        MaQuyen = request.MaQuyen,
                        TrangThai = bool.Parse(request.TrangThai)
                    };
                    _CSKHContext.NvQuyens.Add(nv_quyen);
                    foo.change = await _CSKHContext.SaveChangesAsync();
                }
                output.data = foo;
                output.message = "";
                output.isSuccess = true;
                return output;
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        public async Task<List<NhanVienQuyen>> nhanVienQuyen()
        {
            List<NhanVienQuyen> list = new List<NhanVienQuyen>();
            var nhanvien = (from NV in _CSKHContext.NhanViens
                        select NV).ToList();
            try
            {
                foreach (var items in nhanvien)
                {
                    NhanVienQuyen nhanvien_quyen = (from NV in _CSKHContext.NhanViens
                                                    where NV.MaNhanVien == items.MaNhanVien
                                                    select new NhanVienQuyen
                                                    {
                                                        MaNhanVien = NV.MaNhanVien,
                                                        Ho = NV.Ho,
                                                        Ten = NV.Ten,
                                                        GioiTinh = NV.GioiTinh,
                                                        DiaChi = NV.DiaChi,
                                                        NgaySinh = NV.NgaySinh.ToString(),
                                                        Email = NV.Email,
                                                        Cccd = NV.Cccd,
                                                        Sdt = NV.Sdt,
                                                        TrinhDo = NV.TrinhDo,
                                                        Anh = NV.Anh,
                                                        NgayTuyenDung = NV.NgayTuyenDung.ToString(),
                                                        UserName = NV.UserName,
                                                        Passwd = NV.Passwd,
                                                        TrangThai = NV.TrangThai,
                                                        MaChucDanh = NV.MaChucDanh,
                                                        MaPhongBan = NV.MaPhongBan
                                                    }).FirstOrDefault();
                    List<string> roleNames = (from NV in _CSKHContext.NhanViens
                                              join NV_Quyen in _CSKHContext.NvQuyens on NV.MaNhanVien equals NV_Quyen.MaNhanVien
                                              join Q in _CSKHContext.Quyens on NV_Quyen.MaQuyen equals Q.MaQuyen
                                              where NV.MaNhanVien == items.MaNhanVien
                                              where NV_Quyen.TrangThai == true
                                              select Q.TenQuyen).ToList();
                    if(nhanvien_quyen != null)
                    {
                        nhanvien_quyen.Roles = roleNames;
                    }
                    //if (nhanvien_quyen.Roles.Exists(x => x.ToString() == "Admin"))
                    //{
                    //    continue;
                    //}
                    list.Add(nhanvien_quyen);
                    
                }
            }
            catch(Exception ex)
            {
                return new List<NhanVienQuyen>();
            }
            return list;
        }

        public async Task<List<NhanVienResponse>> getNhanVienMaNhanVien(string maNhanVien)
        {
            var query = from NV in _CSKHContext.NhanViens
                        where NV.MaNhanVien.Contains(maNhanVien)
                        select new { NV };
            var temp = await query.Select(x => new NhanVienResponse()
            {
                MaNhanVien = x.NV.MaNhanVien,
                Ho = x.NV.Ho,
                Ten = x.NV.Ten,
                GioiTinh = x.NV.GioiTinh,
                DiaChi = x.NV.DiaChi,
                NgaySinh = x.NV.NgaySinh.ToString(),
                Email = x.NV.Email,
                Cccd = x.NV.Cccd,
                Sdt = x.NV.Sdt,
                TrinhDo = x.NV.TrinhDo,
                Anh = x.NV.Anh,
                NgayTuyenDung = x.NV.NgayTuyenDung.ToString(),
                UserName = x.NV.UserName,
                Passwd = x.NV.Passwd,
                TrangThai = x.NV.TrangThai,
                MaChucDanh = x.NV.MaChucDanh,
                MaPhongBan = x.NV.MaPhongBan
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> capNhatQuyen(List<string> groupRoles)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                //var session = new SessionAPIAsync();
                var boolres = await IdentityGlobalServiceToken.session.AuthorizeServiceAsync(IdentityGlobalServiceToken.Token, IdentityGlobalServiceToken.UserID, groupRoles);
            }
            catch(Exception ex)
            {
                output.data = ex.ToString();
                output.isSuccess = false;
                return output;
            }

            output.isSuccess = true;
            output.data = foo;
            output.message = "";
            return output;
        }
    }
}
