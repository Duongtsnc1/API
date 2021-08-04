using HoTroKhachHang.Application.fRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MNganhHang
{
    public class NganhHangExpand
    {
        public string MaNganhHang { get; set; }
        public string TenNganhHang { get; set; }
        public string MoTaNganhHang { get; set; }
        public  List<QuangCao> QuangCaos { get; set; }
    }
}
