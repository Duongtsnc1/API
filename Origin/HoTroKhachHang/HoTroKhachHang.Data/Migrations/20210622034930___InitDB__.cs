using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HoTroKhachHang.Data.Migrations
{
    public partial class __InitDB__ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucDanh",
                columns: table => new
                {
                    maChucDanh = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenChucDanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucDanh__2284EAF37681A2D3", x => x.maChucDanh);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    maKhachHang = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CCCD = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    anh = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    coQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngayTuyen = table.Column<DateTime>(type: "date", nullable: true),
                    chucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    trinhDo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    linhVuc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ngaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    userName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    passwd = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__0CCB3D49818C18C3", x => x.maKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "LoaiHoiDap",
                columns: table => new
                {
                    maLoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiHoiD__E5A6B2284A5ED66A", x => x.maLoai);
                });

            migrationBuilder.CreateTable(
                name: "LoaiTaiLieu",
                columns: table => new
                {
                    maLoaiTaiLieu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenLoaiTaiLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiTaiL__52492BD98ABD6B31", x => x.maLoaiTaiLieu);
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    maPhongBan = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenPhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhongBan__3A946B0864E6FA53", x => x.maPhongBan);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    maQuyen = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quyen__97001DA3580159C9", x => x.maQuyen);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    maSanPham = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngayTao = table.Column<DateTime>(type: "date", nullable: true),
                    anh = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__5B439C43B2F7C944", x => x.maSanPham);
                });

            migrationBuilder.CreateTable(
                name: "TrangThai",
                columns: table => new
                {
                    maTrangThai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenTrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__AD42D179B08221B9", x => x.maTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    maNhanVien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CCCD = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    anh = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    trinhDo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ngayTuyenDung = table.Column<DateTime>(type: "date", nullable: true),
                    maChucDanh = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    maPhongBan = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    userName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    passwd = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__BDDEF20DF3B327EA", x => x.maNhanVien);
                    table.ForeignKey(
                        name: "FK__NhanVien__maChuc__44FF419A",
                        column: x => x.maChucDanh,
                        principalTable: "ChucDanh",
                        principalColumn: "maChucDanh",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NhanVien__maPhon__45F365D3",
                        column: x => x.maPhongBan,
                        principalTable: "PhongBan",
                        principalColumn: "maPhongBan",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KeyLicense",
                columns: table => new
                {
                    maKhachHang = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maSanPham = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ngayBD = table.Column<DateTime>(type: "date", nullable: false),
                    ngayKT = table.Column<DateTime>(type: "date", nullable: false),
                    giaHan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KeyLicen__297F048D804237CE", x => new { x.maKhachHang, x.maSanPham });
                    table.ForeignKey(
                        name: "FK__KeyLicens__maKha__4316F928",
                        column: x => x.maKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "maKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__KeyLicens__maSan__440B1D61",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoiDap",
                columns: table => new
                {
                    maHoiDap = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ndHoiDap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maKhachHang = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ndTraLoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maTrangThai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    congKhai = table.Column<bool>(type: "bit", nullable: false),
                    maSanPham = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maLoai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    traVeDuyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    traVeXuatBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ndHoiDapEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngayNhan = table.Column<DateTime>(type: "date", nullable: false),
                    ngayXuatBan = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoiDap__A6533CEA5ECAC499", x => x.maHoiDap);
                    table.ForeignKey(
                        name: "FK__HoiDap__maKhachH__3F466844",
                        column: x => x.maKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "maKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__HoiDap__maLoai__403A8C7D",
                        column: x => x.maLoai,
                        principalTable: "LoaiHoiDap",
                        principalColumn: "maLoai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__HoiDap__maSanPha__412EB0B6",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__HoiDap__maTrangT__4222D4EF",
                        column: x => x.maTrangThai,
                        principalTable: "TrangThai",
                        principalColumn: "maTrangThai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaiLieu",
                columns: table => new
                {
                    maTaiLieu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    tenTaiLieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tenFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duongDan = table.Column<string>(type: "nvarchar(max)", unicode: false, nullable: false),
                    downLoad = table.Column<bool>(type: "bit", nullable: false),
                    ngayDang = table.Column<DateTime>(type: "date", nullable: false),
                    tieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maLoaiTaiLieu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maTrangThai = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maSanPham = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaiLieu__C11CD9C006E2E13D", x => x.maTaiLieu);
                    table.ForeignKey(
                        name: "FK__TaiLieu__maLoaiT__4E88ABD4",
                        column: x => x.maLoaiTaiLieu,
                        principalTable: "LoaiTaiLieu",
                        principalColumn: "maLoaiTaiLieu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TaiLieu__maSanPh__4CA06362",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TaiLieu__maTrang__4D94879B",
                        column: x => x.maTrangThai,
                        principalTable: "TrangThai",
                        principalColumn: "maTrangThai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NV_Quyen",
                columns: table => new
                {
                    maNhanVien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maQuyen = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NV_Quyen__94AEF3D7ACB0C455", x => new { x.maNhanVien, x.maQuyen });
                    table.ForeignKey(
                        name: "FK__NV_Quyen__maNhan__48CFD27E",
                        column: x => x.maNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "maNhanVien",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NV_Quyen__maQuye__49C3F6B7",
                        column: x => x.maQuyen,
                        principalTable: "Quyen",
                        principalColumn: "maQuyen",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NV_HoiDap",
                columns: table => new
                {
                    maNhanVien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maHoiDap = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    noiDungThucHien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayThang = table.Column<DateTime>(type: "date", nullable: true),
                    quyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NV_HoiDa__07BBC1C32854A8A6", x => new { x.maNhanVien, x.maHoiDap });
                    table.ForeignKey(
                        name: "FK__NV_HoiDap__maHoi__46E78A0C",
                        column: x => x.maHoiDap,
                        principalTable: "HoiDap",
                        principalColumn: "maHoiDap",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NV_HoiDap__maNha__47DBAE45",
                        column: x => x.maNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "maNhanVien",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NV_TaiLieu",
                columns: table => new
                {
                    maNhanVien = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    maTaiLieu = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    noiDungThucHien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayThang = table.Column<DateTime>(type: "date", nullable: false),
                    quyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NV_TaiLi__A1CF3F912F332BED", x => new { x.maNhanVien, x.maTaiLieu });
                    table.ForeignKey(
                        name: "FK__NV_TaiLie__maNha__4AB81AF0",
                        column: x => x.maNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "maNhanVien",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__NV_TaiLie__maTai__4BAC3F29",
                        column: x => x.maTaiLieu,
                        principalTable: "TaiLieu",
                        principalColumn: "maTaiLieu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_maKhachHang",
                table: "HoiDap",
                column: "maKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_maLoai",
                table: "HoiDap",
                column: "maLoai");

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_maSanPham",
                table: "HoiDap",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_HoiDap_maTrangThai",
                table: "HoiDap",
                column: "maTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_KeyLicense_maSanPham",
                table: "KeyLicense",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_maChucDanh",
                table: "NhanVien",
                column: "maChucDanh");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_maPhongBan",
                table: "NhanVien",
                column: "maPhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_NV_HoiDap_maHoiDap",
                table: "NV_HoiDap",
                column: "maHoiDap");

            migrationBuilder.CreateIndex(
                name: "IX_NV_Quyen_maQuyen",
                table: "NV_Quyen",
                column: "maQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_NV_TaiLieu_maTaiLieu",
                table: "NV_TaiLieu",
                column: "maTaiLieu");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieu_maLoaiTaiLieu",
                table: "TaiLieu",
                column: "maLoaiTaiLieu");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieu_maSanPham",
                table: "TaiLieu",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieu_maTrangThai",
                table: "TaiLieu",
                column: "maTrangThai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyLicense");

            migrationBuilder.DropTable(
                name: "NV_HoiDap");

            migrationBuilder.DropTable(
                name: "NV_Quyen");

            migrationBuilder.DropTable(
                name: "NV_TaiLieu");

            migrationBuilder.DropTable(
                name: "HoiDap");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "TaiLieu");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "LoaiHoiDap");

            migrationBuilder.DropTable(
                name: "ChucDanh");

            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.DropTable(
                name: "LoaiTaiLieu");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "TrangThai");
        }
    }
}
