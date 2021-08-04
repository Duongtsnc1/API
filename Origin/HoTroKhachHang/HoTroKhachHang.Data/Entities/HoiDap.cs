using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class HoiDap
    {
        public HoiDap()
        {
            NvHoiDaps = new HashSet<NvHoiDap>();
        }

        public string MaHoiDap { get; set; }
        public string TieuDe { get; set; }
        public string NdHoiDap { get; set; }
        public string MaKhachHang { get; set; }
        public string NdTraLoi { get; set; }
        public string MaTrangThai { get; set; }
        public bool? CongKhai { get; set; }
        public string MaSanPham { get; set; }
        public string MaLoai { get; set; }
        public string TraVeDuyet { get; set; }
        public string TraVeXuatBan { get; set; }
        public string NdHoiDapEdit { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime? NgayXuatBan { get; set; }

        public virtual KhachHang MaKhachHangNavigation { get; set; }
        public virtual LoaiHoiDap MaLoaiNavigation { get; set; }
        public virtual SanPham MaSanPhamNavigation { get; set; }
        public virtual TrangThai MaTrangThaiNavigation { get; set; }
        public virtual ICollection<NvHoiDap> NvHoiDaps { get; set; }
    }
}
