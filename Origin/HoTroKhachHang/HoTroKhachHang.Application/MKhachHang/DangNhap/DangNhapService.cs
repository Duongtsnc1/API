using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Functions;

namespace HoTroKhachHang.Application.MKhachHang.DangNhap
{
    public class DangNhapService : IDangNhapService
    {
        private readonly CSKHContext _context;
        private readonly ServiceConfiguration _appSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public DangNhapService(CSKHContext context,
            IOptions<ServiceConfiguration> settings,
            TokenValidationParameters tokenValidationParameters)
        {
            _context = context;
            _appSettings = settings.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<DangNhapResponse> AuthenticateAsync(KhachHang khachhang)
        {
            dynamic foo = new ExpandoObject();
            DangNhapResponse identity = new DangNhapResponse();
            var tokenHandler = new JwtSecurityTokenHandler();
            RemoveSpace rm = new RemoveSpace();
            try
            {
                //var key = Convert.FromBase64String(_appSettings.JwtSettings.Secret);
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);
                ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("MaNhanVien", khachhang.MaKhachHang.ToString()),
                    new Claim("Ten", khachhang.Ten),
                    new Claim("Ho",khachhang.Ho),
                    new Claim(ClaimTypes.Name,rm.loaiBoKhoangTrang(khachhang.Ho) +" " +rm.loaiBoKhoangTrang(khachhang.Ten)),
                    new Claim("Email",khachhang.Email==null?"":khachhang.Email),
                    new Claim("UserName",khachhang.UserName==null?"":khachhang.UserName),
                    new Claim(ClaimTypes.Role,"Khách hàng"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = Subject,
                    //Expires = DateTime.UtcNow.Add(_appSettings.JwtSettings.TokenLifetime),
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                identity.Token = tokenHandler.WriteToken(token);
                return identity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseOutput> LoginAsync(DangNhapRequest login)
        {
            ResponseOutput res = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            md5Hash hash = new md5Hash();
            try
            {
                KhachHang loginUser = _context.KhachHangs.FirstOrDefault(e => e.UserName == login.Username && e.Passwd == hash.MD5Hash(login.Password) && e.TrangThai == "Đã kích hoạt");
                if (loginUser == null)
                {
                    res.message = "Tài khoản hoặc mật khẩu không đúng";
                    res.isSuccess = false;
                    return res;
                }

                DangNhapResponse token = await AuthenticateAsync(loginUser);
                res.data = token;
                res.message = "Đăng nhập thành công";
                res.isSuccess = true;
                return res;
            }
            catch(Exception ex)
            {
                res.message = ex.ToString();
                res.isSuccess = false;
                return res;
            }
}
    }
}
