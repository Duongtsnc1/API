using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Responses;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace HoTroKhachHang.Application.MBanner
{
    public class BannerService : IBannerService
    {
        private readonly CSKHContext _CSKHContext;
        public BannerService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }

        public async Task<ResponseOutput> addBanner(BannerRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var bn = new Banner()
                {
                    TenBanner = request.TenBanner,
                    LinkAnh = request.LinkAnh,
                    MoTa = request.MoTa
                };
                _CSKHContext.Banners.Add(bn);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.message = "";
            output.data = foo;
            return output;
        }

        public async Task<ResponseOutput> deleteBanner(int id)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var deleteBanner = await _CSKHContext.Banners.FindAsync(id);
                if (deleteBanner == null)
                {
                    output.isSuccess = false;
                    output.message = "Không có dữ liệu muốn xóa";
                    return output;
                }
                _CSKHContext.Banners.Remove(deleteBanner);
                foo.change = await _CSKHContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.message = "";
            output.data = foo;
            return output;
        }

        public async Task<List<BannerResponse>> getAll()
        {
            var query = from BN in _CSKHContext.Banners
                        select new { BN };
            var temp = await query.Select(x => new BannerResponse()
            {
                Id = x.BN.Id,
                TenBanner = x.BN.TenBanner,
                LinkAnh = x.BN.LinkAnh,
                MoTa = x.BN.MoTa
            }).ToListAsync();
            return temp;
        }
    }
}
