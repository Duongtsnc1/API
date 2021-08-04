using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class QuangCao
    {
        public QuangCao()
        {
            AnhQuangCaos = new HashSet<AnhQuangCao>();
            NvQuangCaos = new HashSet<NvQuangCao>();
        }

        public string MaQuangCao { get; set; }
        public string TenQuangCao { get; set; }
        public string MaNganhHang { get; set; }
        public string MoTaQuangCao { get; set; }
        public string MaNsx { get; set; }
        public string GiaCa { get; set; }
        public string DiaDiem { get; set; }
        public DateTime? NgayDang { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string NguoiDeNghi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string MaTrangThai { get; set; }
        public string TraVeXuatBan { get; set; }
        public string TraVeDuyet { get; set; }
        public DateTime NgayNhan { get; set; }

        public virtual NganhHang MaNganhHangNavigation { get; set; }
        public virtual Nsx MaNsxNavigation { get; set; }
        public virtual TrangThai MaTrangThaiNavigation { get; set; }
        public virtual ICollection<AnhQuangCao> AnhQuangCaos { get; set; }
        public virtual ICollection<NvQuangCao> NvQuangCaos { get; set; }
    }
}
