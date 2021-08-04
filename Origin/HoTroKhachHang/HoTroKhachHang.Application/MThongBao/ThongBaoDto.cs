using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MThongBao
{
    public class ThongBaoDto
    {
        public int Id { get; set; }
        public string TenLoaiThongBao { get; set; }
        public string ThoiGian { get; set; }
        public bool Check { get; set; }
    }
}
