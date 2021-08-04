using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NganhHang
    {
        public NganhHang()
        {
            QuangCaos = new HashSet<QuangCao>();
        }

        public string MaNganhHang { get; set; }
        public string TenNganhHang { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<QuangCao> QuangCaos { get; set; }
    }
}
