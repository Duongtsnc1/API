using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class TaiLieu
    {
        public TaiLieu()
        {
            NvTaiLieus = new HashSet<NvTaiLieu>();
        }

        public string MaTaiLieu { get; set; }
        public string TenTaiLieu { get; set; }
        public string MoTa { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public bool DownLoad { get; set; }
        public DateTime? NgayThang { get; set; }
        public string LyDoTraVe { get; set; }
        public string MaLoaiTaiLieu { get; set; }
        public string MaTrangThai { get; set; }
        public string MaSanPham { get; set; }

        public virtual LoaiTaiLieu MaLoaiTaiLieuNavigation { get; set; }
        public virtual SanPham MaSanPhamNavigation { get; set; }
        public virtual TrangThai MaTrangThaiNavigation { get; set; }
        public virtual ICollection<NvTaiLieu> NvTaiLieus { get; set; }
    }
}
