using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
namespace HoTroKhachHang.Application.MThongTin
{
    public interface IThongTinService
    {
        public Task<List<ThongTinResponse>> getAll();
        public Task<ResponseOutput> updateThongTin(ThongTinRequest request);
    }
}
