using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class ThongBao
    {
        public int Id { get; set; }
        public string MaLoaiThongBao { get; set; }
        public string MaKhachHang { get; set; }
        public DateTime? ThoiGian { get; set; }
        public bool Check { get; set; }

        public virtual KhachHang MaKhachHangNavigation { get; set; }
        public virtual LoaiThongBao MaLoaiThongBaoNavigation { get; set; }
    }
}
