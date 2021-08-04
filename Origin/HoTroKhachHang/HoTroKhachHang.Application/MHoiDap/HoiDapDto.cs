using System;
using System.Collections.Generic;
using System.Text;

namespace HoTroKhachHang.Application.MHoiDap
{
    public class HoiDapDto
    {
        public string MaHoiDap { get; set; }
        public string TieuDe { get; set; }
        public string NdHoiDap { get; set; }
        public string TenKhachHang { get; set; }
        public string NdTraLoi { get; set; }
        public string TenTrangThai { get; set; }
        public bool? CongKhai { get; set; }
        public string TenSanPham { get; set; }
        public string TenLoai { get; set; }
        public string TraVeDuyet { get; set; }
        public string TraVeXuatBan { get; set; }
        public string NdHoiDapEdit { get; set; }
        public string NgayNhan { get; set; }
        public string NgayXuatBan { get; set; }
    }
}
