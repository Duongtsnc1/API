using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HoTroKhachHang.Application.MSanPham
{
    public class SanPhamService : ISanPhamService
    {
        private readonly CSKHContext _CSKHContext;
        public SanPhamService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        public async Task<List<SanPhamResponse>> getAll()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from SP in _CSKHContext.SanPhams
                        select new { SP };
            var temp = await query.Select(x => new SanPhamResponse()
            {
                MaSanPham = x.SP.MaSanPham,
                TenSanPham = x.SP.TenSanPham,
                Anh = x.SP.Anh,
                NgayTao = (DateTime.ParseExact(x.SP.NgayTao.ToString(),"yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                Link = x.SP.Link,
                MoTa = x.SP.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<List<SanPhamResponse>> getMaSanPham(string maSanPham)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            var query = from SP in _CSKHContext.SanPhams
                        where SP.MaSanPham.Contains(maSanPham)
                        select new { SP };
            var temp = await query.Select(x => new SanPhamResponse()
            {
                MaSanPham = x.SP.MaSanPham,
                TenSanPham = x.SP.TenSanPham,
                Anh = x.SP.Anh,
                NgayTao = (DateTime.ParseExact(x.SP.NgayTao.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy"),
                Link = x.SP.Link,
                MoTa = x.SP.MoTa
            }).ToListAsync();
            return temp;
        }
    }
}
