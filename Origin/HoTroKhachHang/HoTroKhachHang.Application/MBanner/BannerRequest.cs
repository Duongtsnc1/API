using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MBanner
{
    public class BannerRequest
    {
        public int Id { get; set; }
        public string TenBanner { get; set; }
        public string LinkAnh { get; set; }
        public string MoTa { get; set; }
    }
}
