using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class LoaiTaiLieu
    {
        public LoaiTaiLieu()
        {
            TaiLieus = new HashSet<TaiLieu>();
        }

        public string MaLoaiTaiLieu { get; set; }
        public string TenLoaiTaiLieu { get; set; }
        public bool TrangThai { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<TaiLieu> TaiLieus { get; set; }
    }
}
