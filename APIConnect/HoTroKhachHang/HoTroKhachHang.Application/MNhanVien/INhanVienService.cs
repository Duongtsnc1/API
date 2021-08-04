using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Data.Entities;
using AccountManageSystem.SessionAPI;

namespace HoTroKhachHang.Application.MNhanVien
{
    public interface INhanVienService
    {
        public Task<List<EmployeeDTO>> getAll();
        public Task<List<NhanVienExpand>> getAllNVQuyen(string tenQuyen);
        public Task<List<NvQuyen>> quyenNhanVien(string maNhanVien);
        public Task<ResponseOutput> addAndUpdateQuyen(XuLyQuyen request);
        public Task<List<NhanVienQuyen>> nhanVienQuyen();
        public Task<List<NhanVienResponse>> getNhanVienMaNhanVien(string maNhanVien);
        public Task<ResponseOutput> capNhatQuyen(List<string> groupRoles);
    }
}
