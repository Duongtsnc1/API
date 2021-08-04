using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Models;
using System.Dynamic;
using HoTroKhachHang.Data.Entities;
using System.Globalization;
using HoTroKhachHang.Application.fRepository.Functions;
using HoTroKhachHang.Email;

namespace HoTroKhachHang.Application.MKhachHang.DangKi
{
    public class DangKiService : IDangKiService
    {
        private readonly IEmailSender _EmailSender;
        private readonly CSKHContext _CSKHContext;
        private readonly HostingConfiguration _hosting;
        public DangKiService(CSKHContext CSKHContext, IEmailSender EmailSender, HostingConfiguration hosting)
        {
            _CSKHContext = CSKHContext;
            _EmailSender = EmailSender;
            _hosting = hosting;
        }

        private string themMaKhachHang()
        {
            var check = from KH in _CSKHContext.KhachHangs
                        select KH;
            if (check == null)
            {
                return "KH0_00000001";
                //HD00000001
            }
            var query = (from MKH in _CSKHContext.KhachHangs
                         orderby MKH.MaKhachHang descending
                         select MKH.MaKhachHang).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2, 1));
            if (tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }
            string tempmiddle = middle.ToString();
            join_lastrecord = chu + tempmiddle + "_" + tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> Register(DangKiRequest register)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            CheckPhoneNumber phone = new CheckPhoneNumber();
            md5Hash md5 = new md5Hash();
            try
            {
                var check_register = from KH in _CSKHContext.KhachHangs
                                     where KH.Email == register.Email || KH.UserName == register.Username
                                     select KH;
                if (check_register.Count() != 0)
                {
                    output.isSuccess = false;
                    output.message = "Email hoặc tên đăng nhập đã tồn tại";
                    return output;
                }
                if (phone.IsPhoneNumber(register.Sdt) == false)
                {
                    output.isSuccess = false;
                    output.message = "Định dạng số điện thoại không đúng";
                    return output;
                }
                var kh = new KhachHang
                {
                    MaKhachHang = themMaKhachHang(),
                    Ho = register.Ho,
                    Ten = register.Ten,
                    Email = register.Email,
                    UserName = register.Username,
                    Passwd = md5.MD5Hash(register.Password),
                    Sdt = register.Sdt,
                    TrangThai = "Đã kích hoạt"
                };
                _CSKHContext.KhachHangs.Add(kh);
                foo.change = await _CSKHContext.SaveChangesAsync();

                var message = new Message(new string[] { register.Email }, "Chăm sóc khách hàng VCS Việt Nam", string.Format("Đăng nhập website tại <a href=\"http://{0}:{1}/Account/Login\" >đây</a>", _hosting.IpAddress, _hosting.Port));
                await _EmailSender.SendEmailAsync(message);

                output.isSuccess = true;
                output.message = "Tạo tài khoản thành công, vui lòng xác nhận qua email";
                output.data = foo;
                return output;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }
    }
}
