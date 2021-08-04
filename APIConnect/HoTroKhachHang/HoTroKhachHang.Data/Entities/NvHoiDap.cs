using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NvHoiDap
    {
        public int Id { get; set; }
        public string MaNhanVien { get; set; }
        public string MaHoiDap { get; set; }
        public string NoiDungThucHien { get; set; }
        public DateTime? NgayThang { get; set; }
        public string Quyen { get; set; }

        public virtual HoiDap MaHoiDapNavigation { get; set; }
        public virtual NhanVien MaNhanVienNavigation { get; set; }
    }
}
