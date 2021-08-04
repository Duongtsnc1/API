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
                NhanVien loginUser = _context.NhanViens.FirstOrDefault(e => e.UserName == login.Username && e.Passwd == hash.MD5Hash(login.Password));
                if (loginUser == null)
                {
                    res.message = "Tài khoản hoặc mật khẩu không đúng";
                    res.isSuccess = false;
                    return res;
                }

                IdentityResponse token = await AuthenticateAsync(loginUser);
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
        private List<Quyen> GetUserRole(string maNhanVien)
        {
            try
            {
                var quyen = (from NV in _context.NhanViens
                                     join NV_Quyen in _context.NvQuyens on NV.MaNhanVien equals NV_Quyen.MaNhanVien
                                     join Q in _context.Quyens on NV_Quyen.MaQuyen equals Q.MaQuyen
                                     where NV.MaNhanVien == maNhanVien
                                     where NV_Quyen.TrangThai == true
                                     select Q).ToList();
                return quyen;
            }
            catch (Exception)
            {

                return new List<Quyen>();
            }
        }
        public async Task<IdentityResponse> AuthenticateAsync(NhanVien nhanvien)
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
                    new Claim("MaNhanVien", nhanvien.MaNhanVien.ToString()),
                    new Claim("Ten", nhanvien.Ten),
                    new Claim("Ho",nhanvien.Ho),
                    new Claim(ClaimTypes.Name,nhanvien.Ho + nhanvien.Ten),
                    new Claim("Email",nhanvien.Email==null?"":nhanvien.Email),
                    new Claim("UserName",nhanvien.UserName==null?"":nhanvien.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                });

                foreach (var item in GetUserRole(nhanvien.MaNhanVien))
                {
                    Subject.AddClaim(new Claim(ClaimTypes.Role, item.TenQuyen));
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
                var check_matkhaucu = await _context.KhachHangs.FindAsync(request.MaNhanVien);

                if (check_matkhaucu.Passwd != hash.MD5Hash(request.OldPassword))
                {
                    output.message = "Mật khẩu cũ không đúng";
                    output.isSuccess = false;
                    return output;
                }
                check_matkhaucu.Passwd = hash.MD5Hash(request.NewPassword);
                foo.change = await _context.SaveChangesAsync();
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
                var check_matkhaucu = await _context.NhanViens.FindAsync(request.MaNhanVien);

                if (check_matkhaucu.Passwd != hash.MD5Hash(request.OldPassword))
                {
                    output.message = "Mật khẩu cũ không đúng";
                    output.isSuccess = false;
                    return output;
                }
                check_matkhaucu.Passwd = hash.MD5Hash(request.NewPassword);
                foo.change = await _context.SaveChangesAsync();
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
    }
}
