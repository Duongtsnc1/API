using System;
using System.Collections.Generic;
using System.Text;

namespace HoTroKhachHang.Application.MQuangCao
{
    public class QuangCaoRequest
    {
        public string MaQuangCao { get; set; }
        public string TenQuangCao { get; set; }
        public string MaNganhHang { get; set; }
        public string MoTaQuangCao { get; set; }
        public string MaNSX { get; set; }
        public string GiaCa { get; set; }
        public string DiaDiem { get; set; }
        public DateTime NgayDang { get; set; }
        public string NgayHetHan { get; set; }
        public string NguoiDeNghi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string MaTrangThai { get; set; }
        public List<string> DuongDan { get; set; }
        public string NoiDungThucHien { get; set; }
        public string NgayThang { get; set; }
        public string Quyen { get; set; }
        public string MaNhanVien { get; set; }
        public string TraVeDuyet { get; set; }
        public string TraVeXuatBan { get; set; }

    }
}
