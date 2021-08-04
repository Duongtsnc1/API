using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.fRepository.ResponsesForum;

namespace HoTroKhachHang.Application.fRepository.ResponsesForum
{
    public class ForumNhomChuDe
    {
        public string MaNhomChuDe { get; set; }
        public string TenNhomChuDe { get; set; }
        public string MoTa { get; set; }
        public string NgayTao { get; set; }
        public List<ForumChuDe> ChuDe { get; set; }
    }
}
