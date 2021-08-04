using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HoTroKhachHang.Data.Entities
{
    public partial class CSKHContext : DbContext
    {
        public CSKHContext()
        {
        }

        public CSKHContext(DbContextOptions<CSKHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnhQuangCao> AnhQuangCaos { get; set; }
        public virtual DbSet<BaiDang> BaiDangs { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<ChucDanh> ChucDanhs { get; set; }
        public virtual DbSet<Gium> Gia { get; set; }
        public virtual DbSet<HoiDap> HoiDaps { get; set; }
        public virtual DbSet<KeyLicense> KeyLicenses { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiHoiDap> LoaiHoiDaps { get; set; }
        public virtual DbSet<LoaiTaiLieu> LoaiTaiLieus { get; set; }
        public virtual DbSet<LoaiThongBao> LoaiThongBaos { get; set; }
        public virtual DbSet<NganhHang> NganhHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhomChuDe> NhomChuDes { get; set; }
        public virtual DbSet<Nsx> Nsxes { get; set; }
        public virtual DbSet<NvHoiDap> NvHoiDaps { get; set; }
        public virtual DbSet<NvQuangCao> NvQuangCaos { get; set; }
        public virtual DbSet<NvQuyen> NvQuyens { get; set; }
        public virtual DbSet<NvTaiLieu> NvTaiLieus { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<QlthongTin> QlthongTins { get; set; }
        public virtual DbSet<QuangCao> QuangCaos { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiLieu> TaiLieus { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9IHGHBO;Initial Catalog=CSKH;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AnhQuangCao>(entity =>
            {
                entity.ToTable("AnhQuangCao");

                entity.Property(e => e.MaQuangCao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maQuangCao");

                entity.Property(e => e.TenAnh).HasColumnName("tenAnh");

                entity.HasOne(d => d.MaQuangCaoNavigation)
                    .WithMany(p => p.AnhQuangCaos)
                    .HasForeignKey(d => d.MaQuangCao)
                    .HasConstraintName("FK__AnhQuangC__maQua__5CD6CB2B");
            });

            modelBuilder.Entity<BaiDang>(entity =>
            {
                entity.HasKey(e => e.MaBaiDang)
                    .HasName("PK__BaiDang__BD09D33B4E1CF749");

                entity.ToTable("BaiDang");

                entity.Property(e => e.MaBaiDang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maBaiDang");

                entity.Property(e => e.Hot)
                    .HasColumnName("hot")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LanBinhLuanCuoi)
                    .HasColumnType("date")
                    .HasColumnName("lanBinhLuanCuoi");

                entity.Property(e => e.MaChuDe)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maChuDe");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.MaNguoiBinhLuanCuoi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNguoiBinhLuanCuoi");

                entity.Property(e => e.NgayDang)
                    .HasColumnType("date")
                    .HasColumnName("ngayDang");

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasColumnName("noiDung");

                entity.Property(e => e.SoLuongBinhLuan)
                    .HasColumnName("soLuongBinhLuan")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SoLuongXem)
                    .HasColumnName("soLuongXem")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasColumnName("tieuDe");

                entity.HasOne(d => d.MaChuDeNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaChuDe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDang__maChuDe__5DCAEF64");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK__BaiDang__maKhach__5EBF139D");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");

                entity.Property(e => e.LinkAnh)
                    .IsRequired()
                    .HasColumnName("linkAnh");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenBanner)
                    .IsRequired()
                    .HasColumnName("tenBanner");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.ToTable("BinhLuan");

                entity.Property(e => e.MaBaiDang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maBaiDang");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao");

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasColumnName("noiDung");

                entity.HasOne(d => d.MaBaiDangNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaBaiDang)
                    .HasConstraintName("FK__BinhLuan__maBaiD__5FB337D6");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.BinhLuans)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK__BinhLuan__maKhac__60A75C0F");
            });

            modelBuilder.Entity<ChuDe>(entity =>
            {
                entity.HasKey(e => e.MaChuDe)
                    .HasName("PK__ChuDe__19E3E79F2C8445C2");

                entity.ToTable("ChuDe");

                entity.Property(e => e.MaChuDe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maChuDe");

                entity.Property(e => e.MaNhomChuDe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhomChuDe");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao");

                entity.Property(e => e.TenChuDe).HasColumnName("tenChuDe");

                entity.Property(e => e.TrangThai).HasColumnName("trangThai");

                entity.HasOne(d => d.MaNhomChuDeNavigation)
                    .WithMany(p => p.ChuDes)
                    .HasForeignKey(d => d.MaNhomChuDe)
                    .HasConstraintName("FK__ChuDe__maNhomChu__619B8048");
            });

            modelBuilder.Entity<ChucDanh>(entity =>
            {
                entity.HasKey(e => e.MaChucDanh)
                    .HasName("PK__ChucDanh__2284EAF398221095");

                entity.ToTable("ChucDanh");

                entity.Property(e => e.MaChucDanh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maChucDanh");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenChucDanh)
                    .HasMaxLength(100)
                    .HasColumnName("tenChucDanh");
            });

            modelBuilder.Entity<Gium>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gia).HasColumnName("gia");
            });

            modelBuilder.Entity<HoiDap>(entity =>
            {
                entity.HasKey(e => e.MaHoiDap)
                    .HasName("PK__HoiDap__A6533CEAAE141AC6");

                entity.ToTable("HoiDap");

                entity.Property(e => e.MaHoiDap)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maHoiDap");

                entity.Property(e => e.CongKhai).HasColumnName("congKhai");

                entity.Property(e => e.MaKhachHang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.MaLoai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoai");

                entity.Property(e => e.MaSanPham)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maSanPham");

                entity.Property(e => e.MaTrangThai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTrangThai");

                entity.Property(e => e.NdHoiDap)
                    .IsRequired()
                    .HasColumnName("ndHoiDap");

                entity.Property(e => e.NdHoiDapEdit).HasColumnName("ndHoiDapEdit");

                entity.Property(e => e.NdTraLoi).HasColumnName("ndTraLoi");

                entity.Property(e => e.NgayNhan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayNhan");

                entity.Property(e => e.NgayXuatBan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayXuatBan");

                entity.Property(e => e.TieuDe)
                    .IsRequired()
                    .HasColumnName("tieuDe");

                entity.Property(e => e.TraVeDuyet).HasColumnName("traVeDuyet");

                entity.Property(e => e.TraVeXuatBan).HasColumnName("traVeXuatBan");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoiDap__maKhachH__628FA481");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoiDap__maLoai__6383C8BA");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoiDap__maSanPha__6477ECF3");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.HoiDaps)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoiDap__maTrangT__656C112C");
            });

            modelBuilder.Entity<KeyLicense>(entity =>
            {
                entity.HasKey(e => new { e.MaKhachHang, e.MaSanPham })
                    .HasName("PK__KeyLicen__297F048DF098E314");

                entity.ToTable("KeyLicense");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.MaSanPham)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maSanPham");

                entity.Property(e => e.GiaHan).HasColumnName("giaHan");

                entity.Property(e => e.NgayBd)
                    .HasColumnType("date")
                    .HasColumnName("ngayBD");

                entity.Property(e => e.NgayKt)
                    .HasColumnType("date")
                    .HasColumnName("ngayKT");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.KeyLicenses)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KeyLicens__maKha__66603565");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.KeyLicenses)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KeyLicens__maSan__6754599E");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__0CCB3D49E43418CA");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.Anh)
                    .IsUnicode(false)
                    .HasColumnName("anh");

                entity.Property(e => e.Cccd)
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.ChucVu)
                    .HasMaxLength(100)
                    .HasColumnName("chucVu");

                entity.Property(e => e.CoQuan).HasColumnName("coQuan");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(10)
                    .HasColumnName("gioiTinh");

                entity.Property(e => e.Ho)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ho");

                entity.Property(e => e.LinhVuc)
                    .HasMaxLength(200)
                    .HasColumnName("linhVuc");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaySinh");

                entity.Property(e => e.NgayTuyen)
                    .HasColumnType("date")
                    .HasColumnName("ngayTuyen");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("passwd");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ten");

                entity.Property(e => e.TrangThai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("trangThai");

                entity.Property(e => e.TrinhDo)
                    .HasMaxLength(200)
                    .HasColumnName("trinhDo");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<LoaiHoiDap>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiHoiD__E5A6B228267D3A2D");

                entity.ToTable("LoaiHoiDap");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoai");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenLoai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenLoai");
            });

            modelBuilder.Entity<LoaiTaiLieu>(entity =>
            {
                entity.HasKey(e => e.MaLoaiTaiLieu)
                    .HasName("PK__LoaiTaiL__52492BD9D3A0E396");

                entity.ToTable("LoaiTaiLieu");

                entity.Property(e => e.MaLoaiTaiLieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoaiTaiLieu");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenLoaiTaiLieu)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenLoaiTaiLieu");

                entity.Property(e => e.TrangThai).HasColumnName("trangThai");
            });

            modelBuilder.Entity<LoaiThongBao>(entity =>
            {
                entity.HasKey(e => e.MaLoaiThongBao)
                    .HasName("PK__LoaiThon__BD1054EAEDB0D9EF");

                entity.ToTable("LoaiThongBao");

                entity.Property(e => e.MaLoaiThongBao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoaiThongBao");

                entity.Property(e => e.MoTa)
                    .HasMaxLength(100)
                    .HasColumnName("moTa");

                entity.Property(e => e.TenLoaiThongBao)
                    .IsRequired()
                    .HasColumnName("tenLoaiThongBao");
            });

            modelBuilder.Entity<NganhHang>(entity =>
            {
                entity.HasKey(e => e.MaNganhHang)
                    .HasName("PK__NganhHan__72349F9F06297D59");

                entity.ToTable("NganhHang");

                entity.Property(e => e.MaNganhHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNganhHang");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenNganhHang)
                    .IsRequired()
                    .HasColumnName("tenNganhHang");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NhanVien__BDDEF20D50A277F4");

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNhanVien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhanVien");

                entity.Property(e => e.Anh)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("anh");

                entity.Property(e => e.Cccd)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("CCCD");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("diaChi");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.GioiTinh)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("gioiTinh");

                entity.Property(e => e.Ho)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ho");

                entity.Property(e => e.MaChucDanh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maChucDanh");

                entity.Property(e => e.MaPhongBan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maPhongBan");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaySinh");

                entity.Property(e => e.NgayTuyenDung)
                    .HasColumnType("date")
                    .HasColumnName("ngayTuyenDung");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("passwd");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.TrangThai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("trangThai");

                entity.Property(e => e.TrinhDo)
                    .HasMaxLength(100)
                    .HasColumnName("trinhDo");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.MaChucDanhNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaChucDanh)
                    .HasConstraintName("FK__NhanVien__maChuc__68487DD7");

                entity.HasOne(d => d.MaPhongBanNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaPhongBan)
                    .HasConstraintName("FK__NhanVien__maPhon__693CA210");
            });

            modelBuilder.Entity<NhomChuDe>(entity =>
            {
                entity.HasKey(e => e.MaNhomChuDe)
                    .HasName("PK__NhomChuD__9D5F1C894747D855");

                entity.ToTable("NhomChuDe");

                entity.Property(e => e.MaNhomChuDe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhomChuDe");

                entity.Property(e => e.MoTa)
                    .IsUnicode(false)
                    .HasColumnName("moTa");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao");

                entity.Property(e => e.TenNhomChuDe)
                    .IsRequired()
                    .HasColumnName("tenNhomChuDe");
            });

            modelBuilder.Entity<Nsx>(entity =>
            {
                entity.HasKey(e => e.MaNsx)
                    .HasName("PK__NSX__26994274D342735C");

                entity.ToTable("NSX");

                entity.Property(e => e.MaNsx)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNSX");

                entity.Property(e => e.Mota).HasColumnName("mota");

                entity.Property(e => e.TenNsx)
                    .IsRequired()
                    .HasColumnName("tenNSX");
            });

            modelBuilder.Entity<NvHoiDap>(entity =>
            {
                entity.ToTable("NV_HoiDap");

                entity.Property(e => e.MaHoiDap)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maHoiDap");

                entity.Property(e => e.MaNhanVien)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhanVien");

                entity.Property(e => e.NgayThang)
                    .HasColumnType("date")
                    .HasColumnName("ngayThang");

                entity.Property(e => e.NoiDungThucHien)
                    .IsRequired()
                    .HasColumnName("noiDungThucHien");

                entity.Property(e => e.Quyen)
                    .HasMaxLength(100)
                    .HasColumnName("quyen");

                entity.HasOne(d => d.MaHoiDapNavigation)
                    .WithMany(p => p.NvHoiDaps)
                    .HasForeignKey(d => d.MaHoiDap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_HoiDap__maHoi__6A30C649");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.NvHoiDaps)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_HoiDap__maNha__6B24EA82");
            });

            modelBuilder.Entity<NvQuangCao>(entity =>
            {
                entity.ToTable("NV_QuangCao");

                entity.Property(e => e.MaNhanVien)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhanVien");

                entity.Property(e => e.MaQuangcao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maQuangcao");

                entity.Property(e => e.NgayThang)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayThang");

                entity.Property(e => e.NoiDungThucHien)
                    .IsRequired()
                    .HasColumnName("noiDungThucHien");

                entity.Property(e => e.Quyen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("quyen");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.NvQuangCaos)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_QuangC__maNha__6C190EBB");

                entity.HasOne(d => d.MaQuangcaoNavigation)
                    .WithMany(p => p.NvQuangCaos)
                    .HasForeignKey(d => d.MaQuangcao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_QuangC__maQua__6D0D32F4");
            });

            modelBuilder.Entity<NvQuyen>(entity =>
            {
                entity.HasKey(e => new { e.MaNhanVien, e.MaQuyen })
                    .HasName("PK__NV_Quyen__94AEF3D77B32C9EC");

                entity.ToTable("NV_Quyen");

                entity.Property(e => e.MaNhanVien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhanVien");

                entity.Property(e => e.MaQuyen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maQuyen");

                entity.Property(e => e.TrangThai).HasColumnName("trangThai");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.NvQuyens)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_Quyen__maNhan__6E01572D");

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.NvQuyens)
                    .HasForeignKey(d => d.MaQuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_Quyen__maQuye__6EF57B66");
            });

            modelBuilder.Entity<NvTaiLieu>(entity =>
            {
                entity.ToTable("NV_TaiLieu");

                entity.Property(e => e.MaNhanVien)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNhanVien");

                entity.Property(e => e.MaTaiLieu)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTaiLieu");

                entity.Property(e => e.NgayThang)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayThang");

                entity.Property(e => e.NoiDungThucHien)
                    .IsRequired()
                    .HasColumnName("noiDungThucHien");

                entity.Property(e => e.Quyen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("quyen");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.NvTaiLieus)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_TaiLie__maNha__619B8048");

                entity.HasOne(d => d.MaTaiLieuNavigation)
                    .WithMany(p => p.NvTaiLieus)
                    .HasForeignKey(d => d.MaTaiLieu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NV_TaiLie__maTai__628FA481");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.HasKey(e => e.MaPhongBan)
                    .HasName("PK__PhongBan__3A946B08F1D4EC6B");

                entity.ToTable("PhongBan");

                entity.Property(e => e.MaPhongBan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maPhongBan");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenPhongBan)
                    .HasMaxLength(100)
                    .HasColumnName("tenPhongBan");
            });

            modelBuilder.Entity<QlthongTin>(entity =>
            {
                entity.ToTable("QLThongTin");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.LinkBanDo).HasColumnName("linkBanDo");

                entity.Property(e => e.LinkFaceBook)
                    .IsUnicode(false)
                    .HasColumnName("linkFaceBook");

                entity.Property(e => e.LinkInstagram).HasColumnName("linkInstagram");

                entity.Property(e => e.LinkSkype).HasColumnName("linkSkype");

                entity.Property(e => e.LinkTwitter).HasColumnName("linkTwitter");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.PhoneBusiness).HasColumnName("phoneBusiness");

                entity.Property(e => e.Slogan).HasColumnName("slogan");

                entity.Property(e => e.Subslogan).HasColumnName("subslogan");

                entity.Property(e => e.TenCongTy).HasColumnName("tenCongTy");

                entity.Property(e => e.TongDai).HasColumnName("tongDai");
            });

            modelBuilder.Entity<QuangCao>(entity =>
            {
                entity.HasKey(e => e.MaQuangCao)
                    .HasName("PK__QuangCao__0E388B24EDF4E706");

                entity.ToTable("QuangCao");

                entity.Property(e => e.MaQuangCao)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maQuangCao");

                entity.Property(e => e.DiaDiem).HasColumnName("diaDiem");

                entity.Property(e => e.Email)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.GiaCa)
                    .IsRequired()
                    .HasColumnName("giaCa");

                entity.Property(e => e.MaNganhHang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNganhHang");

                entity.Property(e => e.MaNsx)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maNSX");

                entity.Property(e => e.MaTrangThai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTrangThai");

                entity.Property(e => e.MoTaQuangCao).HasColumnName("moTaQuangCao");

                entity.Property(e => e.NgayDang)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayDang");

                entity.Property(e => e.NgayHetHan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayHetHan");

                entity.Property(e => e.NgayNhan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayNhan");

                entity.Property(e => e.NguoiDeNghi).HasColumnName("nguoiDeNghi");

                entity.Property(e => e.Sdt)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenQuangCao)
                    .IsRequired()
                    .HasColumnName("tenQuangCao");

                entity.HasOne(d => d.MaNganhHangNavigation)
                    .WithMany(p => p.QuangCaos)
                    .HasForeignKey(d => d.MaNganhHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuangCao__maNgan__71D1E811");

                entity.HasOne(d => d.MaNsxNavigation)
                    .WithMany(p => p.QuangCaos)
                    .HasForeignKey(d => d.MaNsx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuangCao__maNSX__72C60C4A");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.QuangCaos)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuangCao__maTran__73BA3083");
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen)
                    .HasName("PK__Quyen__97001DA3B3B130E3");

                entity.ToTable("Quyen");

                entity.Property(e => e.MaQuyen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maQuyen");

                entity.Property(e => e.MoTa)
                    .HasMaxLength(100)
                    .HasColumnName("moTa");

                entity.Property(e => e.TenQuyen)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenQuyen");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham)
                    .HasName("PK__SanPham__5B439C4370128EAB");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSanPham)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maSanPham");

                entity.Property(e => e.Anh)
                    .IsUnicode(false)
                    .HasColumnName("anh");

                entity.Property(e => e.Link)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao");

                entity.Property(e => e.TenSanPham)
                    .IsRequired()
                    .HasColumnName("tenSanPham");
            });

            modelBuilder.Entity<TaiLieu>(entity =>
            {
                entity.HasKey(e => e.MaTaiLieu)
                    .HasName("PK__TaiLieu__C11CD9C066A3301A");

                entity.ToTable("TaiLieu");

                entity.Property(e => e.MaTaiLieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTaiLieu");

                entity.Property(e => e.DownLoad).HasColumnName("downLoad");

                entity.Property(e => e.DuongDan)
                    .IsRequired()
                    .HasColumnName("duongDan");

                entity.Property(e => e.LyDoTraVe).HasColumnName("lyDoTraVe");

                entity.Property(e => e.MaLoaiTaiLieu)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoaiTaiLieu");

                entity.Property(e => e.MaSanPham)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maSanPham");

                entity.Property(e => e.MaTrangThai)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTrangThai");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.NgayThang)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayThang");

                entity.Property(e => e.TenFile)
                    .IsRequired()
                    .HasColumnName("tenFile");

                entity.Property(e => e.TenTaiLieu)
                    .IsRequired()
                    .HasColumnName("tenTaiLieu");

                entity.HasOne(d => d.MaLoaiTaiLieuNavigation)
                    .WithMany(p => p.TaiLieus)
                    .HasForeignKey(d => d.MaLoaiTaiLieu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaiLieu__maLoaiT__6383C8BA");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.TaiLieus)
                    .HasForeignKey(d => d.MaSanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaiLieu__maSanPh__6477ECF3");

                entity.HasOne(d => d.MaTrangThaiNavigation)
                    .WithMany(p => p.TaiLieus)
                    .HasForeignKey(d => d.MaTrangThai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaiLieu__maTrang__656C112C");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.ToTable("ThongBao");

                entity.Property(e => e.Check).HasColumnName("check");

                entity.Property(e => e.MaKhachHang)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.MaLoaiThongBao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maLoaiThongBao");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("date")
                    .HasColumnName("thoiGian");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBao__maKhac__778AC167");

                entity.HasOne(d => d.MaLoaiThongBaoNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaLoaiThongBao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBao__maLoai__787EE5A0");
            });

            modelBuilder.Entity<TrangThai>(entity =>
            {
                entity.HasKey(e => e.MaTrangThai)
                    .HasName("PK__TrangTha__AD42D17940CBCDB0");

                entity.ToTable("TrangThai");

                entity.Property(e => e.MaTrangThai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maTrangThai");

                entity.Property(e => e.MoTa).HasColumnName("moTa");

                entity.Property(e => e.TenTrangThai)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenTrangThai");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
