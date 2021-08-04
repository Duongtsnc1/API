using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MIdentity
{
    public interface IIdentityService
    {
        public Task<ResponseOutput> LoginAsync(IdentityRequest login);
        public Task<IdentityResponse> AuthenticateAsync(NhanVien nhanvien);
        public Task<ResponseOutput> ForgetPasswordKhachHang(ForgetPassword request);
        public Task<ResponseOutput> ForgetPasswordNhanVien(ForgetPassword request);
        public Task<ResponseOutput> ChangePasswordKhachHang(ChangePassword request);
        public Task<ResponseOutput> ChangePasswordNhanVien(ChangePassword request);
    }
}
