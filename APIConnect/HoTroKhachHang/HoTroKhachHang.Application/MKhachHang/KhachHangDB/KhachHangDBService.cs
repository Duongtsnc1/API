using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using HoTroKhachHang.Application.MKhachHang.DangNhap;
using AccountManageSystem.SessionAPI;
using HoTroKhachHang.Application.MIdentity;

namespace HoTroKhachHang.Application.MKhachHang.KhachHangDB
{
    public class KhachHangDBService : IKhachHangDBService
    {
        private readonly CSKHContext _CSKHContext;
        public KhachHangDBService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        public async Task<ResponseOutput> CheckUserName(KhachHangDBRequest request)
        {
            ResponseOutput output = new ResponseOutput();
           // dynamic foo = new ExpandoObject();
            try
            {
                var check = _CSKHContext.KhachHangs.FirstOrDefault(s => s.UserName == request.Username);
                if (check == null)                                  
                    output.message = "Tên tài khoản chưa được sửa dụng";                
                else
                    output.message = "Tên tài khoản đã được sửa dụng";
                output.data = null; 
            }
            catch(Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }           
            output.isSuccess = true;           
            return output;
        }        
        public async Task<List<KhachHangDBInformations>> getInformations(string maKhachHang)
        {
            var query = from KH in _CSKHContext.KhachHangs
                        where KH.MaKhachHang == maKhachHang && KH.TrangThai == "Đã kích hoạt"
                        select new { KH };
            var temp = await query.Select(x => new KhachHangDBInformations()
            {
                MaKhachHang = x.KH.MaKhachHang,
                Ho = x.KH.Ho,
                Ten = x.KH.Ten,
                Sdt = x.KH.Sdt,
                Cccd = x.KH.Cccd,
                GioiTinh = x.KH.GioiTinh,
                Email = x.KH.Email,
                NgaySinh = x.KH.NgaySinh == null ? "" : DateTime.ParseExact(x.KH.NgaySinh.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy"),
                Anh = x.KH.Anh,
                ChucVu = x.KH.ChucVu,
                CoQuan = x.KH.CoQuan,
                LinhVuc = x.KH.LinhVuc,
                NgayTuyen = x.KH.NgayTuyen==null ? "" : DateTime.ParseExact(x.KH.NgayTuyen.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy"),
                TrinhDo = x.KH.TrinhDo,
                UserName = x.KH.UserName,
                TrangThai = x.KH.TrangThai
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> updateInformation(KhachHangDBInformations info)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                //var session = new SessionAPIAsync();
                var check_informations = await _CSKHContext.KhachHangs.FindAsync(info.MaKhachHang);
                if (check_informations == null)
                {
                    output.isSuccess = false;
                    output.message = "Khách hàng này không tồn tại";
                    return output;
                }

                if (info.NgayTuyen != null && info.NgayTuyen != "")
                {
                    var dt_ngaytuyen = DateTime.ParseExact(info.NgayTuyen, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    check_informations.NgayTuyen = DateTime.ParseExact(dt_ngaytuyen, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                if (info.NgaySinh != null && info.NgaySinh != "")
                {
                    var dt_ngaysinh = DateTime.ParseExact(info.NgaySinh, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    check_informations.NgaySinh = DateTime.ParseExact(dt_ngaysinh, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                check_informations.Ho = info.Ho;
                check_informations.Ten = info.Ten;
                check_informations.Sdt = info.Sdt;
                check_informations.Cccd = info.Cccd;
                check_informations.ChucVu = info.ChucVu;
                check_informations.CoQuan = info.CoQuan;
                check_informations.GioiTinh = info.GioiTinh;
                check_informations.Anh = info.Anh;
                check_informations.TrinhDo = info.TrinhDo;
                check_informations.LinhVuc = info.LinhVuc;
                check_informations.Email = info.Email;

                var updateVCS = await IdentityGlobalServiceToken.session.UpdateCustomerAsync(IdentityGlobalServiceToken.Token,Guid.Parse(info.MaKhachHang),info.Ho+" "+info.Ten,info.Email,info.Sdt);
                foo.change = await _CSKHContext.SaveChangesAsync();

                output.isSuccess = true;
                output.data = foo;
                output.message = "Cập nhật thông tin thành công";
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
