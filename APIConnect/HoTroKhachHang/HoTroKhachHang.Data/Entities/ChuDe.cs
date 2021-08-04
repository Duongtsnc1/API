using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class ChuDe
    {
        public ChuDe()
        {
            BaiDangs = new HashSet<BaiDang>();
        }

        public string MaChuDe { get; set; }
        public DateTime NgayTao { get; set; }
        public string MoTa { get; set; }
        public string TenChuDe { get; set; }
        public string MaNhomChuDe { get; set; }
        public bool? TrangThai { get; set; }

        public virtual NhomChuDe MaNhomChuDeNavigation { get; set; }
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
    }
}
