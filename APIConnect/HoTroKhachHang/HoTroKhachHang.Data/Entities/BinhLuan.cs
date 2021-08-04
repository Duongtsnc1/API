using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class BinhLuan
    {
        public int Id { get; set; }
        public DateTime NgayTao { get; set; }
        public string NoiDung { get; set; }
        public string MaKhachHang { get; set; }
        public string MaBaiDang { get; set; }

        public virtual BaiDang MaBaiDangNavigation { get; set; }
        public virtual KhachHang MaKhachHangNavigation { get; set; }
    }
}
