using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class Nsx
    {
        public Nsx()
        {
            QuangCaos = new HashSet<QuangCao>();
        }

        public string MaNsx { get; set; }
        public string TenNsx { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<QuangCao> QuangCaos { get; set; }
    }
}
