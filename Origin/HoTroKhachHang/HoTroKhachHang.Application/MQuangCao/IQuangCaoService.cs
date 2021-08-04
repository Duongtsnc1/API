using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MQuangCao
{
    public interface IQuangCaoService
    {
        public Task<ResponseOutput> addQuangCao(QuangCaoRequest request);
        public Task<ResponseOutput> addQuangCaoBienTap(QuangCaoRequest request);
        public Task<List<DanhSachSoLuongTheoTrangThai>> danhSachSoLuongTrangThai(TenQuyen tenQuyen);
        public Task<List<QuangCaoExpand>> danhSachQuangCaoTheoQuyen(TenQuyenPaging tenQuyen);
        public Task<ResponseOutput> xuLyQuangCao(QuangCaoRequest request);
        public Task<List<QuangCaoResponse>> getById(string MaQuangCao);
        public Task<List<QuangCaoResponse>> getByIdNganhHang(string MaNganhHang);
        public Task<List<QuangCaoExpand>> getQuangCaoTrangThai(string maTrangThai,string manganhhang,string mabientap,int status);
        public Task<List<QuangCaoResponse>> getHienThiQuangCao();
        public Task<List<QuangCaoClient>> SameNganhHang(string maQuangCao);
        public Task<List<QuangCaoResponse>> getQuangCaoClient(string maQuangCao);
        public  Task<ResponseOutput> deleteQuangCao(string maQuangCao);
    }
}
