using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MThongBao;
using HoTroKhachHang.Application.fRepository.Responses;
namespace HoTroKhachHang.Application.MThongBao
{
    public interface IThongBaoService
    {
        public Task<List<ThongBaoResponse>> getAllThongBao(string maKhachHang);
        public Task<List<ThongBaoResponse>> getThongBao(string maKhachHang);
        public Task<SoLuongThongBao> soLuongThongBao(string maKhachHang);
        public Task<ResponseOutput> daXemThongBao(ThongBaoRequest request);
    }
}
