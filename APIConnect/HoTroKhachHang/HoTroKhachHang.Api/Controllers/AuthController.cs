using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MIdentity;
using HoTroKhachHang.Application.MKhachHang.KhachHangDB;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using System.Security.Claims;
using HoTroKhachHang.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.MKhachHang.DangKi;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _IdentityService;
        private readonly IKhachHangDBService _khachHangDBService;
        private readonly IDangKiService _dangKiService;
        public AuthController(IIdentityService IdentityService, IKhachHangDBService khachHangDBService, IDangKiService dangKiService)
        {
            _IdentityService = IdentityService;
            _khachHangDBService = khachHangDBService;
            _dangKiService = dangKiService;
        }
        public OkObjectResult makeOutput(ResponseOutput Data)
        {
            dynamic foo = new ExpandoObject();
            if (Data.isSuccess)
            {
                foo.message = "Success";
                foo.error = Data.message;
                foo.status = 200;
                foo.data = Data.data;
            }
            else
            {
                foo.message = "Error";
                foo.error = Data.message;
                foo.status = 400;
                foo.data = Data.data;
            }

            return Ok(foo);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] IdentityRequest request)
        {
            var result = await _IdentityService.LoginAsync(request);
            return makeOutput(result);
        }

        protected ResponseOutput GetUserIdFromToken()
        {
            UserCurrent user = new UserCurrent();
            ResponseOutput output = new ResponseOutput();
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if(identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        user.manhanvien = identity.FindFirst("MaNhanVien").Value;
                        user.roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    }
                }
                output.data = user;
                output.message = "";
                output.isSuccess = true;
                return output;
                
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }

        [HttpPost("usercurrent")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = GetUserIdFromToken();
            return makeOutput(result);
        }

        [HttpPost("khachhang-matkhaumoi")]
        public async Task<IActionResult> ForgetPasswordKhachHang(ForgetPassword request)
        {
            var result = await _IdentityService.ForgetPasswordKhachHang(request);
            return makeOutput(result);
        }

        [HttpPost("nhanvien-matkhaumoi")]
        public async Task<IActionResult> ForgetPasswordNhanVien(ForgetPassword request)
        {
            var result = await _IdentityService.ForgetPasswordNhanVien(request);
            return makeOutput(result);
        }

        [HttpPost("nhanvien-doimatkhau")]
        [Authorize(Roles = "Biên tập, Duyệt, Xuất bản, Admin")]
        public async Task<IActionResult> ChangePasswordNhanVien(ChangePassword request)
        {
            var result = await _IdentityService.ChangePasswordNhanVien(request);
            return makeOutput(result);
        }

        [HttpPost("khachhang-doimatkhau")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> ChangePasswordKhachHang(ChangePassword request)
        {
            var result = await _IdentityService.ChangePasswordKhachHang(request);
            return makeOutput(result);
        }
        [HttpPost("check-UserName")]
        //[Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> CheckUserName(KhachHangDBRequest request)
        {
            var result = await _khachHangDBService.CheckUserName(request);
            return makeOutput(result);
        }

        [HttpPost("Register")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> Register(DangKiRequest request)
        {
            var result = await _dangKiService.Register(request);
            return makeOutput(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _IdentityService.Logout();
            return makeOutput(result);
        }
    }
}
