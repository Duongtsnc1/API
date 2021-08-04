using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MSanPham
{
    public class SanPhamRequest
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public string NgayTao { get; set; }
        public string Anh { get; set; }
        public string Link { get; set; }
    }
}
