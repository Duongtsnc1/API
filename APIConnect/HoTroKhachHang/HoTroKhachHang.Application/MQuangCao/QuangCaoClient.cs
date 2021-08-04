using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MQuangCao
{
    public class QuangCaoClient
    {
        public string MaQuangCao { get; set; }
        public string TenQuangCao { get; set; }    
        public string NgayDang { get; set; }
        public string NgayHetHan { get; set; }
        public string GiaCa { get; set; }

        public List<string> DuongDan { get; set; }
    }
}
