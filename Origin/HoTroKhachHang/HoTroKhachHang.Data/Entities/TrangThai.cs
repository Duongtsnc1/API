using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            HoiDaps = new HashSet<HoiDap>();
            QuangCaos = new HashSet<QuangCao>();
            TaiLieus = new HashSet<TaiLieu>();
        }

        public string MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HoiDap> HoiDaps { get; set; }
        public virtual ICollection<QuangCao> QuangCaos { get; set; }
        public virtual ICollection<TaiLieu> TaiLieus { get; set; }
    }
}
