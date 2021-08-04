using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NvQuangCao
    {
        public int Id { get; set; }
        public string MaNhanVien { get; set; }
        public string MaQuangcao { get; set; }
        public string NoiDungThucHien { get; set; }
        public DateTime NgayThang { get; set; }
        public string Quyen { get; set; }

        public virtual NhanVien MaNhanVienNavigation { get; set; }
        public virtual QuangCao MaQuangcaoNavigation { get; set; }
    }
}
