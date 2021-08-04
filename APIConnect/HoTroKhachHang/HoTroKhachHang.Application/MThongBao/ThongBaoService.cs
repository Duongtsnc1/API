using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Dynamic;

namespace HoTroKhachHang.Application.MThongBao
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly CSKHContext _CSKHContext;
        public ThongBaoService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }

        public async Task<ResponseOutput> daXemThongBao(ThongBaoRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var updatethongbao = await _CSKHContext.ThongBaos.FindAsync(request.Id);
                updatethongbao.Check = false;
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.data = foo;
            output.isSuccess = true;
            output.message = "";
            return output;

        }

        public async Task<List<ThongBaoResponse>> getAllThongBao(string maKhachHang)
        {
            var query = from TB in _CSKHContext.ThongBaos
                        join LTB in _CSKHContext.LoaiThongBaos on TB.MaLoaiThongBao equals LTB.MaLoaiThongBao
                        where TB.MaKhachHang.Contains(maKhachHang)
                        select new { TB, LTB };
            var temp = await query.Select(x => new ThongBaoResponse()
            {
                Id = x.TB.Id,
                TenLoaiThongBao = x.LTB.TenLoaiThongBao,
                ThoiGian = (DateTime.ParseExact(x.TB.ThoiGian.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                Check = x.TB.Check
            }).ToListAsync();
            return temp;       
        }

        public async Task<List<ThongBaoResponse>> getThongBao(string maKhachHang)
        {
            var query = from TB in _CSKHContext.ThongBaos
                        join LTB in _CSKHContext.LoaiThongBaos on TB.MaLoaiThongBao equals LTB.MaLoaiThongBao
                        where TB.MaKhachHang.Contains(maKhachHang)
                        where TB.Check == true
                        select new { TB, LTB };
            var temp = await query.Select(x => new ThongBaoResponse()
            {
                Id = x.TB.Id,
                TenLoaiThongBao = x.LTB.TenLoaiThongBao,
                ThoiGian = (DateTime.ParseExact(x.TB.ThoiGian.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                Check = x.TB.Check
            }).ToListAsync();
            return temp;
        }

        public async Task<SoLuongThongBao> soLuongThongBao(string maKhachHang)
        {
            SoLuongThongBao soluong = new SoLuongThongBao();
            var query = from TB in _CSKHContext.ThongBaos
                        join LTB in _CSKHContext.LoaiThongBaos on TB.MaLoaiThongBao equals LTB.MaLoaiThongBao
                        where TB.MaKhachHang.Contains(maKhachHang)
                        where TB.Check == true
                        select new { TB, LTB };
            soluong.SoLuong = query.Count();
            return soluong;
        }
    }
}
