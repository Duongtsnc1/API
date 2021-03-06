USE [CSKH]
GO
/****** Object:  User [IIS APPPOOL\SupportCustomer]    Script Date: 7/13/2021 9:53:30 PM ******/
CREATE USER [IIS APPPOOL\SupportCustomer] FOR LOGIN [IIS APPPOOL\supportcustomer] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [IIS APPPOOL\SupportCustomer]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\SupportCustomer]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\SupportCustomer]
GO
/****** Object:  Table [dbo].[BaiDang]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaiDang](
	[maBaiDang] [varchar](20) NOT NULL,
	[tieuDe] [nvarchar](max) NOT NULL,
	[noiDung] [nvarchar](max) NOT NULL,
	[ngayDang] [date] NOT NULL,
	[lanBinhLuanCuoi] [date] NULL,
	[maNguoiBinhLuanCuoi] [varchar](20) NULL,
	[maChuDe] [varchar](20) NOT NULL,
	[soLuongBinhLuan] [int] NULL,
	[hot] [bit] NULL,
	[maKhachHang] [varchar](20) NULL,
	[soLuongXem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[maBaiDang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[tenBanner] [nvarchar](max) NOT NULL,
	[linkAnh] [nvarchar](max) NOT NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BinhLuan]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BinhLuan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ngayTao] [date] NOT NULL,
	[noiDung] [nvarchar](max) NOT NULL,
	[maKhachHang] [varchar](20) NULL,
	[maBaiDang] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucDanh]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucDanh](
	[maChucDanh] [varchar](20) NOT NULL,
	[tenChucDanh] [nvarchar](100) NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maChucDanh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuDe]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuDe](
	[maChuDe] [varchar](20) NOT NULL,
	[ngayTao] [date] NOT NULL,
	[moTa] [nvarchar](max) NULL,
	[tenChuDe] [nvarchar](max) NULL,
	[maNhomChuDe] [varchar](20) NULL,
	[trangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[maChuDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoiDap]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoiDap](
	[maHoiDap] [varchar](20) NOT NULL,
	[tieuDe] [nvarchar](max) NOT NULL,
	[ndHoiDap] [nvarchar](max) NOT NULL,
	[maKhachHang] [varchar](20) NOT NULL,
	[ndTraLoi] [nvarchar](max) NULL,
	[maTrangThai] [varchar](20) NOT NULL,
	[congKhai] [bit] NULL,
	[maSanPham] [varchar](20) NOT NULL,
	[maLoai] [varchar](20) NOT NULL,
	[traVeDuyet] [nvarchar](max) NULL,
	[traVeXuatBan] [nvarchar](max) NULL,
	[ndHoiDapEdit] [nvarchar](max) NULL,
	[ngayNhan] [date] NOT NULL,
	[ngayXuatBan] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[maHoiDap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KeyLicense]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KeyLicense](
	[maKhachHang] [varchar](20) NOT NULL,
	[maSanPham] [varchar](20) NOT NULL,
	[ngayBD] [date] NOT NULL,
	[ngayKT] [date] NOT NULL,
	[giaHan] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maKhachHang] ASC,
	[maSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[maKhachHang] [varchar](20) NOT NULL,
	[ho] [nvarchar](100) NOT NULL,
	[ten] [nvarchar](100) NOT NULL,
	[gioiTinh] [nvarchar](10) NOT NULL,
	[SDT] [varchar](10) NOT NULL,
	[email] [varchar](200) NOT NULL,
	[CCCD] [varchar](20) NOT NULL,
	[anh] [varchar](max) NULL,
	[coQuan] [nvarchar](max) NULL,
	[ngayTuyen] [date] NULL,
	[chucVu] [nvarchar](100) NULL,
	[trinhDo] [nvarchar](200) NULL,
	[linhVuc] [nvarchar](200) NULL,
	[ngaySinh] [date] NULL,
	[userName] [varchar](100) NOT NULL,
	[passwd] [varchar](100) NOT NULL,
	[trangThai] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiHoiDap]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHoiDap](
	[maLoai] [varchar](20) NOT NULL,
	[tenLoai] [nvarchar](50) NOT NULL,
	[trangThai] [bit] NOT NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiTaiLieu]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiTaiLieu](
	[maLoaiTaiLieu] [varchar](20) NOT NULL,
	[tenLoaiTaiLieu] [nvarchar](100) NOT NULL,
	[trangThai] [bit] NOT NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maLoaiTaiLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiThongBao]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiThongBao](
	[maLoaiThongBao] [varchar](20) NOT NULL,
	[tenLoaiThongBao] [nvarchar](max) NOT NULL,
	[moTa] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[maLoaiThongBao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[maNhanVien] [varchar](20) NOT NULL,
	[ho] [nvarchar](50) NOT NULL,
	[ten] [nvarchar](50) NOT NULL,
	[gioiTinh] [nvarchar](5) NOT NULL,
	[SDT] [varchar](10) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[CCCD] [varchar](10) NOT NULL,
	[anh] [varchar](100) NOT NULL,
	[diaChi] [nvarchar](200) NOT NULL,
	[ngaySinh] [date] NOT NULL,
	[trinhDo] [nvarchar](100) NULL,
	[ngayTuyenDung] [date] NULL,
	[maChucDanh] [varchar](20) NULL,
	[maPhongBan] [varchar](20) NULL,
	[userName] [varchar](100) NOT NULL,
	[passwd] [varchar](100) NOT NULL,
	[trangThai] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomChuDe]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomChuDe](
	[maNhomChuDe] [varchar](20) NOT NULL,
	[tenNhomChuDe] [nvarchar](max) NOT NULL,
	[moTa] [varchar](max) NULL,
	[ngayTao] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhomChuDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NV_HoiDap]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NV_HoiDap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[maNhanVien] [varchar](20) NOT NULL,
	[maHoiDap] [varchar](20) NOT NULL,
	[noiDungThucHien] [nvarchar](max) NOT NULL,
	[ngayThang] [date] NULL,
	[quyen] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[maNhanVien] ASC,
	[maHoiDap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NV_Quyen]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NV_Quyen](
	[maNhanVien] [varchar](20) NOT NULL,
	[maQuyen] [varchar](20) NOT NULL,
	[trangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhanVien] ASC,
	[maQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NV_TaiLieu]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NV_TaiLieu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[maNhanVien] [varchar](20) NOT NULL,
	[maTaiLieu] [varchar](20) NOT NULL,
	[noiDungThucHien] [nvarchar](max) NOT NULL,
	[ngayThang] [datetime] NOT NULL,
	[quyen] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__NV_TaiLi__38081FFE1BE2100C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[maNhanVien] ASC,
	[maTaiLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[maPhongBan] [varchar](20) NOT NULL,
	[tenPhongBan] [nvarchar](100) NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maPhongBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QLThongTin]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QLThongTin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[tenCongTy] [nvarchar](max) NULL,
	[logo] [nvarchar](max) NULL,
	[slogan] [nvarchar](max) NULL,
	[subslogan] [nvarchar](max) NULL,
	[linkFaceBook] [varchar](max) NULL,
	[linkSkype] [nvarchar](max) NULL,
	[linkTwitter] [nvarchar](max) NULL,
	[linkInstagram] [nvarchar](max) NULL,
	[linkBanDo] [nvarchar](max) NULL,
	[email] [nvarchar](max) NULL,
	[phone] [nvarchar](max) NULL,
	[address] [nvarchar](max) NULL,
	[phoneBusiness] [nvarchar](max) NULL,
	[tongDai] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quyen]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quyen](
	[maQuyen] [varchar](20) NOT NULL,
	[tenQuyen] [nvarchar](50) NOT NULL,
	[moTa] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[maQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[maSanPham] [varchar](20) NOT NULL,
	[tenSanPham] [nvarchar](max) NOT NULL,
	[moTa] [nvarchar](max) NULL,
	[ngayTao] [date] NULL,
	[anh] [varchar](100) NULL,
	[link] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiLieu]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiLieu](
	[maTaiLieu] [varchar](20) NOT NULL,
	[tenTaiLieu] [nvarchar](max) NOT NULL,
	[moTa] [nvarchar](max) NULL,
	[tenFile] [nvarchar](max) NOT NULL,
	[duongDan] [nvarchar](max) NOT NULL,
	[downLoad] [bit] NOT NULL,
	[ngayThang] [datetime] NULL,
	[lyDoTraVe] [nvarchar](max) NULL,
	[maLoaiTaiLieu] [varchar](20) NOT NULL,
	[maTrangThai] [varchar](20) NOT NULL,
	[maSanPham] [varchar](20) NOT NULL,
 CONSTRAINT [PK__TaiLieu__C11CD9C066A3301A] PRIMARY KEY CLUSTERED 
(
	[maTaiLieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[maLoaiThongBao] [varchar](20) NOT NULL,
	[maKhachHang] [varchar](20) NOT NULL,
	[thoiGian] [date] NULL,
	[check] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrangThai]    Script Date: 7/13/2021 9:53:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrangThai](
	[maTrangThai] [varchar](20) NOT NULL,
	[tenTrangThai] [nvarchar](100) NOT NULL,
	[moTa] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[maTrangThai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[BaiDang] ([maBaiDang], [tieuDe], [noiDung], [ngayDang], [lanBinhLuanCuoi], [maNguoiBinhLuanCuoi], [maChuDe], [soLuongBinhLuan], [hot], [maKhachHang], [soLuongXem]) VALUES (N'BD0_00000001', N'Phiên bản 2.0 A bạn đã thử chưa ', N'Thực sự nó hỗ trợ rất nhiều ', CAST(N'2021-01-11' AS Date), CAST(N'2021-07-11' AS Date), N'KH002', N'CD0_00000001', 1, 0, N'KH001', 10)
INSERT [dbo].[BaiDang] ([maBaiDang], [tieuDe], [noiDung], [ngayDang], [lanBinhLuanCuoi], [maNguoiBinhLuanCuoi], [maChuDe], [soLuongBinhLuan], [hot], [maKhachHang], [soLuongXem]) VALUES (N'BD0_00000002', N'Lỗi XyZ quá nhiêu ', N'tôi đã gặp nhiêu lỗi quá ', CAST(N'2021-01-11' AS Date), CAST(N'2021-07-11' AS Date), N'KH002', N'CD0_00000001', 1, 1, N'KH001', 15)
INSERT [dbo].[BaiDang] ([maBaiDang], [tieuDe], [noiDung], [ngayDang], [lanBinhLuanCuoi], [maNguoiBinhLuanCuoi], [maChuDe], [soLuongBinhLuan], [hot], [maKhachHang], [soLuongXem]) VALUES (N'BD0_00000003', N'tesst quá', N'aaaa', CAST(N'2021-01-11' AS Date), CAST(N'2021-07-11' AS Date), N'KH001', N'CD0_00000002', 2, 0, N'KH002', 10)
GO
SET IDENTITY_INSERT [dbo].[Banner] ON 

INSERT [dbo].[Banner] ([Id], [tenBanner], [linkAnh], [moTa]) VALUES (18, N'Ảnh 1', N'/images/anh1_14d4.png', N'Công nghệ tiên phong - tính năng vượt trội - hiệu quả hàng đầu')
INSERT [dbo].[Banner] ([Id], [tenBanner], [linkAnh], [moTa]) VALUES (19, N'Công nghệ tiên phong', N'/images/anh2_c4e2.jpg', N'Công nghệ tiên phong')
INSERT [dbo].[Banner] ([Id], [tenBanner], [linkAnh], [moTa]) VALUES (20, N'Ảnh 3', N'/images/anh3_b009.jpg', N'Tính năng vượt trội')
INSERT [dbo].[Banner] ([Id], [tenBanner], [linkAnh], [moTa]) VALUES (21, N'Ảnh 4', N'/images/anh4_5740.jpg', N'Hiệu quả hàng đầu')
INSERT [dbo].[Banner] ([Id], [tenBanner], [linkAnh], [moTa]) VALUES (22, N'test', N'/images/slide2_7449_84e8.jpg', N'test')
SET IDENTITY_INSERT [dbo].[Banner] OFF
GO
SET IDENTITY_INSERT [dbo].[BinhLuan] ON 

INSERT [dbo].[BinhLuan] ([Id], [ngayTao], [noiDung], [maKhachHang], [maBaiDang]) VALUES (1, CAST(N'2021-01-11' AS Date), N'Nó thự sự tốt ', N'KH002', N'BD0_00000001')
INSERT [dbo].[BinhLuan] ([Id], [ngayTao], [noiDung], [maKhachHang], [maBaiDang]) VALUES (2, CAST(N'2021-01-11' AS Date), N'Tôi đã sửa được lỗi này ', N'KH002', N'BD0_00000002')
INSERT [dbo].[BinhLuan] ([Id], [ngayTao], [noiDung], [maKhachHang], [maBaiDang]) VALUES (3, CAST(N'2021-01-11' AS Date), N'Toi da dung no', N'KH001', N'BD0_00000002')
INSERT [dbo].[BinhLuan] ([Id], [ngayTao], [noiDung], [maKhachHang], [maBaiDang]) VALUES (4, CAST(N'2021-01-11' AS Date), N'aaaaaa', N'KH003', N'BD0_00000002')
SET IDENTITY_INSERT [dbo].[BinhLuan] OFF
GO
INSERT [dbo].[ChucDanh] ([maChucDanh], [tenChucDanh], [moTa]) VALUES (N'CD001', N'Trưởng phòng', NULL)
INSERT [dbo].[ChucDanh] ([maChucDanh], [tenChucDanh], [moTa]) VALUES (N'CD002', N'Phó phòng', NULL)
GO
INSERT [dbo].[ChuDe] ([maChuDe], [ngayTao], [moTa], [tenChuDe], [maNhomChuDe], [trangThai]) VALUES (N'CD0_00000001', CAST(N'2021-06-13' AS Date), NULL, N'Cách cài đặt sản phẩm A', N'ND0_00000001', 1)
INSERT [dbo].[ChuDe] ([maChuDe], [ngayTao], [moTa], [tenChuDe], [maNhomChuDe], [trangThai]) VALUES (N'CD0_00000002', CAST(N'2021-06-13' AS Date), NULL, N'Cách cài đặt sản phẩm B', N'ND0_00000002', 1)
INSERT [dbo].[ChuDe] ([maChuDe], [ngayTao], [moTa], [tenChuDe], [maNhomChuDe], [trangThai]) VALUES (N'CD0_00000003', CAST(N'2021-06-13' AS Date), NULL, N'Lỗi của sản phẩm A', N'ND0_00000001', 1)
INSERT [dbo].[ChuDe] ([maChuDe], [ngayTao], [moTa], [tenChuDe], [maNhomChuDe], [trangThai]) VALUES (N'CD0_00000004', CAST(N'2021-06-13' AS Date), NULL, N'Lỗi XYZ thông dụng', N'ND0_00000001', 0)
GO
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000001', N'this is tieu de', N'Máy báo nhiêu tiền', N'KH001', N'rqwrerjqwiorewjqrioewqjrioewjqoirje', N'TT005', NULL, N'SP001', N'LHD001', NULL, NULL, N'Nội dung hỏi đáp chỉnh sửa', CAST(N'2021-06-11' AS Date), CAST(N'2021-05-20' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000002', N'day la tieu de', N'Máy sửa có dễ không ak', N'KH002', N'qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa', N'TT005', NULL, N'SP001', N'LHD001', NULL, NULL, N'Nội dung hỏi đáp chỉnh sửa', CAST(N'2021-06-11' AS Date), CAST(N'2021-05-25' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000003', N'tieu de', N'Pin máy tính xài được trong bao lâu ', N'KH003', N'qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa', N'TT005', NULL, N'SP001', N'LHD001', NULL, NULL, N'Nội dung hỏi đáp chỉnh sửa', CAST(N'2021-05-15' AS Date), CAST(N'2021-06-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000004', N'tieu de', N'cách fix bug ABC như nào vậy ', N'KH002', NULL, N'TT003', NULL, N'SP001', N'LHD004', NULL, NULL, NULL, CAST(N'2021-06-13' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000005', N'tieu de', N'Cách cài đặt ABC cho máy tính Dell', N'KH003', NULL, N'TT002', NULL, N'SP001', N'LHD001', NULL, NULL, NULL, CAST(N'2021-05-11' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000006', N'tieu de', N'ho11a3ng21132', N'KH003', NULL, N'TT002', NULL, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2020-10-22' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000007', N'tieu de', N'aho11a3ng21132', N'KH003', NULL, N'TT002', NULL, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2020-10-22' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000008', N'tieu de', N'aho1s1a3ng21132', N'KH003', NULL, N'TT002', NULL, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2020-10-22' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000009', N'tieu de', N'aho1s1a3ng21132', N'KH003', NULL, N'TT002', NULL, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2020-10-22' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000010', N'tieu de', N'aho1s1a3ng21132', N'KH003', NULL, N'TT002', NULL, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2020-10-22' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000011', N'tieu de', N'aho1s1a3ng21132', N'KH003', NULL, N'TT005', 1, N'SP003', N'LHD002', N'', N'', N'', CAST(N'2020-10-22' AS Date), CAST(N'2021-07-09' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000012', N'tieu de', N'Tôi không thể cài đặt được, lỗi thiếu .net 4.6', N'KH001', N'Bạn cài thêm .net 4.6', N'TT005', 1, N'SP001', N'LHD001', N'', N'', N'Câu hỏi Tôi không thể cài đặt được, lỗi thiếu .net 4.6', CAST(N'2021-07-04' AS Date), CAST(N'2021-07-04' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000013', N'tieu de', N'câu hỏi test cho phần mềm 2', N'KH001', NULL, N'TT001', NULL, N'SP002', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000014', N'tieu de', N'test sử dụng', N'KH001', NULL, N'TT001', NULL, N'SP001', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000015', N'tieu de', N'test nhẹ', N'KH001', NULL, N'TT001', NULL, N'SP001', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-03' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000016', N'tieu de', N'Lỗi này là lỗi gì?', N'KH001', NULL, N'TT001', NULL, N'SP001', N'LHD003', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000017', N'tieu de', N'Tôi hông biết cài đặt!', N'KH001', NULL, N'TT001', NULL, N'SP003', N'LHD001', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000018', N'tieu de', N'Thử lỗi này xem sao!', N'KH001', NULL, N'TT001', NULL, N'SP004', N'LHD003', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000019', N'tieu de', N'Tôi không cài được phần mềm, mong được trợ giúp ', N'KH001', NULL, N'TT001', NULL, N'SP001', N'LHD001', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000020', N'tieu de', N'Tôi không thêm mới được các hợp đồng tiền gửi có kỳ hạn tại ngân hàng và không biết thêm ở đâu? Mong công ty trợ giúp ', N'KH001', NULL, N'TT001', NULL, N'SP006', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-04' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000021', N'tieu de', N'bui thanh hoang test', N'KH001', N'da tra loi', N'TT005', 1, N'SP001', N'LHD001', NULL, N'abcasdasd', N'bui than hhoang', CAST(N'2020-10-22' AS Date), CAST(N'2021-11-20' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000022', N'tiêu đề dài để test định dạng ô', N'Nội dung hỏi đáp', N'KH001', N'<p><strong>Qu&aacute; trời đơn sơ</strong></p>
', N'TT005', 1, N'SP001', N'LHD001', N'', N'', N'Câu hỏi test đơn sơ.', CAST(N'2021-07-04' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000023', N'lỗi khi sử dụng ', N'lỗi quá nhiều ', N'KH002', NULL, N'TT001', 1, N'SP001', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000024', N'cách thức sử dụng ', N'mục A rất khó sử dụng ', N'KH002', NULL, N'TT001', 1, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000025', N'cách thức sử dụng ', N'mục A rất khó sử dụng ', N'KH002', NULL, N'TT001', 1, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000026', N'lỗi khi sử dụng ', N'lỗi ABC thường xuyên xuất hiện ', N'KH002', NULL, N'TT001', 1, N'SP002', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000027', N'Hướng dẫn cài mục A', N'cài đặt update A như nào v', N'KH002', N'<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5">Căn cứ Th&ocirc;ng tư số 111/2013/TT-BTC ng&agrave;y 15/8/2013 của Bộ trưởng Bộ T&agrave;i ch&iacute;nh hướng dẫn về thuế TNCN;</span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5">- Tại Khoản 2 Điều 2 quy định thu nhập từ tiền lương, tiền c&ocirc;ng: </span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&ldquo;Thu nhập từ tiền lương, tiền c&ocirc;ng l&agrave; thu nhập người lao động nhận được từ người sử dụng lao động, bao gồm:</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>a) Tiền lương, tiền c&ocirc;ng v&agrave; c&aacute;c khoản c&oacute; t&iacute;nh chất tiền lương, tiền c&ocirc;ng dưới c&aacute;c h&igrave;nh thức bằng tiền hoặc kh&ocirc;ng bằng tiền.</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&nbsp;...</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>g) Kh&ocirc;ng t&iacute;nh v&agrave;o thu nhập chịu thuế đối với c&aacute;c khoản sau:</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&hellip;</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>g.5) Khoản tiền ăn giữa ca, ăn trưa do người sử dụng lao động tổ chức bữa ăn giữa ca, ăn trưa cho người lao động dưới c&aacute;c h&igrave;nh thức như trực tiếp nấu ăn, mua suất ăn, cấp phiếu ăn.</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>Trường hợp người sử dụng lao động kh&ocirc;ng tổ chức bữa ăn giữa ca, ăn trưa m&agrave; chi tiền cho người lao động th&igrave; kh&ocirc;ng t&iacute;nh v&agrave;o thu nhập chịu thuế của c&aacute; nh&acirc;n, nếu mức chi ph&ugrave; hợp với hướng dẫn của Bộ Lao động - Thương binh v&agrave; X&atilde; hội. Trường hợp mức chi cao hơn mức hướng dẫn của Bộ Lao động - Thương binh v&agrave; X&atilde; hội th&igrave; phần chi vượt mức phải t&iacute;nh v&agrave;o thu nhập chịu thuế của c&aacute; nh&acirc;n.</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>Mức chi cụ thể &aacute;p dụng đối với doanh nghiệp Nh&agrave; nước v&agrave; c&aacute;c tổ chức, đơn vị thuộc cơ quan h&agrave;nh ch&iacute;nh sự nghiệp, Đảng, Đo&agrave;n thể, c&aacute;c Hội kh&ocirc;ng qu&aacute; mức hướng dẫn của Bộ Lao động - Thương binh v&agrave; X&atilde; hội. Đối với c&aacute;c doanh nghiệp ngo&agrave;i Nh&agrave; nước v&agrave; c&aacute;c tổ chức kh&aacute;c, mức chi do thủ trưởng đơn vị thống nhất với chủ tịch c&ocirc;ng đo&agrave;n quyết định nhưng tối đa kh&ocirc;ng vượt qu&aacute; mức &aacute;p dụng đối với doanh nghiệp Nh&agrave; nước.&rdquo;;</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5">- Tại Điều 7 quy định căn cứ t&iacute;nh thuế đối với thu nhập chịu thuế từ kinh doanh, từ tiền lương, tiền c&ocirc;ng:</span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&ldquo;Căn cứ t&iacute;nh thuế đối với thu nhập từ kinh doanh v&agrave; thu nhập từ tiền lương, tiền c&ocirc;ng l&agrave; thu nhập t&iacute;nh thuế v&agrave; thuế suất, cụ thể như sau: </em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>1. Thu nhập t&iacute;nh thuế được x&aacute;c định bằng thu nhập chịu thuế theo hướng dẫn tại Điều 8 Th&ocirc;ng tư n&agrave;y trừ (-) c&aacute;c khoản giảm trừ sau:</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>a) C&aacute;c khoản giảm trừ gia cảnh theo hướng dẫn tại khoản 1, Điều 9 Th&ocirc;ng tư n&agrave;y. </em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>b) C&aacute;c khoản đ&oacute;ng bảo hiểm, quỹ hưu tr&iacute; tự nguyện theo hướng dẫn tại khoản 2, Điều 9 Th&ocirc;ng tư n&agrave;y.</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>c) C&aacute;c khoản đ&oacute;ng g&oacute;p từ thiện, nh&acirc;n đạo, khuyến học theo hướng dẫn tại khoản 3, Điều 9 Th&ocirc;ng tư n&agrave;y. </em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>...&rdquo;;</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5">- Tại Khoản 2 Điều 8 quy định x&aacute;c định thu nhập chịu thuế từ tiền lương, tiền c&ocirc;ng: </span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&ldquo;a) Thu nhập chịu thuế từ tiền lương, tiền c&ocirc;ng được x&aacute;c định bằng tổng số tiền lương, tiền c&ocirc;ng, tiền th&ugrave; lao, c&aacute;c khoản thu nhập kh&aacute;c c&oacute; t&iacute;nh chất tiền lương, tiền c&ocirc;ng m&agrave; người nộp thuế nhận được trong kỳ t&iacute;nh thuế theo hướng dẫn tại khoản 2, Điều 2 Th&ocirc;ng tư n&agrave;y.</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&nbsp;...&rdquo;;</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5">- Tại Điều 25 quy định khấu trừ thuế v&agrave; chứng từ khấu trừ thuế:</span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>&ldquo;1. Khấu trừ thuế ...</em></span></span></p>

<p><span id="T:oc_8978575726region1:pgl1"><span id="T:oc_8978575726region1:pgl5"><em>i) Khấu trừ thuế đối với một số trường hợp kh&aacute;c</em></span></span></p>
', N'TT005', 0, N'SP003', N'LHD001', N'', N'', N'Kính gửi Quý Bộ Tài Chính, Trong năm 2021, công ty chúng tôi có chi trả các khoản chi không chịu thuế thu nhập cá nhân (TNCN) như: chi hiếu hỷ, tăng ca không chịu thuế, tiền cơm dưới 730,000 VND cho người lao động (NLĐ) đang trong thời gian thử việc 2 tháng. Công ty chúng tôi xin hỏi : (1) Thu nhập để tính vào thu nhập chịu thuế của NLĐ trong thời gian thử việc 2 tháng có bao gồm các khoản chi trên không. (2) Trường hợp trong thời gian thử việc nếu thu nhập chịu thuế của NLĐ bao gồm tổng các khoản chi trên và tính thuế suất 10%. Sau đó NLĐ ký tiếp hợp đồng lao động trên 3 tháng, được áp dụng tính thuế TNCN theo biểu thuế lũy tiến từng phần. Thì khi quyết toán thuế TNCN năm 2021, NLĐ trên có được trừ các khoản thu nhập không chịu thuế TNCN đã tính thuế 10% trong thời gian thử việc không ? ( hiếu hỷ, tăng ca không chịu thuế, tiền cơm dưới 730,000 VND ). Kính mong Quý Bộ hướng dẫn để Công ty thực hiện đúng quy định. Xin trân trọng cám ơn! ', CAST(N'2021-07-06' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000028', N'Lỗi khi sử dụng sản phẩm ', N'lỗi ABC thường xuyên xuất hiện 
', N'KH002', N'<p><strong>1. Qu&yacute; kh&aacute;ch ch&uacute; &yacute;&nbsp;</strong></p>

<p><strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;<strong>​</strong>&nbsp; &nbsp; Ch&uacute;ng t&ocirc;i thực hiện một số c&ocirc;ng t&aacute;c như ABC&nbsp;</p>

<p><b>2. Điều khoản sử dụng&nbsp;</b></p>

<p><b>&nbsp; &nbsp;&nbsp;</b>Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;Thực hiện quy định v&agrave; cam kết khi sử dụng sản phẩm&nbsp;</p>

<p><strong>3. C&aacute;ch thức sửa lỗi&nbsp;</strong></p>

<p>&nbsp; &nbsp; Qu&yacute; kh&aacute;ch vui l&ograve;ng li&ecirc;n hệ&nbsp;<span style="background-color:Yellow;">0904158333</span>&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;để biết th&ecirc;m th&ocirc;ng tin chi tiết&nbsp;</p>
', N'TT004', 0, N'SP005', N'LHD003', N'Xem lại câu trả lời', N'', N'Lỗi ABC xuất hiện nhiều lần ', CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000029', N'Cài đặt thành côngnhưng thiếu chức năng', N'Tôi đã cài đặt thành công nhưng chức năng không đầy đủ như mô tả của công ty', N'KH001', N'<p>B&agrave;i viết n&agrave;y hướng dẫn c&aacute;ch hạch to&aacute;n c&aacute;c khoản chi hộ trong chế độ kế to&aacute;n ng&acirc;n s&aacute;ch v&agrave; t&agrave;i ch&iacute;nh x&atilde; theo ban h&agrave;nh k&egrave;m theo Th&ocirc;ng tư 70/2019/TT-BTC ng&agrave;y 31/10/2019</p>

<p>B&agrave;i viết n&agrave;y hướng dẫn c&aacute;ch hạch to&aacute;n c&aacute;c khoản chi hộ trong chế độ kế to&aacute;n ng&acirc;n s&aacute;ch v&agrave; t&agrave;i ch&iacute;nh x&atilde; theo ban h&agrave;nh k&egrave;m theo Th&ocirc;ng tư 70/2019/TT-BTC ng&agrave;y 31/10/2019</p>

<p><strong>I. Chi hộ tại x&atilde; l&agrave; g&igrave;? </strong></p>

<p>Chi hộ tại x&atilde; l&agrave; khoản m&agrave; x&atilde; đứng ra để chi tiền cho c&aacute; nh&acirc;n tổ chức n&agrave;o đ&oacute;. C&aacute;c khoản n&agrave;y kh&ocirc;ng li&ecirc;n quan đến hoạt động của x&atilde; v&agrave; c&aacute;c khoản chi hộ&nbsp; n&agrave;y cũng được coi l&agrave; một khoản phải thu hoặc nợ phải trả.</p>

<p><strong>II. Phương ph&aacute;p hạch to&aacute;n của c&aacute;c khoản chi hộ</strong></p>

<p><strong>Kết cấu v&agrave; nội dung phản &aacute;nh của t&agrave;i khoản 336 &ndash; Thu hộ, chi hộ</strong>.</p>

<p><strong>B&ecirc;n Nợ:</strong></p>

<p>- Số đ&atilde; chi hộ cấp tr&ecirc;n</p>

<p>- Số tiền chi hộ kh&ocirc;ng hết nộp lại cấp tr&ecirc;n</p>

<p>- Th&ugrave; lao chi hộ được hưởng (nếu c&oacute;)</p>

<p><strong>B&ecirc;n C&oacute;: </strong></p>

<p>- Nhận được tiền do cấp tr&ecirc;n chuyển về nhờ chi hộ</p>

<p><strong>Số dư b&ecirc;n C&oacute;:</strong></p>

<p>- Số tiền chi hộ x&atilde; đ&atilde; nhận nhưng chưa chi</p>

<p><strong>T&agrave;i khoản 336 &ndash; C&aacute;c khoản thu hộ, chi hộ c&oacute; t&agrave;i khoản cấp 2:</strong></p>

<p>- T&agrave;i khoản 3362- C&aacute;c khoản chi hộ: T&agrave;i khoản chi hộ: t&agrave;i khoản n&agrave;y phản &aacute;nh c&aacute;c khoản UBND x&atilde; đứng ra nhận tiền chi hộ cho c&aacute;c cơ quan cấp tr&ecirc;n v&agrave; việc thanh to&aacute;n c&aacute;c khoản chi đ&oacute; với cơ quan cấp tr&ecirc;n.</p>

<p align="center"><strong>Sơ đồ hạch to&aacute;n kế to&aacute;n t&agrave;i khoản 336 &ndash; C&aacute;c khoản thu hộ, chi hộ</strong></p>
', N'TT005', 0, N'SP002', N'LHD001', N'', N'', N'', CAST(N'2021-07-07' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000030', N'Tôi chư biết cài đặt phần mềm', N'Bạn hướng dẫn tôi cách cài phần mềm VCS A invesst với ạ!', N'KH002', NULL, N'TT001', 1, N'SP002', N'LHD001', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000031', N'cách thức sử dụng ', N'Sử dụng mục A rất khó ', N'KH002', NULL, N'TT001', 1, N'SP003', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-06' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000032', N'Hỗ trợ khách hàng', N'tôi cần hỗ trợ thì liên hệ vào đâu ', N'KH002', N'<p>Kh&ocirc;ng biết nữa</p>
', N'TT005', 1, N'SP003', N'LHD004', N'', N'', N'Tôi cần hỗ trợ liên hệ thế nào', CAST(N'2021-07-06' AS Date), CAST(N'2021-07-08' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000033', N'Sửa phần mề VCS -E', N'Sửa phần mề VCS -E không sửa được chút nào', N'KH002', N'<p>Bạn tự t&igrave;m hiểu nh&eacute;!</p>
', N'TT005', 0, N'SP003', N'LHD002', N'', N'', N'', CAST(N'2021-07-06' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000034', N'Cau hoi lien qun den cai dat phan mem?Tôi nên làm thế nào?', N'Toi khong cai dat duoc phan mem! XIn huong dan cho toi! Cam on!', N'KH002', N'<p style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 1.6; color: rgb(0, 0, 0); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><strong>Trưa 7/7, Sở Gi&aacute;o dục v&agrave; Đ&agrave;o tạo TP.HCM th&ocirc;ng tin về t&igrave;nh h&igrave;nh thi tốt nghiệp Trung học phổ th&ocirc;ng (THPT) năm 2021 s&aacute;ng 7/7 tại TPHCM.</strong></p>

<p style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 1.6; color: rgb(0, 0, 0); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Theo đ&oacute;, s&aacute;ng 7/7, đối với m&ocirc;n Ngữ Văn, th&agrave;nh phố c&oacute; 88.245 th&iacute; sinh đăng k&iacute;, 82.978 th&iacute; sinh dự thi (chiếm tỷ lệ 94.03%). Số lượng nh&acirc;n vi&ecirc;n,&nbsp;c&aacute;n bộ coi thi l&agrave; 17.009/17.052 người, vắng 43 người do nằm trong khu vực phong tỏa. Th&agrave;nh phố đ&atilde; bổ sung 30 c&aacute;n bộ coi thi thay thế nhằm đảm bảo đủ nh&acirc;n lực tổ chức kỳ thi.</p>

<p style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 1.6; color: rgb(0, 0, 0); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><strong style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 1.6;">Về c&ocirc;ng t&aacute;c ph&ograve;ng chống dịch</strong>, Sở Gi&aacute;o dục v&agrave; Đ&agrave;o tạo TP.HCM c&ugrave;ng c&aacute;c điểm thi đ&atilde; triển khai đầy đủ c&aacute;c phương &aacute;n tham gia thi an to&agrave;n. Tuy nhi&ecirc;n, tại một số điểm thi tr&ecirc;n địa b&agrave;n th&agrave;nh phố c&oacute; ph&aacute;t sinh li&ecirc;n quan đến dịch bệnh<span>&nbsp;</span><a class="TextlinkBaiviet" href="https://www.24h.com.vn/dich-covid-19-c62e6058.html" style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; text-decoration: none; line-height: 1.6; color: rgb(0, 0, 255);" title="COVID-19">COVID-19</a>.</p>

<p style="font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 1.6; color: rgb(0, 0, 0); font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Cụ thể, trường THCS Đặng Trần C&ocirc;n (quận T&acirc;n Ph&uacute;) c&oacute; 1 th&iacute; sinh F0 đến l&agrave;m thủ tục v&agrave;o chiều 6/7, ng&agrave;y 7/7 kh&ocirc;ng đến điểm thi.</p>
', N'TT005', 0, N'SP003', N'LHD001', N'', N'', N'', CAST(N'2021-07-07' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000035', N'Toi chua biet su dung phan mem?', N'Nho ban quna trị huong dan toi SD phan mem. Toi chua biet su dung phan mem', N'KH002', N'<p><strong>Bạn tự tim hương dẫn tr&ecirc;n mạng nh&eacute;!</strong></p>
', N'TT004', 0, N'SP002', N'LHD002', N'Xem lai nội dung trả lời', N'', N'', CAST(N'2021-07-07' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000036', N'Tôi chưa biết cách cài đặt VCS-eBufget?', N'Tôi chưa biết cách cài đặt VCS-eBufget. Bạn có thể chỉ cho tôi được không?', N'KH002', N'<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;"><strong>A forum and Q&amp;A application with real-time updating in multiple languages.</strong></p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">The commercially hosted version appears at<span>&nbsp;</span><a href="https://popforums.com/" rel="nofollow" style="box-sizing: border-box; background-color: transparent; color: var(--color-text-link); text-decoration: none;">popforums.com</a>. This is the open source version.</p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">The main branch is now the work-in-progress for future versions running on .NET 5. The v17.x branch is v17.x, v16.x branch is v16.x, both for ASP.NET Core v3.1. v15 targeted Core v2.2. If you&#39;re looking for the version that works on .NET Framework 4.5+ with MVC 5, check out v13.0.2.</p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Roadmap: The v17 release concentrates on performance and optimization, along with significant refactoring and bug fixes. Forthcoming releases will focus on UI refinement and modernization.</p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">The next release will embrace .NET 5, the newer Bootstrap, ditch all of the old jQuery, use isolated process Azure Functions, etc.</p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">For the latest information and documentation, and how to get started, check the pages (also in markdown in /docs of source):<br style="box-sizing: border-box;" />
<a href="https://popworldmedia.github.io/POPForums/" rel="nofollow" style="box-sizing: border-box; background-color: transparent; color: var(--color-text-link); text-decoration: underline; outline: none; box-shadow: none;">https://popworldmedia.github.io/POPForums/</a></p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">Try it out and make test posts here:<br style="box-sizing: border-box;" />
<a href="https://meta.popforums.com/Forums" rel="nofollow" style="box-sizing: border-box; background-color: transparent; color: var(--color-text-link); text-decoration: none;">https://meta.popforums.com/Forums</a></p>

<p style="box-sizing: border-box; margin-bottom: 16px; margin-top: 0px; color: rgb(36, 41, 46); font-family: -apple-system, BlinkMacSystemFont, &quot;Segoe UI&quot;, Helvetica, Arial, sans-serif, &quot;Apple Color Emoji&quot;, &quot;Segoe UI Emoji&quot;; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;">CI build of master runs here:<br style="box-sizing: border-box;" />
<a href="https://popforumsdev.azurewebsites.net/Forums" rel="nofollow" style="box-sizing: border-box; background-color: transparent; color: var(--color-text-link); text-decoration: none;">https://popforumsdev.azurewebsites.net/Forums</a></p>
', N'TT005', 1, N'SP005', N'LHD001', N'', N'', N'', CAST(N'2021-07-07' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000037', N'Tôi muốn biết kết quả trận đấu giữa anh và đan mạch?', N'CHỗ tôi đang mất mạng. Tôi muốn biết kết quả trận đấu giữa anh và đan mạch? Xin cám ơn', N'KH002', N'<p>Bạn xem tr&ecirc;n 24h.com.vn nh&eacute;</p>

<p><span style="color: rgb(0, 0, 0); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">Được chơi tr&ecirc;n s&acirc;n nh&agrave; với sự cổ vũ cuồng nhiệt của c&aacute;c CĐV, tuyển Anh lập tức tr&agrave;n l&ecirc;n tấn c&ocirc;ng với lối đ&aacute; nhanh. Ph&uacute;t 13, Sterling tạo ra t&igrave;nh huống h&atilde;m th&agrave;nh đầu ti&ecirc;n nhưng pha đi b&oacute;ng v&agrave;o trong rồi dứt điểm của cầu thủ thuộc bi&ecirc;n chế Man City qu&aacute; hiền v&agrave; kh&ocirc;ng thể thắng được Kasper Schmeichel</span></p>

<p><span style="color: rgb(0, 0, 0); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;"><img alt="" src="https://cdn.24h.com.vn/upload/3-2021/images/2021-07-08/Video-Anh---dan-Mach-Nguoc-dong-kich-tinh-vo-oa-chien-tich-lich-su-Ban-ket-EURO-hjhj-1625694955-122-width660height412.jpg" /></span></p>
', N'TT005', 1, N'SP003', N'LHD001', N'', N'', N'', CAST(N'2021-07-07' AS Date), CAST(N'2021-07-07' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000038', N'Tiêu đề test', N'Vẫn là câu hỏi testtest', N'KH002', N'<p>C&acirc;u trả lời cũng test lu&ocirc;n</p>

<p>&nbsp;</p>
', N'TT005', 1, N'SP003', N'LHD002', N'', N'', N'Vẫn là câu hỏi testtest', CAST(N'2021-07-08' AS Date), CAST(N'2021-07-08' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000039', N'Tôi muốn hướng dẫn về cách cài đặt phần mềm và đăng nhập vào phần mềm', N'Tôi không cài đặt được phần mềm kế toán
Phần mềm tổng hợp không mở ra được, không đăng nhập được vào phần mềm', N'KH001', N'<p style="box-sizing: border-box; margin: 0px 0px 10px; color: rgb(121, 121, 121); font-family: &quot;Segoe UI&quot;, Tahoma, Geneva, Verdana, sans-serif; font-size: 14px;">K&iacute;nh thưa Anh chị, Ph&ograve;ng TK xin trả lời c&acirc;u hỏi của Anh chị như sau:</p>

<p style="box-sizing: border-box; margin: 0px 0px 10px; color: rgb(121, 121, 121); font-family: &quot;Segoe UI&quot;, Tahoma, Geneva, Verdana, sans-serif; font-size: 14px;">Việc c&agrave;i đặt phần mềm Anh chị l&agrave;m theo hướng dẫn tại link b&ecirc;n dưới:</p>

<p style="box-sizing: border-box; margin: 0px 0px 10px; color: rgb(121, 121, 121); font-family: &quot;Segoe UI&quot;, Tahoma, Geneva, Verdana, sans-serif; font-size: 14px;"><a href="https://vcsvietnam.com/" style="box-sizing: border-box; color: rgb(0, 122, 255); text-decoration-line: none; outline: 0px;">https://vcsvietnam.com/</a></p>

<p style="box-sizing: border-box; margin: 0px 0px 10px; color: rgb(121, 121, 121); font-family: &quot;Segoe UI&quot;, Tahoma, Geneva, Verdana, sans-serif; font-size: 14px;">Trường hợp Anh chị chưa c&agrave;i đặt được, anh chị c&oacute; thể li&ecirc;n hệ trực tiếp để được c&ocirc;ng ty hỗ trợ trực tiếp</p>

<p style="box-sizing: border-box; margin: 0px 0px 10px; color: rgb(121, 121, 121); font-family: &quot;Segoe UI&quot;, Tahoma, Geneva, Verdana, sans-serif; font-size: 14px;">Tr&acirc;n trọng cảm ơn</p>
', N'TT007', 0, N'SP001', N'LHD001', N'', N'', N'', CAST(N'2021-07-08' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000040', N'Test tối 9/7', N'Test', N'KH001', N'<p><a href="https://google.com.vn">https://google.com.vn</a></p>
', N'TT005', 1, N'SP001', N'LHD003', N'', N'', N'Test', CAST(N'2021-07-09' AS Date), CAST(N'2021-07-09' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000041', N'Temp', N'Temp', N'KH001', NULL, N'TT001', 1, N'SP001', N'LHD002', NULL, NULL, NULL, CAST(N'2021-07-09' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000042', N'T', N'TempTep', N'KH001', NULL, N'TT001', 1, N'SP004', N'LHD001', NULL, NULL, NULL, CAST(N'2021-07-09' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000043', N'Lỗi gì đây bạn ơi!', N'Lỗi gì đây bạn ơi. CHo mình hỏi với nhé!', N'KH001', NULL, N'TT001', 1, N'SP001', N'LHD003', NULL, NULL, NULL, CAST(N'2021-07-09' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000044', N'Test popup', N'test', N'KH001', NULL, N'TT001', 1, N'SP003', N'LHD004', NULL, NULL, NULL, CAST(N'2021-07-10' AS Date), NULL)
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000045', N'Test đặt câu hỏi?', N'Tôi chưa biết câu hỏi này nên đặt nhu thế nào! Đặt hộ tôi!', N'KH002', N'<p>Thử tra google</p>
', N'TT005', 1, N'SP003', N'LHD002', N'', N'', N'
Tôi chưa biết câu hỏi này nên đặt nhu thế nào! ', CAST(N'2021-07-09' AS Date), CAST(N'2021-07-09' AS Date))
INSERT [dbo].[HoiDap] ([maHoiDap], [tieuDe], [ndHoiDap], [maKhachHang], [ndTraLoi], [maTrangThai], [congKhai], [maSanPham], [maLoai], [traVeDuyet], [traVeXuatBan], [ndHoiDapEdit], [ngayNhan], [ngayXuatBan]) VALUES (N'HD0_00000046', N'aaaaaaaaa', N'aaaaaaaaaaaaa', N'KH002', NULL, N'TT001', 1, N'SP003', N'LHD001', NULL, NULL, NULL, CAST(N'2021-07-10' AS Date), NULL)
GO
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH001', N'SP001', CAST(N'2021-06-01' AS Date), CAST(N'2021-10-21' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH001', N'SP002', CAST(N'2021-01-15' AS Date), CAST(N'2021-12-31' AS Date), 2)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH001', N'SP003', CAST(N'2020-10-31' AS Date), CAST(N'2021-12-30' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH001', N'SP004', CAST(N'2020-07-22' AS Date), CAST(N'2021-09-14' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH001', N'SP006', CAST(N'2020-01-23' AS Date), CAST(N'2021-09-23' AS Date), 2)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH002', N'SP002', CAST(N'2020-12-20' AS Date), CAST(N'2021-12-30' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH002', N'SP003', CAST(N'2020-10-31' AS Date), CAST(N'2021-11-05' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH002', N'SP005', CAST(N'2020-10-31' AS Date), CAST(N'2021-12-25' AS Date), 2)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH003', N'SP001', CAST(N'2021-01-11' AS Date), CAST(N'2021-06-21' AS Date), 3)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH003', N'SP002', CAST(N'2020-10-31' AS Date), CAST(N'2021-12-31' AS Date), 1)
INSERT [dbo].[KeyLicense] ([maKhachHang], [maSanPham], [ngayBD], [ngayKT], [giaHan]) VALUES (N'KH003', N'SP005', CAST(N'2020-10-31' AS Date), CAST(N'2021-11-05' AS Date), 1)
GO
INSERT [dbo].[KhachHang] ([maKhachHang], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [coQuan], [ngayTuyen], [chucVu], [trinhDo], [linhVuc], [ngaySinh], [userName], [passwd], [trangThai]) VALUES (N'KH001', N'Nguyễn Huy', N' Hùng', N'Nam', N'0123456789', N'chunickdeptrailam@gmail.com', N'1234567898', N'hungavt.jpg', NULL, NULL, NULL, NULL, NULL, CAST(N'1999-10-07' AS Date), N'hungnguyenhuy', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[KhachHang] ([maKhachHang], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [coQuan], [ngayTuyen], [chucVu], [trinhDo], [linhVuc], [ngaySinh], [userName], [passwd], [trangThai]) VALUES (N'KH002', N'Bùi Văn', N'Dương', N'Nam', N'0123543212', N'cuongmtavietnam@gmail.com', N'1232143212', N'duongavt.jpg', NULL, NULL, NULL, NULL, NULL, CAST(N'1999-10-07' AS Date), N'duongtsnc1', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[KhachHang] ([maKhachHang], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [coQuan], [ngayTuyen], [chucVu], [trinhDo], [linhVuc], [ngaySinh], [userName], [passwd], [trangThai]) VALUES (N'KH003', N'Lê', N'Kim', N'Nữ', N'0123543212', N'Kim@gmail.com', N'1232143212', N'kimavt.jpg', NULL, NULL, NULL, NULL, NULL, CAST(N'1999-10-07' AS Date), N'kimkimkim', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
GO
INSERT [dbo].[LoaiHoiDap] ([maLoai], [tenLoai], [trangThai], [moTa]) VALUES (N'LHD001', N'Cài đặt', 1, NULL)
INSERT [dbo].[LoaiHoiDap] ([maLoai], [tenLoai], [trangThai], [moTa]) VALUES (N'LHD002', N'Sử dụng', 1, NULL)
INSERT [dbo].[LoaiHoiDap] ([maLoai], [tenLoai], [trangThai], [moTa]) VALUES (N'LHD003', N'Lỗi', 1, NULL)
INSERT [dbo].[LoaiHoiDap] ([maLoai], [tenLoai], [trangThai], [moTa]) VALUES (N'LHD004', N'Lỗi chung', 1, N'Lỗi chung thông dụng')
GO
INSERT [dbo].[LoaiTaiLieu] ([maLoaiTaiLieu], [tenLoaiTaiLieu], [trangThai], [moTa]) VALUES (N'LTL001', N'Tài liệu text', 1, NULL)
INSERT [dbo].[LoaiTaiLieu] ([maLoaiTaiLieu], [tenLoaiTaiLieu], [trangThai], [moTa]) VALUES (N'LTL002', N'Tài liệu video', 1, NULL)
INSERT [dbo].[LoaiTaiLieu] ([maLoaiTaiLieu], [tenLoaiTaiLieu], [trangThai], [moTa]) VALUES (N'LTL003', N'File cài đặt', 1, NULL)
GO
INSERT [dbo].[LoaiThongBao] ([maLoaiThongBao], [tenLoaiThongBao], [moTa]) VALUES (N'LTB0001', N'License của bạn sắp hết xin vui lòng gia hạn để tiếp tục sử dụng', NULL)
INSERT [dbo].[LoaiThongBao] ([maLoaiThongBao], [tenLoaiThongBao], [moTa]) VALUES (N'LTB0002', N'Câu hỏi của bạn đã được giải đáp', NULL)
GO
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV001', N'Nguyễn ', N'Hoàng', N'Nam', N'1234321432', N'Hoang@gmail.com', N'1343221343', N'hoangavt.jpg', N'Hoàng quốc việt', CAST(N'1998-12-20' AS Date), NULL, NULL, N'CD001', N'PB002', N'bientap', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV002', N'Nguyễn ', N'Huy', N'Nam', N'1234321432', N'Huy@gmail.com', N'1343221343', N'huyavt.jpg', N'Hoàng quốc việt', CAST(N'1998-10-20' AS Date), NULL, NULL, N'CD002', N'PB001', N'duyet', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV003', N'Cao', N'Ly', N'Nữ', N'1234321532', N'Ly@gmail.com', N'1232123213', N'lyavt.jpg', N'Hoàng quốc việt', CAST(N'1997-09-10' AS Date), NULL, NULL, N'CD001', N'PB001', N'xuatban', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV004', N'Dương Đức', N' Anh', N'Nam', N'1098765473', N'ducAnh@gmail.com', N'1093827482', N'ducanhavt.jpg', N'Hoàng quốc việt', CAST(N'1999-07-10' AS Date), NULL, NULL, N'CD002', N'PB001', N'admin', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV005', N'Lê Hoàng', N' Huynh', N'Nam', N'1098765473', N'huynhlehoang98@gmail.com', N'1093827482', N'ducanhavt.jpg', N'Hoàng quốc việt', CAST(N'1999-07-10' AS Date), NULL, NULL, N'CD002', N'PB001', N'bientap-duyet', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
INSERT [dbo].[NhanVien] ([maNhanVien], [ho], [ten], [gioiTinh], [SDT], [email], [CCCD], [anh], [diaChi], [ngaySinh], [trinhDo], [ngayTuyenDung], [maChucDanh], [maPhongBan], [userName], [passwd], [trangThai]) VALUES (N'NV006', N'Nguyễn Tú', N' Anh', N'Nữ', N'1098765473', N'anhtunguyen@gmail.com', N'1093827482', N'ducanhavt.jpg', N'Hoàng quốc việt', CAST(N'1999-07-10' AS Date), NULL, NULL, N'CD002', N'PB001', N'duyet-xuatban', N'd77be63bba7806f1045a28bd09dc7153', N'Đã kích hoạt')
GO
INSERT [dbo].[NhomChuDe] ([maNhomChuDe], [tenNhomChuDe], [moTa], [ngayTao]) VALUES (N'ND0_00000001', N'Sản phẩm A', N'', CAST(N'2021-06-13' AS Date))
INSERT [dbo].[NhomChuDe] ([maNhomChuDe], [tenNhomChuDe], [moTa], [ngayTao]) VALUES (N'ND0_00000002', N'Sản phẩm B', N'', CAST(N'2021-06-13' AS Date))
GO
SET IDENTITY_INSERT [dbo].[NV_HoiDap] ON 

INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (1, N'NV001', N'HD0_00000001', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-06-13' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (2, N'NV001', N'HD0_00000002', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-06-12' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (3, N'NV001', N'HD0_00000003', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-05-16' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (4, N'NV001', N'HD0_00000004', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-05-12' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (5, N'NV001', N'HD0_00000005', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-06-13' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (6, N'NV005', N'HD0_00000006', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-05-17' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (7, N'NV005', N'HD0_00000007', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-05-16' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (8, N'NV005', N'HD0_00000008', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-05-12' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (9, N'NV001', N'HD0_00000009', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-07-04' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (10, N'NV001', N'HD0_00000010', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-07-04' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (11, N'NV001', N'HD0_00000011', N'Đã biên tập, đang chờ duyệt', CAST(N'2021-07-04' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (12, N'NV002', N'HD0_00000001', N'Đã duyệt', CAST(N'2021-07-04' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (13, N'NV005', N'HD0_00000002', N'Đã duyệt', CAST(N'2021-07-04' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (14, N'NV006', N'HD0_00000003', N'Đã duyệt', CAST(N'2021-07-04' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (15, N'NV002', N'HD0_00000004', N'Đã duyệt', CAST(N'2021-07-04' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (16, N'NV003', N'HD0_00000001', N'Xuất bản ', CAST(N'2021-07-04' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (17, N'NV003', N'HD0_00000002', N'Xuất bản ', CAST(N'2021-07-04' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (18, N'NV006', N'HD0_00000003', N'Xuất bản ', CAST(N'2021-07-04' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (19, N'NV001', N'HD0_00000012', N'Gửi duyệt', CAST(N'2021-07-04' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (20, N'NV002', N'HD0_00000012', N'Gửi xuất bản', CAST(N'2021-07-04' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (21, N'NV003', N'HD0_00000012', N'Xuất bản ', CAST(N'2021-07-04' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (22, N'NV001', N'HD0_00000021', N'thuc hien theo quyen', CAST(N'2021-09-23' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (23, N'NV002', N'HD0_00000021', N'thuc hien theo quyen', CAST(N'2021-09-23' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (24, N'NV003', N'HD0_00000021', N'thuc hien theo quyen', CAST(N'2021-09-23' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (25, N'NV001', N'HD0_00000028', N'Gửi duyệt', CAST(N'2021-07-06' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (26, N'NV001', N'HD0_00000027', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (27, N'NV002', N'HD0_00000027', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (28, N'NV003', N'HD0_00000027', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (29, N'NV001', N'HD0_00000029', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (30, N'NV002', N'HD0_00000029', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (31, N'NV003', N'HD0_00000029', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (32, N'NV001', N'HD0_00000022', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (33, N'NV002', N'HD0_00000022', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (34, N'NV003', N'HD0_00000022', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (35, N'NV001', N'HD0_00000034', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (36, N'NV002', N'HD0_00000034', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (37, N'NV003', N'HD0_00000034', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (38, N'NV001', N'HD0_00000035', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (39, N'NV002', N'HD0_00000035', N'Trả về biên tập', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (40, N'NV001', N'HD0_00000033', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (41, N'NV002', N'HD0_00000033', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (42, N'NV003', N'HD0_00000033', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (43, N'NV003', N'HD0_00000034', N'Thu hồi câu hỏi', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (44, N'NV003', N'HD0_00000034', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (45, N'NV001', N'HD0_00000036', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (46, N'NV002', N'HD0_00000036', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (47, N'NV003', N'HD0_00000036', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (48, N'NV001', N'HD0_00000037', N'Gửi duyệt', CAST(N'2021-07-07' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (49, N'NV002', N'HD0_00000037', N'Gửi xuất bản', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (50, N'NV002', N'HD0_00000028', N'Trả về biên tập', CAST(N'2021-07-07' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (51, N'NV003', N'HD0_00000037', N'Xuất bản ', CAST(N'2021-07-07' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (52, N'NV001', N'HD0_00000038', N'Gửi duyệt', CAST(N'2021-07-08' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (53, N'NV002', N'HD0_00000038', N'Gửi xuất bản', CAST(N'2021-07-08' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (54, N'NV003', N'HD0_00000038', N'Xuất bản ', CAST(N'2021-07-08' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (55, N'NV001', N'HD0_00000032', N'Gửi duyệt', CAST(N'2021-07-08' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (56, N'NV002', N'HD0_00000032', N'Gửi xuất bản', CAST(N'2021-07-08' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (57, N'NV003', N'HD0_00000032', N'Xuất bản ', CAST(N'2021-07-08' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (58, N'NV001', N'HD0_00000039', N'Gửi duyệt', CAST(N'2021-07-09' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (59, N'NV002', N'HD0_00000039', N'Gửi xuất bản', CAST(N'2021-07-09' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (60, N'NV003', N'HD0_00000039', N'Xuất bản ', CAST(N'2021-07-09' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (61, N'NV003', N'HD0_00000039', N'Thu hồi câu hỏi', CAST(N'2021-07-09' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (62, N'NV001', N'HD0_00000040', N'Gửi duyệt', CAST(N'2021-07-09' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (63, N'NV002', N'HD0_00000040', N'Gửi xuất bản', CAST(N'2021-07-09' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (64, N'NV003', N'HD0_00000040', N'Xuất bản ', CAST(N'2021-07-09' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (65, N'NV001', N'HD0_00000045', N'Gửi duyệt', CAST(N'2021-07-09' AS Date), N'Biên tập')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (66, N'NV002', N'HD0_00000045', N'Gửi xuất bản', CAST(N'2021-07-09' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (67, N'NV003', N'HD0_00000045', N'Xuất bản ', CAST(N'2021-07-09' AS Date), N'Xuất bản')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (68, N'NV001', N'HD0_00000011', N'Gửi xuất bản', CAST(N'2021-07-09' AS Date), N'Duyệt')
INSERT [dbo].[NV_HoiDap] ([Id], [maNhanVien], [maHoiDap], [noiDungThucHien], [ngayThang], [quyen]) VALUES (69, N'NV001', N'HD0_00000011', N'Xuất bản ', CAST(N'2021-07-09' AS Date), N'Xuất bản')
SET IDENTITY_INSERT [dbo].[NV_HoiDap] OFF
GO
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV001', N'Q001', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV001', N'Q002', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV001', N'Q003', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV002', N'Q002', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV003', N'Q003', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV004', N'Q004', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV005', N'Q001', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV005', N'Q002', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV006', N'Q001', 0)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV006', N'Q002', 1)
INSERT [dbo].[NV_Quyen] ([maNhanVien], [maQuyen], [trangThai]) VALUES (N'NV006', N'Q003', 1)
GO
SET IDENTITY_INSERT [dbo].[NV_TaiLieu] ON 

INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (1, N'NV001', N'TL000001', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (2, N'NV005', N'TL000002', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-01-15T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (3, N'NV001', N'TL000003', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-06-13T00:00:00.000' AS DateTime), N'Biên tập ')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (4, N'NV001', N'TL000004', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-06-12T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (5, N'NV001', N'TL000005', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (6, N'NV005', N'TL000006', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-01-17T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (7, N'NV001', N'TL000007', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2020-11-30T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (8, N'NV005', N'TL000008', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-01-17T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (9, N'NV005', N'TL000009', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-06-14T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (10, N'NV005', N'TL000010', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-06-13T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (12, N'NV001', N'TL000011', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-06-14T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (13, N'NV005', N'TL000012', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-05-22T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (16, N'NV005', N'TL000013', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-02T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (17, N'NV002', N'TL000001', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-05T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (18, N'NV005', N'TL000002', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-06-29T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (19, N'NV002', N'TL000003', N'Tài liệu đã được trả về', CAST(N'2021-07-05T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (20, N'NV006', N'TL000004', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-06-28T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (21, N'NV002', N'TL000006', N'Tài liệu đã được trả về', CAST(N'2021-06-28T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (22, N'NV002', N'TL000013', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (23, N'NV002', N'TL000012', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (24, N'NV002', N'TL000011', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (25, N'NV002', N'TL000010', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (26, N'NV002', N'TL000009', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (27, N'NV002', N'TL000008', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (28, N'NV002', N'TL000007', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (29, N'NV002', N'TL000005', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (30, N'NV001', N'TL000014', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (31, N'NV002', N'TL000014', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (32, N'NV001', N'TL000015', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (33, N'NV001', N'TL000016', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (34, N'NV002', N'TL000016', N'Tài liệu đã được trả về', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Trả về')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (35, N'NV002', N'TL000015', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (36, N'NV001', N'TL000016', N'Tài liệu đã được gửi lại để duyệt', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (37, N'NV002', N'TL000016', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (38, N'NV001', N'TL000017', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (39, N'NV002', N'TL000017', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (40, N'NV001', N'TL000018', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (41, N'NV001', N'TL000019', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (42, N'NV002', N'TL000019', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (43, N'NV002', N'TL000019', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (44, N'NV001', N'TL000020', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (45, N'NV001', N'TL000021', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (46, N'NV002', N'TL000021', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (47, N'NV002', N'TL000021', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (48, N'NV002', N'TL000021', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (49, N'NV001', N'TL000022', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (50, N'NV002', N'TL000022', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (51, N'NV001', N'TL000023', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (52, N'NV001', N'TL000024', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (53, N'NV001', N'TL000025', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (54, N'NV001', N'TL000026', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (55, N'NV001', N'TL000027', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (56, N'NV002', N'TL000024', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (57, N'NV002', N'TL000025', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (58, N'NV002', N'TL000026', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (59, N'NV002', N'TL000027', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-08T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (60, N'NV005', N'TL000028', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (61, N'NV001', N'TL000029', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (62, N'NV005', N'TL000030', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-10T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (63, N'NV005', N'TL000031', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-10T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (64, N'NV005', N'TL000032', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-10T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (65, N'NV005', N'TL000033', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-10T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (66, N'NV005', N'TL000034', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-10T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (67, N'NV002', N'TL000022', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (68, N'NV002', N'TL000017', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (69, N'NV002', N'TL000016', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (70, N'NV002', N'TL000015', N'Tài liệu đã bị thu hồi', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (71, N'NV001', N'TL000035', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (72, N'NV001', N'TL000036', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (73, N'NV001', N'TL000037', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (74, N'NV001', N'TL000038', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (75, N'NV001', N'TL000039', N'Thực hiện biên tập, soạn thảo tài liệu tham khảo', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Biên tập')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (76, N'NV002', N'TL000035', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (77, N'NV002', N'TL000036', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (78, N'NV002', N'TL000037', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (79, N'NV002', N'TL000038', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
INSERT [dbo].[NV_TaiLieu] ([Id], [maNhanVien], [maTaiLieu], [noiDungThucHien], [ngayThang], [quyen]) VALUES (80, N'NV002', N'TL000039', N'Tài liệu đã được duyệt và xuất bản thành công', CAST(N'2021-07-09T00:00:00.000' AS DateTime), N'Duyệt')
SET IDENTITY_INSERT [dbo].[NV_TaiLieu] OFF
GO
INSERT [dbo].[PhongBan] ([maPhongBan], [tenPhongBan], [moTa]) VALUES (N'PB001', N'Phòng nhân sự', NULL)
INSERT [dbo].[PhongBan] ([maPhongBan], [tenPhongBan], [moTa]) VALUES (N'PB002', N'Phòng kỹ thuật', NULL)
GO
SET IDENTITY_INSERT [dbo].[QLThongTin] ON 

INSERT [dbo].[QLThongTin] ([Id], [tenCongTy], [logo], [slogan], [subslogan], [linkFaceBook], [linkSkype], [linkTwitter], [linkInstagram], [linkBanDo], [email], [phone], [address], [phoneBusiness], [tongDai]) VALUES (1, N'Công ty Cổ phần công nghệ VCS Việt Nam', N'/images/logo-VCS_20d0.ico', N'CÔNG NGHỆ TIÊN PHONG - TÍNH NĂNG VƯỢT TRỘI - HIỆU QUẢ HÀNG ĐẦU', N'CÔNG NGHỆ TIÊN PHONG', N'https://www.facebook.com/MDMyOTM0MTU4Mw/', N'https://www.google.com/', N'https://twitter.com/', N'https://www.instagram.com/le_hoang_huynh/', N'https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d15256528.818432583!2d105.82014079999999!3d21.0031177!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab624d5b38d7%3A0xc4eb647dc53c8099!2zMTAxIEzDoW5nIEjhuqEsIMSQ4buRbmcgxJBhLCBIw6AgTuG7mWksIFZp4buHdCBOYW0!5e0!3m2!1svi!2s!4v1625820111782!5m2!1svi!2s', N'huynhlehoang98@gmail.com', N'1900.555.526 - (024) 3773.9829', N'Tầng 2, Tòa B, Số 101 Láng Hạ, Đống Đa, Hà Nội', N'0981209212', N'1900.555.526')
SET IDENTITY_INSERT [dbo].[QLThongTin] OFF
GO
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen], [moTa]) VALUES (N'Q001', N'Biên tập', NULL)
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen], [moTa]) VALUES (N'Q002', N'Duyệt', NULL)
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen], [moTa]) VALUES (N'Q003', N'Xuất bản', NULL)
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen], [moTa]) VALUES (N'Q004', N'Admin', NULL)
GO
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP001', N'Phần mềm kế toán đơn vị Chủ đầu tư (áp dụng cho Doanh nghiệp) VCS-INVEST (E)', N'Là sản phẩm chuyên dụng hàng đầu cho công tác kế toán Ban quản lý dự án đầu tư trong các doanh nghiệp thuộc mọi thành phần kinh tế; được xây dựng trên nền công nghệ và kỹ thuật hiện đại, tiên tiến; với nhiều tính năng vượt trội, dễ sử dụng; Tuân thủ và đáp ứng đầy đủ các yêu cầu của công tác kế tại các ban quản lý dự án đầu tư trong CÁC DOANH NGHIỆP do Bộ Tài chính ban hành: - Thông tư số 195/2012/TT-BTC ngày 15/11/2012 của Bộ Tài chính hướng dẫn kế toán áp dụng cho đơn vị chủ đầu tư; - Các quy định khác của nhà nước liên quan đến kế toán đơn vị chủ đầu tư.', CAST(N'2021-06-26' AS Date), N'a397b8ab-f8b3-4de2-8fb3-7dbf3929ba2fpng_cover-dvd-vcs--vcs-invest-1.1.png', N'https://vcsvietnam.com/sanpham?Id=1')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP002', N'Phần mềm kế toán đơn vị Chủ đầu tư (áp dụng cho đơn vị HCSN) VCS-INVEST (A)', N'Phần mềm Kế toán đơn vị chủ đầu tư VCS INVEST (A) là sản phẩm chuyên dụng hàng đầu cho công tác kế toán của các chủ đầu tư trong khu vực hành chính sự nghiệp; được xây dựng trên nền công nghệ và kỹ thuật tiên tiến, dễ sử dụng; Tuân thủ và đáp ứng đầy đủ các yêu cầu của công tác kế tại đầu tư trong KHU VỰC HCSN do Bộ Tài chính ban hành (Thông tư số 195/2012/TT-BTC ngày 15/11/2012 của Bộ Tài chính hướng dẫn kế toán áp dụng cho đơn vị chủ đầu tư) và các quy định khác của nhà nước liên quan đến kế toán đơn vị chủ đầu tư.', CAST(N'2021-06-26' AS Date), N'f2af0f3f-660f-4820-9d36-1b2237027f58png_cover-dvd-vcs--vcs-invest-e-1.1.png', N'https://vcsvietnam.com/sanpham?Id=2')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP003', N'Phần mềm kế toán Doanh nghiệp VCS-Enterprise', N'Phần mềm kế toán doanh nghiệp (VCS Enterprise) là công cụ chuyên dụng cho công tác kế toán của các doanh nghiệp nhỏ và vừa; Được xây dựng trên nền công nghệ và kỹ thuật, tiên tiến; dễ sử dụng; Tuân thủ và đáp ứng đầy đủ các yêu cầu của công tác kế toán trong doanh nghiệp nhỏ và vừa do Bộ Tài chính ban hành: - Quyết định số 48/2006/QĐ-BTC ngày 14/09/2006 của Bộ Tài chính ban hành Chế độ kế toán doanh nghiệp nhỏ và vừa, Thông tư số 138/2011/TT-BTC của Bộ Tài chính hướng dẫn sửa đổi, bổ sung Quyết định số 48/2006/QĐ-BTC. - Các quy định khác của nhà nước liên quan đến công tác kế toán doanh nghiệp nhỏ và vừa.', CAST(N'2021-06-26' AS Date), N'15bb862b-9058-44d6-99c6-6ba3bb6ab710PNG__cover-dvd-VCS--VCS-Enterprise%20-1.5.png', N'https://vcsvietnam.com/sanpham?Id=3')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP004', N'Phần mềm kế toán Hợp tác xã VCS - PADDY', N'Công ty Cổ phần Công nghệ VCS Việt Nam, với bề dày nhiều năm kinh nghiệm và chuyên môn trong lĩnh vực Kế toán Hợp tác xã đã nghiên cứu, phát triển và hoàn thiện phiên bản mới của Phần mềm Kế toán Hợp tác xã (VCS – PADDY 18.1), cập nhật kịp thời Chế độ kế toán Hợp tác xã mới theo Thông tư 24/2017/TT-BTC do Bộ Tài chính ban hành ngày 28/03/3017, đồng thời vẫn tiếp tục kế thừa, nâng cao các tính năng ưu việt, tiện ích của Phần mềm hiện đang được hàng nghìn khách hàng trên cả nước yêu thích và tin dùng.', CAST(N'2021-06-26' AS Date), N'ca9b630e-a641-4cab-beb0-12ef8e121e7aPNG_cover-dvd-VCS--VCS-PADDY-1.2.png', N'https://vcsvietnam.com/sanpham?Id=4')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP005', N'Phần mềm kế toán Ngân sách và Tài chính xã VCS-eBudget', N'Phần mềm kế toán Ngân sách và tài chính xã phiên bản 21.1, được xây dựng trên nền công nghệ và kỹ thuật hiện đại, tiên tiến; với nhiều tính năng vượt trội, dễ sử dụng; phù hợp với đặc thù của công tác kế toán ngân sách và hoạt động tài chính ở các xã, phường, thị trấn. Tuân thủ và đáp ứng đầy đủ các yêu cầu của Chế độ kế toán Ngân sách và Tài chính xã và các quy định hiện hành có liên quan của Nhà nước, như: - Thông tư số 70/2019/TT-BTC ngày 03/10/2019 của Bộ Tài chính về hướng dẫn chế độ kế toán ngân sách và tài chính xã; Và các quy định liên quan của Nhà nước đến công tác kế toán Ngân sách xã.', CAST(N'2021-06-26' AS Date), N'559a64fd-f65e-4fb6-841a-5a6544aef769PNG_cover-dvd-VCS--VCS-eBudget%20-1.1.png', N'https://vcsvietnam.com/sanpham?Id=5')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP006', N'Phần mềm kế toán Hành chính sự nghiệp VCS-ACS', N'Công ty Cổ phần Công nghệ VCS Việt Nam, với bề dày nhiều năm kinh nghiệm và chuyên môn trong lĩnh vực Kế toán Hành chính sự nghiệp đã nghiên cứu, phát triển và hoàn thiện phiên bản mới của Phần mềm Kế toán Hành chính sự nghiệp (VCS – ACS 21.1), cập nhật kịp thời Chế độ kế toán Hành chính, sự nghiệp mới theo Thông tư số 107/2017/TT-BTC và các quy định mới nhất của Nhà nước, đồng thời vẫn tiếp tục kế thừa, nâng cao các tính năng ưu việt, tiện ích của Phần mềm hiện đang được hàng nghìn khách hàng trên cả nước yêu thích và tin dùng.', CAST(N'2021-06-26' AS Date), N'5e22e6c0-4c97-4bcc-a1ec-ef2db52bbfbfPNG_cover-dvd-VCS--All%20Cover%20-%20v2_1_v1.png', N'https://vcsvietnam.com/sanpham?Id=6')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP007', N'DỊCH VỤ CÔNG NGHỆ THÔNG TIN', N'Cùng với hoạt động nghiên cứu, phát triển các ứng dụng CNTT phục vụ quản lý, Công ty CP Công nghệ VCS Việt Nam cung cấp các dịch vụ công nghệ thông tin: 1. Phân phối, hướng dẫn sử dụng sử dụng, bảo trì phần mềm ứng dụng do Công ty phát triển, cung ứng. 2. Cập nhật dữ liệu số. 3. Cho thuê trong lĩnh vực CNTT. 4. Xây dựng phần mềm quản lý theo yêu cầu. 5. Cài đặt mạng máy tính. 6. Các dịch vụ công nghệ thông tin khác.', CAST(N'2021-06-26' AS Date), N'db3e4db7-849e-4161-a7ae-8743776211f4it-phanmem.png', N'https://vcsvietnam.com/sanpham?Id=7')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP008', N'CÁC DỊCH VỤ KHÁC', N'1. Tư vấn đầu tư trong lĩnh vực CNTT. 2. Đại lý phân phối các sản phẩm, dịch vụ và thiết bị CNTT, thiết bị văn phòng. 3. Tư vấn quản lý kinh tế- tài chính.', CAST(N'2021-06-26' AS Date), N'9c58dcae-18a9-4fa4-aeb0-40ef1fb46f12it-dichvu.png', N'https://vcsvietnam.com/sanpham?Id=8')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP009', N'Hệ thống Quản lý thu RMS.NET', N'Phần mềm Quản lý thu RMS.NET là công cụ chuyên dụng trong công tác quản lý các khoản thu do chính quyền cấp xã và các tổ chức trên địa bàn xã thực hiện theo quy định; Hệ thống Quản lý thu được xây dựng trên nền công nghệ và kỹ thuật tiên tiến, dễ sử dụng; đáp ứng yêu cầu triển khai trên diện rộng; Các tính năng của phần mềm đáp ứng đầy đủ các quy định của Nhà nước về tổ chức quản lý các khoản thu trên địa bàn xã, phường, thị trấn theo các quy định của trung ương và địa phương, bao gồm các khoản thu thuộc NSNN và các khoản thu của các quỹ tài chính ngoài NSNN, các khoản thu hộ,... do cấp xã thực hiện.', CAST(N'2021-06-26' AS Date), N'5b66501c-bb06-4f8e-a63b-b5b18d9c6522png_cover-dvd-vcs--rms-1.3.png', N'https://vcsvietnam.com/sanpham?Id=9')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP010', N'Hệ thống Quản lý giáo dục VCS-EDUNET', N'Phần mềm Quản lý hoạt động giáo dục EDUNET là sản phẩm chuyên dụng cho công tác quản lý hoạt động giáo dục ở các cơ sở giáo dục và cơ quan quản lý cấp trên; được xây dựng trên nền công nghệ và kỹ thuật, tiên tiến, dễ sử dụng; đáp ứng yêu cầu triển khai trên diện rộng. Phần mềm tuân thủ và đáp ứng đầy đủ các quy định của Nhà nước về tổ chức hoạt động, sử dụng thư điện tử và cổng thông tin điện tử tại sở giáo dục và đào tạo, phòng giáo dục và đào tạo và các cơ sở giáo dục mầm non, giáo dục phổ thông và giáo dục thường xuyên và các quy định của Bộ Giáo dục về quy định đánh giá, xếp loại học sinh và công khai đối với cơ sở giáo dục của hệ thống giáo dục quốc dân. Đăp ứng đầy đủ yêu cầu cung cấp thông tin cho công tác quản lý, giám sát thường xuyên của cơ quan nhà nước đối với các cơ sở giáo dục.', CAST(N'2021-06-26' AS Date), N'0c769381-f947-4678-ba56-4e36a3d54203png__cover-dvd-vcs--edunet.png', N'https://vcsvietnam.com/sanpham?Id=10')
INSERT [dbo].[SanPham] ([maSanPham], [tenSanPham], [moTa], [ngayTao], [anh], [link]) VALUES (N'SP011', N'Phần mềm Quản lý tài sản ATS.NET', N'Hệ thống phần mềm quản lý tài sản (ATS.NET) đáp ứng nhu cầu cho công tác quản lý tài sản tại các cơ quan nhà nước, doanh nghiệp; được phát triển đáp ứng nhu cầu triển khai trên diện rộng, dễ sử dụng; Hệ thống đáp ứng quy định trong quản lý, sử dụng tài sản nhà nước. Hệ thống là công cụ có hiệu quả trong việc cập nhật tình hình tài sản, giám sát quản lý sử dụng tài sản tại các cơ quan, tổ chức và doanh nghiệp.', CAST(N'2021-06-26' AS Date), N'd98ca92f-ac15-40bf-89d9-289bae467c0bpng__cover-dvd-vcs--ats.png', N'https://vcsvietnam.com/sanpham?Id=11')
GO
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000001', N'Thông tin sản phẩm DELL xxxx', N'Hỗ trợ sản phẩm ', N'abc.txt', N'TL001.text', 1, CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'abc', N'LTL003', N'TT003', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000002', N'Hướng dẫn sử dụng ', N'Sử dụng sản phẩm ', N'abc.txt', N'TL002.txt', 1, CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT003', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000003', N'Thông tin sản phẩm', N'Sản phẩm abc', N'abc.txt', N'TL003.txt', 0, CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT004', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000004', N'Cách fix bug xxx', N'Hỗ trợ sử dụng sản phẩm', N'abc.txt', N'TL004.txt', 1, CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT003', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000005', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'abc.txt', N'TL005.txt', 1, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000006', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'LearningSQL.pdf', N'LearningSQL.pdf', 1, CAST(N'2020-12-31T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT004', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000007', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu', N'BonehShoup_0_5.pdf', N'BonehShoup_0_5.pdf', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000008', N'Thêm tài khoản', N'Tài khoản là một điêu không thể thiếu ', N'video1.mp4', N'video1.mp4', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL002', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000009', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'video2.mp4', N'video2.mp4', 1, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL002', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000010', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'LearningSQL.pdf', N'LearningSQL.pdf', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000011', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'BonehShoup_0_5.pdf', N'BonehShoup_0_5.pdf', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000012', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'video1.mp4', N'video1.mp4', 1, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL002', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000013', N'Thêm tài khoản', N'Tài khoản là một điều không thể thiếu ', N'video2.mp4', N'video2.mp4', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), N'abc', N'LTL002', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000014', N'Test TL này xem sao', N'Chắc là đúng thôi', N'$5to$100kchallenge_81d4.pdf', N'$5to$100kchallenge_81d4.pdf', 1, CAST(N'2021-07-07T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000015', N'file hướng dẫn', N'file hướng dẫn', N'NCKH_2021(1)_5014.pdf', N'NCKH_2021(1)_5014.pdf', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT007', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000016', N'File cài đặt', N'File cài đặt', N'bool_day_4707.exe', N'bool_day_4707.exe', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL002', N'TT007', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000017', N'test word', N'test word', N'TestWWebSite_V2_932c.docx', N'TestWWebSite_V2_932c.docx', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT007', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000018', N'file exe', N'cài đạt Wireshare', N'Wireshark-win64-3.2.3_bcfb.exe', N'Wireshark-win64-3.2.3_bcfb.exe', 0, CAST(N'2021-07-07T00:00:00.000' AS DateTime), NULL, N'LTL002', N'TT002', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000019', N'file exe', N'cài đặt pichon', N'PichonSetup_0a1a.exe', N'PichonSetup_0a1a.exe', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL002', N'TT007', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000020', N'File cài H up test', N'Test', N'winrar-550-64bit_e79e.exe', N'winrar-550-64bit_e79e.exe', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000021', N'File cài đặt', N'File cài đặt phần mềm', N'bool_day_4707_eb7d.exe', N'bool_day_4707_eb7d.exe', 1, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000022', N'File cài đặt chương trình', N'File cài đặt chương trình', N'bool_day_4707_e3ad.exe', N'bool_day_4707_e3ad.exe', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT007', N'SP003')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000023', N'thanh hoang tesst', N'hoang test', N'OceanFinder_web_201909_4ec5.pdf', N'OceanFinder_web_201909_4ec5.pdf', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000024', N'File cài đặt phần mềm', N'Cài đặt môi trường', N'setup_prerequisite21_25e6.exe', N'setup_prerequisite21_25e6.exe', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000025', N'File cài đặt phần mềm kế toán', N'Cài đặt phần mềm kế toán ', N'setup_prerequisite21_f10e.exe', N'setup_prerequisite21_f10e.exe', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000026', N'Tài liệu hướng dẫn sử dụng phần mềm kế toán CĐ', N'Tài liệu hướng dẫn SD', N'54_2021_TT-BTC_480452_46c4.doc', N'54_2021_TT-BTC_480452_46c4.doc', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000027', N'Tài liệu hướng dẫn cài đặt', N'Hướng dẫn cài đặt phần mềm', N'480555(2)_f127.pdf', N'480555(2)_f127.pdf', 0, CAST(N'2021-07-08T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000028', N'Test doc', N'Test', N'Phụlục17_b022.docx', N'Ph?l?c17_b022.docx', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000029', N'Huynh Test docx', N'J4F', N'Ghibài_335a.docx', N'Ghibài_335a.docx', 0, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000030', N'Test docxxxx', N'Tesstttttttt', N'Phụlục17_3011.docx', N'Ph?l?c17_3011.docx', 0, CAST(N'2021-07-10T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000031', N'another test', N'test another file', N'Le_Hoang_Huynh_3449.docx', N'Le_Hoang_Huynh_3449.docx', 0, CAST(N'2021-07-10T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000032', N'Test xlsx', N'Tài liệu xlsxtest', N'Tổngkếttoànkhóa_687d.xlsx', N'T?ngk?ttoànkhóa_687d.xlsx', 0, CAST(N'2021-07-10T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000033', N'another xlsx', N'xlsx file', N'Tổng_kết_toàn_khóa_6504.xlsx', N'T?ng_k?t_toàn_khóa_6504.xlsx', 0, CAST(N'2021-07-10T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000034', N'test tên tiếng việt', N'test', N'Tổng_kết_toàn_khóa_22ac.xlsx', N'T?ng_k?t_toàn_khóa_22ac.xlsx', 0, CAST(N'2021-07-10T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT002', N'SP001')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000035', N'Tài liệu dạng PDF', N'Tài liệu hướng dẫn dạng PDF', N'480555_8e14.pdf', N'480555_8e14.pdf', 1, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP004')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000036', N'Tài liệu dạng word', N'Tài liệu hướng dẫn word', N'Thông_tư_54_04aa.doc', N'Thông_tu_54_04aa.doc', 1, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP004')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000037', N'Tài liệu dạng excel', N'Tài liệu hướng dẫn bảng biểu excel', N'UNC-Viettinbank_của_Hằng_9d8a.xlsx', N'UNC-Viettinbank_c?a_H?ng_9d8a.xlsx', 1, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL001', N'TT005', N'SP004')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000038', N'File cai dat .exe', N'File cai dat .exe. ', N'bool_day_4707_2361.exe', N'bool_day_4707_2361.exe', 1, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL003', N'TT005', N'SP004')
INSERT [dbo].[TaiLieu] ([maTaiLieu], [tenTaiLieu], [moTa], [tenFile], [duongDan], [downLoad], [ngayThang], [lyDoTraVe], [maLoaiTaiLieu], [maTrangThai], [maSanPham]) VALUES (N'TL000039', N'Video hướng dẫn', N'Video hướng dẫn', N'ConTrai2thangr_dbc5.mp4', N'ConTrai2thangr_dbc5.mp4', 1, CAST(N'2021-07-09T00:00:00.000' AS DateTime), NULL, N'LTL002', N'TT005', N'SP004')
GO
SET IDENTITY_INSERT [dbo].[ThongBao] ON 

INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (1, N'LTB0002', N'KH002', CAST(N'2021-07-03' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (2, N'LTB0002', N'KH002', CAST(N'2021-07-02' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (3, N'LTB0002', N'KH002', CAST(N'2021-07-01' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (4, N'LTB0002', N'KH002', CAST(N'2021-07-01' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (5, N'LTB0002', N'KH001', CAST(N'2021-07-04' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (6, N'LTB0002', N'KH001', CAST(N'2021-07-04' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (7, N'LTB0002', N'KH001', CAST(N'2021-07-04' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (8, N'LTB0002', N'KH001', CAST(N'2021-11-20' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (9, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (10, N'LTB0002', N'KH001', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (11, N'LTB0002', N'KH001', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (12, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (13, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (14, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (15, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (16, N'LTB0002', N'KH002', CAST(N'2021-07-07' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (17, N'LTB0002', N'KH002', CAST(N'2021-07-08' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (18, N'LTB0002', N'KH002', CAST(N'2021-07-08' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (19, N'LTB0002', N'KH001', CAST(N'2021-07-09' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (20, N'LTB0002', N'KH001', CAST(N'2021-07-09' AS Date), 0)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (21, N'LTB0002', N'KH002', CAST(N'2021-07-09' AS Date), 1)
INSERT [dbo].[ThongBao] ([Id], [maLoaiThongBao], [maKhachHang], [thoiGian], [check]) VALUES (22, N'LTB0002', N'KH003', CAST(N'2021-07-09' AS Date), 1)
SET IDENTITY_INSERT [dbo].[ThongBao] OFF
GO
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT001', N'Chờ biên tập', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT002', N'Chờ duyệt', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT003', N'Chờ xuất bản', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT004', N'Trả về', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT005', N'Đã xuất bản', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT006', N'Huỷ', NULL)
INSERT [dbo].[TrangThai] ([maTrangThai], [tenTrangThai], [moTa]) VALUES (N'TT007', N'Thu hồi', NULL)
GO
ALTER TABLE [dbo].[BaiDang] ADD  DEFAULT ((0)) FOR [soLuongBinhLuan]
GO
ALTER TABLE [dbo].[BaiDang] ADD  DEFAULT ((0)) FOR [hot]
GO
ALTER TABLE [dbo].[BaiDang] ADD  DEFAULT ((0)) FOR [soLuongXem]
GO
ALTER TABLE [dbo].[BaiDang]  WITH CHECK ADD FOREIGN KEY([maChuDe])
REFERENCES [dbo].[ChuDe] ([maChuDe])
GO
ALTER TABLE [dbo].[BaiDang]  WITH CHECK ADD FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD FOREIGN KEY([maBaiDang])
REFERENCES [dbo].[BaiDang] ([maBaiDang])
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[ChuDe]  WITH CHECK ADD FOREIGN KEY([maNhomChuDe])
REFERENCES [dbo].[NhomChuDe] ([maNhomChuDe])
GO
ALTER TABLE [dbo].[HoiDap]  WITH CHECK ADD FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[HoiDap]  WITH CHECK ADD FOREIGN KEY([maLoai])
REFERENCES [dbo].[LoaiHoiDap] ([maLoai])
GO
ALTER TABLE [dbo].[HoiDap]  WITH CHECK ADD FOREIGN KEY([maSanPham])
REFERENCES [dbo].[SanPham] ([maSanPham])
GO
ALTER TABLE [dbo].[HoiDap]  WITH CHECK ADD FOREIGN KEY([maTrangThai])
REFERENCES [dbo].[TrangThai] ([maTrangThai])
GO
ALTER TABLE [dbo].[KeyLicense]  WITH CHECK ADD FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[KeyLicense]  WITH CHECK ADD FOREIGN KEY([maSanPham])
REFERENCES [dbo].[SanPham] ([maSanPham])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maChucDanh])
REFERENCES [dbo].[ChucDanh] ([maChucDanh])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maPhongBan])
REFERENCES [dbo].[PhongBan] ([maPhongBan])
GO
ALTER TABLE [dbo].[NV_HoiDap]  WITH CHECK ADD FOREIGN KEY([maHoiDap])
REFERENCES [dbo].[HoiDap] ([maHoiDap])
GO
ALTER TABLE [dbo].[NV_HoiDap]  WITH CHECK ADD FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[NV_Quyen]  WITH CHECK ADD FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[NV_Quyen]  WITH CHECK ADD FOREIGN KEY([maQuyen])
REFERENCES [dbo].[Quyen] ([maQuyen])
GO
ALTER TABLE [dbo].[NV_TaiLieu]  WITH CHECK ADD  CONSTRAINT [FK__NV_TaiLie__maNha__619B8048] FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[NV_TaiLieu] CHECK CONSTRAINT [FK__NV_TaiLie__maNha__619B8048]
GO
ALTER TABLE [dbo].[NV_TaiLieu]  WITH CHECK ADD  CONSTRAINT [FK__NV_TaiLie__maTai__628FA481] FOREIGN KEY([maTaiLieu])
REFERENCES [dbo].[TaiLieu] ([maTaiLieu])
GO
ALTER TABLE [dbo].[NV_TaiLieu] CHECK CONSTRAINT [FK__NV_TaiLie__maTai__628FA481]
GO
ALTER TABLE [dbo].[TaiLieu]  WITH CHECK ADD  CONSTRAINT [FK__TaiLieu__maLoaiT__6383C8BA] FOREIGN KEY([maLoaiTaiLieu])
REFERENCES [dbo].[LoaiTaiLieu] ([maLoaiTaiLieu])
GO
ALTER TABLE [dbo].[TaiLieu] CHECK CONSTRAINT [FK__TaiLieu__maLoaiT__6383C8BA]
GO
ALTER TABLE [dbo].[TaiLieu]  WITH CHECK ADD  CONSTRAINT [FK__TaiLieu__maSanPh__6477ECF3] FOREIGN KEY([maSanPham])
REFERENCES [dbo].[SanPham] ([maSanPham])
GO
ALTER TABLE [dbo].[TaiLieu] CHECK CONSTRAINT [FK__TaiLieu__maSanPh__6477ECF3]
GO
ALTER TABLE [dbo].[TaiLieu]  WITH CHECK ADD  CONSTRAINT [FK__TaiLieu__maTrang__656C112C] FOREIGN KEY([maTrangThai])
REFERENCES [dbo].[TrangThai] ([maTrangThai])
GO
ALTER TABLE [dbo].[TaiLieu] CHECK CONSTRAINT [FK__TaiLieu__maTrang__656C112C]
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[ThongBao]  WITH CHECK ADD FOREIGN KEY([maLoaiThongBao])
REFERENCES [dbo].[LoaiThongBao] ([maLoaiThongBao])
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD CHECK  (([gioiTinh]=N'Nam' OR [gioiTinh]=N'Nữ'))
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD CHECK  (([trangThai]=N'Chưa kích hoạt' OR [trangThai]=N'Đã kích hoạt' OR [trangThai]=N'Dừng hoạt động'))
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD CHECK  (([trinhDo]=N'Trung Cấp' OR [trinhDo]=N'Cao Đẳng' OR [trinhDo]=N'Đại Học' OR [trinhDo]=N'Cao Học'))
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD CHECK  (([gioiTinh]=N'Nam' OR [gioiTinh]=N'Nữ'))
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD CHECK  (([trangThai]=N'Chưa kích hoạt' OR [trangThai]=N'Đã kích hoạt' OR [trangThai]=N'Dừng hoạt động'))
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD CHECK  (([trinhDo]=N'Trung Cấp' OR [trinhDo]=N'Cao Đẳng' OR [trinhDo]=N'Đại Học' OR [trinhDo]=N'Cao Học'))
GO
