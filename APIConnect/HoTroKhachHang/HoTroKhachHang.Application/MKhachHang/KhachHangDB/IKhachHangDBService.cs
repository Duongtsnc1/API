using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MKhachHang.KhachHangDB
{
    public interface IKhachHangDBService
    {
        public Task<ResponseOutput> CheckUserName(KhachHangDBRequest request);
        public Task<ResponseOutput> updateInformation(KhachHangDBInformations info);
        public Task<List<KhachHangDBInformations>> getInformations(string maKhachHang);
    }
}
