using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class SanPham
    {
        public SanPham()
        {
            HoiDaps = new HashSet<HoiDap>();
            KeyLicenses = new HashSet<KeyLicense>();
            TaiLieus = new HashSet<TaiLieu>();
        }

        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayTao { get; set; }
        public string Anh { get; set; }
        public string Link { get; set; }

        public virtual ICollection<HoiDap> HoiDaps { get; set; }
        public virtual ICollection<KeyLicense> KeyLicenses { get; set; }
        public virtual ICollection<TaiLieu> TaiLieus { get; set; }
    }
}
