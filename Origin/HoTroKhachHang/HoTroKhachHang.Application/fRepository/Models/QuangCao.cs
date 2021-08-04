using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Models
{
    public class QuangCao
    {
        public string MaQuangCao { get; set; }
        public string TenQuangCao { get; set; }
        public string MaNganhHang { get; set; }
        public string MoTaQuangCao { get; set; }
        public string XuatXu { get; set; }
        public string GiaCa { get; set; }
        public string DiaDiem { get; set; }
        public string NgayDang { get; set; }
        public string NgayHetHan { get; set; }
        public string NguoiDeNghi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string MaTrangThai { get; set; }
        public List<string> DuongDan { get; set; } 
       public string NgayNhan { get; set; }
    }
}
