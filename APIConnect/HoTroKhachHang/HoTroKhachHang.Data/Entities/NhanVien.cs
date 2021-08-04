using System;
using System.Collections.Generic;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            NvHoiDaps = new HashSet<NvHoiDap>();
            NvQuangCaos = new HashSet<NvQuangCao>();
            NvQuyens = new HashSet<NvQuyen>();
            NvTaiLieus = new HashSet<NvTaiLieu>();
        }

        public string MaNhanVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string Anh { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TrinhDo { get; set; }
        public DateTime? NgayTuyenDung { get; set; }
        public string MaChucDanh { get; set; }
        public string MaPhongBan { get; set; }
        public string UserName { get; set; }
        public string Passwd { get; set; }
        public string TrangThai { get; set; }

        public virtual ChucDanh MaChucDanhNavigation { get; set; }
        public virtual PhongBan MaPhongBanNavigation { get; set; }
        public virtual ICollection<NvHoiDap> NvHoiDaps { get; set; }
        public virtual ICollection<NvQuangCao> NvQuangCaos { get; set; }
        public virtual ICollection<NvQuyen> NvQuyens { get; set; }
        public virtual ICollection<NvTaiLieu> NvTaiLieus { get; set; }
    }
}
