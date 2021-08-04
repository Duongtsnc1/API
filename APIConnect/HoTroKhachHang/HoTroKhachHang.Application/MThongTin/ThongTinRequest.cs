using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MThongTin
{
    public class ThongTinRequest
    {
        public int Id { get; set; }
        public string TenCongTy { get; set; }
        public string Logo { get; set; }
        public string Slogan { get; set; }
        public string Subslogan { get; set; }
        public string LinkFaceBook { get; set; }
        public string LinkSkype { get; set; }
        public string LinkTwitter { get; set; }
        public string LinkInstagram { get; set; }
        public string LinkBanDo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhoneBusiness { get; set; }
        public string TongDai { get; set; }
    }
}
