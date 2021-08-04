using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Responses
{
    public class linkDownload
    {
        public string MaTaiLieu { get; set; }
        public string TenTaiLieu { get; set; }
        public string TenFile { get; set; }
        public string NgayThang { get; set; }
        public bool Download { get; set; }
        public string DuongDan { get; set; }
        public string MoTa { get; set; }
    }
}
