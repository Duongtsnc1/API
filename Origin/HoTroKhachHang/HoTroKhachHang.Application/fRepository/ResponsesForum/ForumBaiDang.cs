using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.ResponsesForum
{
    public class ForumBaiDang
    {
        public string MaBaiDang { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string NgayDang { get; set; }
        public string LanBinhLuanCuoi { get; set; } // có thể null
        public string MaNguoiBinhLuanCuoi { get; set; }
        public string TenNguoiBinhLuanCuoi { get; set; }
        public string MaChuDe { get; set; }
        public string TenChuDe { get; set; }
        public int? SoLuongBinhLuan { get; set; }
        public bool? Hot { get; set; }
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public int? SoLuongXem { get; set; }
    }
}
