using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NhomChuDe
    {
        public NhomChuDe()
        {
            ChuDes = new HashSet<ChuDe>();
        }

        public string MaNhomChuDe { get; set; }
        public string TenNhomChuDe { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual ICollection<ChuDe> ChuDes { get; set; }
    }
}
