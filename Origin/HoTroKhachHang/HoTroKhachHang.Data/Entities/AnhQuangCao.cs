using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class AnhQuangCao
    {
        public int Id { get; set; }
        public string MaQuangCao { get; set; }
        public string TenAnh { get; set; }

        public virtual QuangCao MaQuangCaoNavigation { get; set; }
    }
}
