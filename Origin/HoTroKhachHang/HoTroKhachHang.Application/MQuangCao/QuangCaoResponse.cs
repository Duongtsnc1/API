using System;
using System.Collections.Generic;
using System.Text;

namespace HoTroKhachHang.Application.MQuangCao
{
    public class QuangCaoResponse:QuangCaoDto
    {
        public string TenTrangThai { get; set; }
        public string TenNganhHang { get; set; }
        public string TinhTrang { get; set; }
        public string TenNguoiBienTap { get; set; }
        public string MaNSX { get; set; }

    }
}
