using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MNSX
{
    public interface INSXService
    {
        public Task<ResponseOutput> addNSX(NSXRequest request);
        public Task<List<NSXResponse>> getNSX();
        public Task<ResponseOutput> suaNSX(NSXRequest request);
    }
}
