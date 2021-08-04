using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class Quyen
    {
        public Quyen()
        {
            NvQuyens = new HashSet<NvQuyen>();
        }

        public string MaQuyen { get; set; }
        public string TenQuyen { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<NvQuyen> NvQuyens { get; set; }
    }
}
