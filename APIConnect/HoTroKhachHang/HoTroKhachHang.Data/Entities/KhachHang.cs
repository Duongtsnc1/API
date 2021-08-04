using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            BaiDangs = new HashSet<BaiDang>();
            BinhLuans = new HashSet<BinhLuan>();
            HoiDaps = new HashSet<HoiDap>();
            KeyLicenses = new HashSet<KeyLicense>();
            ThongBaos = new HashSet<ThongBao>();
        }

        public string MaKhachHang { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string Anh { get; set; }
        public string CoQuan { get; set; }
        public DateTime? NgayTuyen { get; set; }
        public string ChucVu { get; set; }
        public string TrinhDo { get; set; }
        public string LinhVuc { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string UserName { get; set; }
        public string Passwd { get; set; }
        public string TrangThai { get; set; }

        public virtual ICollection<BaiDang> BaiDangs { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
        public virtual ICollection<HoiDap> HoiDaps { get; set; }
        public virtual ICollection<KeyLicense> KeyLicenses { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
