using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MSanPham;
using HoTroKhachHang.Application.fRepository.Responses;

namespace HoTroKhachHang.Application.MSanPham
{
    public interface ISanPhamService
    {
        public Task<List<SanPhamResponse>> getAll();
        public Task<List<SanPhamResponse>> getMaSanPham(string maSanPham);
    }
}
