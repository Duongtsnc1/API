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
using AccountManageSystem.SessionAPI;
using HoTroKhachHang.Application.MIdentity;
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

        public async Task<DangNhapResponse> AuthenticateAsync(WebService khachhang)
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
                    new Claim("MaNhanVien", khachhang.UserID.ToString()),
                    new Claim(ClaimTypes.Name,khachhang.FullName),
                    new Claim("Email",khachhang.Email==null?"":khachhang.Email),
                    new Claim("UserName",khachhang.UserName==null?"":khachhang.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                });
                foreach (var item in khachhang.GroupRoles)
                {
                    Subject.AddClaim(new Claim(ClaimTypes.Role, item));
                }
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
                List<string> ListGroupRoles = new List<string>();
                WebService webService = new WebService();
                //var session = new SessionAPIAsync();
                var loginUser = await IdentityGlobalServiceToken.session.EstablishConnectionAsync(login.Username, login.Password);
                var webServiceTicket = await IdentityGlobalServiceToken.session.AuthenticateServiceAsync();

                //khi đăng nhập k sử dụng hàm này nữa, đây là test
                var boolAddRole = await IdentityGlobalServiceToken.session.AuthorizeServiceAsync(webServiceTicket.ServiceToken,
                webServiceTicket.UserID, new List<string> { "Customer" });

                var lstGroupRole = await IdentityGlobalServiceToken.session.GetRoleListAsync(webServiceTicket.ServiceToken);
                webService.Email = webServiceTicket.Email;
                webService.FullName = webServiceTicket.FullName;
                webService.PhoneNumber = webServiceTicket.PhoneNumber;
                webService.UserName = webServiceTicket.UserName;
                webService.ServerToken = webServiceTicket.ServiceToken;
                webService.UserID = webServiceTicket.UserID;
                IdentityGlobalServiceToken.Token = webServiceTicket.ServiceToken;
                IdentityGlobalServiceToken.UserID = webServiceTicket.UserID;
                foreach (var item in lstGroupRole)
                {
                    switch (item)
                    {
                        case "Customer":
                            ListGroupRoles.Add("Khách hàng");
                            break;
                        default:
                            break;
                    }
                }
                webService.GroupRoles = ListGroupRoles;
                DangNhapResponse token = await AuthenticateAsync(webService);
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
