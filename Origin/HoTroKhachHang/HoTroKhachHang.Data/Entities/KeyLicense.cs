using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class KeyLicense
    {
        public string MaKhachHang { get; set; }
        public string MaSanPham { get; set; }
        public DateTime NgayBd { get; set; }
        public DateTime NgayKt { get; set; }
        public int GiaHan { get; set; }

        public virtual KhachHang MaKhachHangNavigation { get; set; }
        public virtual SanPham MaSanPhamNavigation { get; set; }
    }
}
