using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class LoaiHoiDap
    {
        public LoaiHoiDap()
        {
            HoiDaps = new HashSet<HoiDap>();
        }

        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HoiDap> HoiDaps { get; set; }
    }
}
