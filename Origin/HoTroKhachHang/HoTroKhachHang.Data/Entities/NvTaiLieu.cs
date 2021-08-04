using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NvTaiLieu
    {
        public int Id { get; set; }
        public string MaNhanVien { get; set; }
        public string MaTaiLieu { get; set; }
        public string NoiDungThucHien { get; set; }
        public DateTime NgayThang { get; set; }
        public string Quyen { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual TaiLieu MaTaiLieuNavigation { get; set; }
    }
}
