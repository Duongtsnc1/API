using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string TenBanner { get; set; }
        public string LinkAnh { get; set; }
        public string MoTa { get; set; }
    }
}
