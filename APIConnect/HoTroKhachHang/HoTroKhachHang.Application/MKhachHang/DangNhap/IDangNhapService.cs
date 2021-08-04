using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MKhachHang.DangNhap;

namespace HoTroKhachHang.Application.MKhachHang.DangNhap
{
    public interface IDangNhapService
    {
        public Task<ResponseOutput> LoginAsync(DangNhapRequest login);
        public Task<DangNhapResponse> AuthenticateAsync(WebService khachhang);
    }
}
