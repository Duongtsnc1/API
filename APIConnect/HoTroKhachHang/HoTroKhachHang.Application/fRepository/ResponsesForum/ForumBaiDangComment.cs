using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.ResponsesForum
{
    public class ForumBaiDangComment
    {
        public int Id { get; set; }
        public string NgayTao { get; set; }
        public string NoiDung { get; set; }
        public string MaKhachHang { get; set; }
        public string MaBaiDang { get; set; }
        public string TenKhachHang { get; set; }
    }
}
    