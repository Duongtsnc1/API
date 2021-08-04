using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Responses;

namespace HoTroKhachHang.Application.fRepository.Models
{
    public class TaiLieuTheoSanPham
    {
        public string MaTaiLieu { get; set; }
        public string TenTaiLieu { get; set; }
        public string MoTa { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public bool DownLoad { get; set; }
        public string NgayThang { get; set; }
        public string TenLoaiTaiLieu { get; set; }
        public string TenSanPham { get; set; }
    }
}
