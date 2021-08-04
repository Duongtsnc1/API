using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.ModelsForum
{
    public class ForumBaiDangRequest
    {
        public string maBaiDang { get; set; }
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
        public string ngayDang { get; set; }
        public string maChuDe { get; set; }
        public string maKhachHang { get; set; }
    }
}
