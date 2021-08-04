using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Models
{
    public class TenQuyenPaging
    {
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
        public List<string> tenQuyen { get; set; }
        public string maLoaiHoiDap { get; set; }
        public string maSanPham { get; set; }
        public string maBienTap { get; set; }
        public string maNganhHang { get; set; }
        public int status { get; set; }
    }
}
