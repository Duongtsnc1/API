using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.MLoaiTaiLieu;
namespace HoTroKhachHang.Application.MLoaiTaiLieu
{
    public interface ILoaiTaiLieuService
    {
        public Task<List<LoaiTaiLieuResponse>> getAll();
        public Task<List<LoaiTaiLieuResponse>> getAllAdmin();
        public Task<ResponseOutput> addLoaiTaiLieu(LoaiTaiLieuRequest request);
        public Task<ResponseOutput> updateLoaiTaiLieu(LoaiTaiLieuRequest request);
        public Task<ResponseOutput> trangThaiLoaiTaiLieu(LoaiTaiLieuRequest request);
        public Task<List<LoaiTaiLieuResponse>> searchLoaiTaiLieuMaLoaiTaiLieu(string maLoaiTaiLieu);
    }
}
