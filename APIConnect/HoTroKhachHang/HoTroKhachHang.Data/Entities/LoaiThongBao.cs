using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class LoaiThongBao
    {
        public LoaiThongBao()
        {
            ThongBaos = new HashSet<ThongBao>();
        }

        public string MaLoaiThongBao { get; set; }
        public string TenLoaiThongBao { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
