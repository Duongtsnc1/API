using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
namespace HoTroKhachHang.Application.MBanner
{
    public interface IBannerService
    {
        public Task<ResponseOutput> addBanner(BannerRequest request);
        public Task<List<BannerResponse>> getAll();
        public Task<ResponseOutput> deleteBanner(int id);
    }
}
