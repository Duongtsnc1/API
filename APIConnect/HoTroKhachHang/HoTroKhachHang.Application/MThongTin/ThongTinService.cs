using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace HoTroKhachHang.Application.MThongTin
{
    public class ThongTinService : IThongTinService
    {
        private readonly CSKHContext _CSKHContext;
        public ThongTinService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        public async Task<List<ThongTinResponse>> getAll()
        {
            var query = from QLTT in _CSKHContext.QlthongTins
                        select new { QLTT };
            var temp = await query.Select(x => new ThongTinResponse()
            {
                Id = x.QLTT.Id,
                TenCongTy = x.QLTT.TenCongTy,
                Address = x.QLTT.Address,
                LinkFaceBook = x.QLTT.LinkFaceBook,
                LinkSkype = x.QLTT.LinkSkype,
                LinkTwitter = x.QLTT.LinkTwitter,
                Logo = x.QLTT.Logo,
                Slogan = x.QLTT.Slogan,
                Subslogan = x.QLTT.Subslogan,
                Email = x.QLTT.Email,
                Phone = x.QLTT.Phone,
                PhoneBusiness = x.QLTT.PhoneBusiness,
                TongDai = x.QLTT.TongDai,
                LinkInstagram = x.QLTT.LinkInstagram,
                LinkBanDo = x.QLTT.LinkBanDo
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> updateThongTin(ThongTinRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var updateThongTin = await _CSKHContext.QlthongTins.FindAsync(request.Id);
                if(updateThongTin == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                updateThongTin.TenCongTy = request.TenCongTy;
                updateThongTin.Address = request.Address;
                updateThongTin.LinkFaceBook = request.LinkFaceBook;
                updateThongTin.LinkSkype = request.LinkSkype;
                updateThongTin.LinkTwitter = request.LinkTwitter;
                updateThongTin.Logo = request.Logo;
                updateThongTin.Slogan = request.Slogan;
                updateThongTin.Subslogan = request.Subslogan;
                updateThongTin.Email = request.Email;
                updateThongTin.Phone = request.Phone;
                updateThongTin.PhoneBusiness = request.PhoneBusiness;
                updateThongTin.TongDai = request.TongDai;
                updateThongTin.LinkInstagram = request.LinkInstagram;
                updateThongTin.LinkBanDo = request.LinkBanDo;
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.data = foo;
            output.message = "";
            output.isSuccess = true;
            return output;
        }
    }
}
