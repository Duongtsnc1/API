using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;

namespace HoTroKhachHang.Application.MHoiDap
{
    public interface IHoiDapService
    {
        public Task<List<TimKiemHoiDap>> getHoiDapKhachHang(string HoTen);
        public Task<List<TimKiemHoiDap>> getHoiDapNhanVien(string HoTen);
        public Task<List<TimKiemHoiDap>> getHoiDapSanPham(string TenSanPham);
        public Task<List<HoiDapSupport>> getHoiDapMaHoiDap(string maHoiDap);
        public Task<List<HoiDapResponse>> getHoiDapMaLoaiHoiDap(string maLoaiHoiDap);
        public Task<List<HoiDapResponse>> getHoiDapMaKhachHang(string maKhachHang);
        public Task<List<HoiDapResponse>> getHoiDapMaSanPhamMaLoaiHoiDap(string maSanPham, string maLoaiHoiDap);
        public Task<List<HoiDapResponse>> getHoiDapMaSanPham(string maSanPham);
        public Task<List<HoiDapResponse>> getHoiDapTieuDe(string tieuDe);
        public Task<ResponseOutput> addCauHoi(HoiDapRequest request);
        public Task<ResponseOutput> xuLyCauHoi(HoiDapRequest request);
        public Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen);
        public Task<List<HoiDapExpand>> danhSachCauHoiTheoQuyen(TenQuyenPaging tenQuyen);
        public Task<List<HoiDapExpand>> getHoiDapTrangThai(string maTrangThai, string maBienTap, string maLoaiHoiDap, string maSanPham);
        public Task<string> getMaSanPhamMaHoiDap(string maHoiDap);
        public Task<ResponseOutput> deleteHoiDap(string maHoiDap);

    }
}
