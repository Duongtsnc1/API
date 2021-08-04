using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HoTroKhachHang.Email;
using HoTroKhachHang.Application.fRepository.Functions;
using AccountManageSystem.SessionAPI;
using HoTroKhachHang.Application.MKhachHang.DangNhap;

namespace HoTroKhachHang.Application.MIdentity
{
    public class IdentityService : IIdentityService
    {
        private readonly CSKHContext _context;
        private readonly ServiceConfiguration _appSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IEmailSender _EmailSender;
        public IdentityService(CSKHContext context,
            IOptions<ServiceConfiguration> settings,
            TokenValidationParameters tokenValidationParameters, IEmailSender EmailSender)
        {
            _context = context;
            _appSettings = settings.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _EmailSender = EmailSender;
        }

        public async Task<ResponseOutput> LoginAsync(IdentityRequest login)
        {
            ResponseOutput res = new ResponseOutput();
            md5Hash hash = new md5Hash();
            dynamic foo = new ExpandoObject();
            try
            {
                List<string> ListGroupRoles = new List<string>();
                WebService webService = new WebService();
                //var session = new SessionAPIAsync();
                var loginUser = await IdentityGlobalServiceToken.session.EstablishConnectionAsync(login.Username, login.Password);
                var webServiceTicket = await IdentityGlobalServiceToken.session.AuthenticateServiceAsync();

                //khi đăng nhập k sử dụng hàm này nữa, đây là test
                var boolAddRole = await IdentityGlobalServiceToken.session.AuthorizeServiceAsync(webServiceTicket.ServiceToken,
                webServiceTicket.UserID, new List<string> { "Editor", "Approve","Publish","Admin" });

                var lstGroupRole = await IdentityGlobalServiceToken.session.GetRoleListAsync(webServiceTicket.ServiceToken);
                webService.Email = webServiceTicket.Email;
                webService.FullName = webServiceTicket.FullName;
                webService.PhoneNumber = webServiceTicket.PhoneNumber;
                webService.UserName = webServiceTicket.UserName;
                webService.ServerToken = webServiceTicket.ServiceToken;
                IdentityGlobalServiceToken.Token = webServiceTicket.ServiceToken;
                IdentityGlobalServiceToken.UserID = webServiceTicket.UserID;
                webService.UserID = webServiceTicket.UserID;
                foreach(var item in lstGroupRole)
                {
                    switch (item)
                    {
                        case "Editor":
                            ListGroupRoles.Add("Biên tập");
                            break;
                        case "Approve":
                            ListGroupRoles.Add("Duyệt");
                            break;
                        case "Publish":
                            ListGroupRoles.Add("Xuất bản");
                            break;
                        default:
                            ListGroupRoles.Add("Admin");
                            break;
                    }
                }
                webService.GroupRoles = ListGroupRoles;
                IdentityResponse token = await AuthenticateAsync(webService);
                res.data = token;
                res.message = "Đăng nhập thành công";
                res.isSuccess = true;
                return res;

            }catch(Exception ex)
            {
                res.message = ex.ToString();
                res.isSuccess = false;
                return res;
            }
        }
        public async Task<IdentityResponse> AuthenticateAsync(WebService nhanvien)
        {
            dynamic foo = new ExpandoObject();
            IdentityResponse identity = new IdentityResponse();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                //var key = Convert.FromBase64String(_appSettings.JwtSettings.Secret);
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);
                ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("MaNhanVien",nhanvien.UserID.ToString()),
                    new Claim(ClaimTypes.Name,nhanvien.FullName),
                    new Claim("Email",nhanvien.Email==null?"":nhanvien.Email),
                    new Claim("UserName",nhanvien.UserName==null?"":nhanvien.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                });

                foreach (var item in nhanvien.GroupRoles)
                {
                    Subject.AddClaim(new Claim(ClaimTypes.Role,item));
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
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseOutput> ForgetPasswordKhachHang(ForgetPassword request)
        {
            ResponseOutput output = new ResponseOutput();
            RandomPassword rd = new RandomPassword();
            md5Hash hash = new md5Hash();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_email = _context.KhachHangs.FirstOrDefault(e => e.Email == request.Email);
                if (check_email == null)
                {
                    output.message = "Email không tồn tại";
                    output.isSuccess = false;
                    return output;
                }

                var email = from KH in _context.KhachHangs
                            where KH.Email == request.Email
                            select KH.Email;
                var emailsender = email.Select(x => x.ToString()).ToArray();

                string matkhaumoi = rd.randomString();
                var check_makhachhang = from KH in _context.KhachHangs
                                        where KH.Email == request.Email
                                        select KH.MaKhachHang;
                var update_makhachhang = check_makhachhang.Select(x => x.ToString()).ToArray();
                var update_matkhaumoi = await _context.KhachHangs.FindAsync(update_makhachhang[0]);
                update_matkhaumoi.Passwd =hash.MD5Hash(matkhaumoi);

                foo.change = await _context.SaveChangesAsync();
                var message = new Message(new string[] { emailsender[0] }, "Chăm sóc khách hàng VCS Việt Nam", "Mật khẩu mới của bạn là: " + matkhaumoi);
                await _EmailSender.SendEmailAsync(message);
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.data = foo;
            output.isSuccess = true;
            output.message = "Thành công";
            return output;
        }

        public async Task<ResponseOutput> ForgetPasswordNhanVien(ForgetPassword request)
        {
            ResponseOutput output = new ResponseOutput();
            RandomPassword rd = new RandomPassword();
            md5Hash hash = new md5Hash();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_email = _context.NhanViens.FirstOrDefault(e => e.Email == request.Email);
                if (check_email == null)
                {
                    output.message = "Email không tồn tại";
                    output.isSuccess = false;
                    return output;
                }

                var email = from NV in _context.NhanViens
                            where NV.Email == request.Email
                            select NV.Email;
                var emailsender = email.Select(x => x.ToString()).ToArray();

                string matkhaumoi = rd.randomString();
                var check_manhanvien = from NV in _context.NhanViens
                                        where NV.Email == request.Email
                                        select NV.MaNhanVien;
                var update_manhanvien = check_manhanvien.Select(x => x.ToString()).ToArray();
                var update_matkhaumoi = await _context.NhanViens.FindAsync(update_manhanvien[0]);
                update_matkhaumoi.Passwd = hash.MD5Hash(matkhaumoi);

                foo.change = await _context.SaveChangesAsync();
                var message = new Message(new string[] { emailsender[0] }, "Chăm sóc khách hàng VCS Việt Nam", "Mật khẩu mới của bạn là: " + matkhaumoi);
                await _EmailSender.SendEmailAsync(message);
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.data = foo;
            output.isSuccess = true;
            output.message = "Thành công";
            return output;
        }

        public async Task<ResponseOutput> ChangePasswordKhachHang(ChangePassword request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            md5Hash hash = new md5Hash();
            try
            {

                //var check_matkhaucu = await _context.KhachHangs.FindAsync(request.MaNhanVien);

                //if (check_matkhaucu.Passwd != hash.MD5Hash(request.OldPassword))
                //{
                //    output.message = "Mật khẩu cũ không đúng";
                //    output.isSuccess = false;
                //    return output;
                //}
                //check_matkhaucu.Passwd = hash.MD5Hash(request.NewPassword);

                //var session = new SessionAPIAsync();
                var changepassword = await IdentityGlobalServiceToken.session.UpdateCredentialPasswordAsync(IdentityGlobalServiceToken.Token, IdentityGlobalServiceToken.UserID, request.NewPassword, request.OldPassword);
                //foo.change = await _context.SaveChangesAsync();
                foo.change = changepassword;
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.data = foo;
            output.message = "Đổi mật khẩu thành công !";
            return output;
        }

        public async Task<ResponseOutput> ChangePasswordNhanVien(ChangePassword request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            md5Hash hash = new md5Hash();
            try
            {
                //var check_matkhaucu = await _context.NhanViens.FindAsync(request.MaNhanVien);

                //if (check_matkhaucu.Passwd != hash.MD5Hash(request.OldPassword))
                //{
                //    output.message = "Mật khẩu cũ không đúng";
                //    output.isSuccess = false;
                //    return output;
                //}
                //check_matkhaucu.Passwd = hash.MD5Hash(request.NewPassword);
                //foo.change = await _context.SaveChangesAsync();

                //var session = new SessionAPIAsync();
                var changepassword = await IdentityGlobalServiceToken.session.UpdateCredentialPasswordAsync(IdentityGlobalServiceToken.Token, IdentityGlobalServiceToken.UserID, request.NewPassword, request.OldPassword);
                //foo.change = await _context.SaveChangesAsync();
                foo.change = changepassword;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.data = foo;
            output.message = "Đổi mật khẩu thành công !";
            return output;
        }

        public async Task<ResponseOutput> Logout()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                //var session = new SessionAPIAsync();
                var res = IdentityGlobalServiceToken.session.CloseConnectionAsync(IdentityGlobalServiceToken.Token);
                IdentityGlobalServiceToken.Token = "";
                IdentityGlobalServiceToken.UserID = Guid.Empty;
                IdentityGlobalServiceToken.CustomerID = Guid.Empty;

                foo.change = res;
                output.isSuccess = true;
                output.data = foo;
                output.message = "Đăng xuất thành công";
                return output;
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }
    }
}
