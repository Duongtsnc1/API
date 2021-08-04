using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class PhongBan
    {
        public PhongBan()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string MaPhongBan { get; set; }
        public string TenPhongBan { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
