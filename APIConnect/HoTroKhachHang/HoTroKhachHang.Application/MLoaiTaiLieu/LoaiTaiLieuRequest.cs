using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MLoaiTaiLieu
{
    public class LoaiTaiLieuRequest
    {
        public string MaLoaiTaiLieu { get; set; }
        public string TenLoaiTaiLieu { get; set; }
        public bool TrangThai { get; set; }
        public string MoTa { get; set; }
    }
}
