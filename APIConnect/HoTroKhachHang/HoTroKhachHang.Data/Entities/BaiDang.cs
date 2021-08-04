using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class BaiDang
    {
        public BaiDang()
        {
            BinhLuans = new HashSet<BinhLuan>();
        }

        public string MaBaiDang { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
        public DateTime? LanBinhLuanCuoi { get; set; }
        public string MaNguoiBinhLuanCuoi { get; set; }
        public string MaChuDe { get; set; }
        public int? SoLuongBinhLuan { get; set; }
        public bool? Hot { get; set; }
        public string MaKhachHang { get; set; }
        public int? SoLuongXem { get; set; }

        public virtual ChuDe MaChuDeNavigation { get; set; }
        public virtual KhachHang MaKhachHangNavigation { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }
    }
}
