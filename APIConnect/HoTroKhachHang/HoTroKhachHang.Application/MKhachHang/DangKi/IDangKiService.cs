using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;

namespace HoTroKhachHang.Application.MKhachHang.DangKi
{
    public interface IDangKiService
    {
        public Task<ResponseOutput> Register(DangKiRequest register);
    }
}
