using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NvQuyen
    {
        public string MaNhanVien { get; set; }
        public string MaQuyen { get; set; }
        public bool TrangThai { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual Quyen MaQuyenNavigation { get; set; }
    }
}
