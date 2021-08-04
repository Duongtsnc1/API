using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class ChucDanh
    {
        public ChucDanh()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string MaChucDanh { get; set; }
        public string TenChucDanh { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
