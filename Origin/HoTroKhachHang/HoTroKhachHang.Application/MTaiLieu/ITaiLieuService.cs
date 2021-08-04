using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Models.Pagination;

namespace HoTroKhachHang.Application.MTaiLieu
{
    public interface ITaiLieuService
    {
        public Task<ResponseOutput> addTaiLieu(TaiLieuRequest request);
        public Task<ResponseOutput> duyetTaiLieu(XuLyTaiLieu request);
        public Task<ResponseOutput> traVeTaiLieu(XuLyTaiLieu request);
        public Task<ResponseOutput> guiDuyetTaiLieu(XuLyTaiLieu request);
        public Task<ResponseOutput> thuHoiTaiLieu(XuLyTaiLieu request);
        public Task<ResponseOutput> huyTaiLieu(XuLyTaiLieu request);
        public Task<List<linkDownload>> listLinkDownload(string maSanPham);
        //public Task<ResponseOutput> getURL(string tenFile);
        public Task<List<TaiLieuTheoSanPham>> searchLoaiTaiLieuSanPham(string maSanPham, string loaiTaiLieu);
        public Task<List<TaiLieuTheoMaTaiLieu>> searchMaTaiLieuTaiLieu(string maTaiLieu);
        public Task<ResponseOutput> thongKeTheoTrangThai(string maTrangThai);
        public Task<List<SPham>> hienThiSanPham();
        public Task<List<LTaiLieu>> hienThiLoaiTaiLieu();
        public Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen);
        public Task<List<TaiLieuResponse>> danhSachTaiLieuTheoQuyen(TenQuyenPaging tenQuyen);
        public Task<List<TaiLieuResponse>> getTaiLieuTrangThai(string maTrangThai, string maBienTap, string maSanPham);
        public Task<List<Test_SanPhamTheoKhachHang>> testSanPhamTheoKhachHang(string maKhachHang); //test
        public Task<ResponseOutput> deleteTaiLieu(string maTaiLieu);

    }
}
