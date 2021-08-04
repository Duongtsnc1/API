using HoTroKhachHang.Application.fRepository.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MTaiLieu;
using HoTroKhachHang.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using System.Globalization;
using AccountManageSystem.SessionAPI;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiLieuController : ControllerBase
    {
        private readonly ITaiLieuService _TaiLieuService;
        
        public TaiLieuController(ITaiLieuService TaiLieuService)
        {
            _TaiLieuService = TaiLieuService;
        }
        public OkObjectResult makeOutputPaging(ResponseOutputPaging Data)
        {
            dynamic foo = new ExpandoObject();
            if (Data.isSuccess)
            {
                foo.message = "Success";
                foo.error = Data.message;
                foo.count = Data.count;
                foo.status = 200;
                foo.data = Data.data;
            }
            else
            {
                foo.message = "Error";
                foo.error = Data.message;
                foo.count = Data.count;
                foo.status = 400;
                foo.data = Data.data;
            }

            return Ok(foo);
        }
        public OkObjectResult makeOutput(ResponseOutput Data)
        {
            dynamic foo = new ExpandoObject();
            if (Data.isSuccess)
            {
                foo.message = "Success";
                foo.error = Data.message;
                foo.status = 200;
                foo.data = Data.data;
            }
            else
            {
                foo.message = "Error";
                foo.error = Data.message;
                foo.status = 400;
                foo.data = Data.data;
            }

            return Ok(foo);
        }

        public ObjectResult hienThiLoaiTaiLieuTheoSanPham(List<TaiLieuTheoSanPham> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiTimKiemMaTaiLieu(List<TaiLieuTheoMaTaiLieu> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiSPham(List<SPham> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiLTaiLieu(List<LTaiLieu> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiTaiLieuTrangThai(List<TaiLieuResponse> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                foreach (TaiLieuResponse item in data)
                {
                    if (item.NgayThang != null)
                    {
                        item.NgayThang = (DateTime.ParseExact(item.NgayThang, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    }
                }
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiTrangThaiTheoQuyen(List<DanhSachSoLuongTheoTrangThai> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        public ObjectResult hienThiTaiLieuTheoQuyen(List<TaiLieuResponse> data,long count)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutputPaging output = new ResponseOutputPaging();
            try
            {
                output.data = data;
                output.count = count;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                output.count = 0;
                return makeOutputPaging(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutputPaging(output);
        }

        public ObjectResult hienThiListLink(List<linkDownload> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        [HttpPost]
        [Authorize(Roles = "Biên tập")]
        public async Task<IActionResult> addTaiLieu([FromBody] TaiLieuRequest request)
        {
            var result = await _TaiLieuService.addTaiLieu(request);
            return makeOutput(result);
        }

        [HttpGet("sanpham")]
        [Authorize(Roles ="Khách hàng")]
        public async Task<IActionResult> getLoaiTaiLieuSanPham(string masanpham,string maloaitailieu)
        {
            var result = await _TaiLieuService.searchLoaiTaiLieuSanPham(masanpham, maloaitailieu);
            return hienThiLoaiTaiLieuTheoSanPham(result);
        }

        //[HttpGet("url")]
        ////[Authorize(Roles ="")]
        //public async Task<IActionResult> getURL(string tenFile)
        //{
        //    var result = await _TaiLieuService.getURL(tenFile);
        //    return makeOutput(result);
        //}

        [HttpGet("tentrangthai")]
        [Authorize(Roles ="Biên tập, Duyệt, Xuất bản")]
        public async Task<IActionResult> thongKeTheoTrangThai(string matrangthai)
        {
            var result = await _TaiLieuService.thongKeTheoTrangThai(matrangthai);
            return makeOutput(result);
        }
        [HttpPost("duyet")]
        [Authorize(Roles ="Duyệt")]
        public async Task<IActionResult> duyetTaiLieu(XuLyTaiLieu request)
        {
            var result = await _TaiLieuService.duyetTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("trave")]
        [Authorize(Roles ="Duyệt")]
        public async Task<IActionResult> traVeTaiLieu(XuLyTaiLieu request)
        {
            var result = await _TaiLieuService.traVeTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("guiduyet")]
        [Authorize(Roles ="Biên tập")]
        public async Task<IActionResult> guiDuyetTaiLieu(XuLyTaiLieu request)
        {
            var result = await _TaiLieuService.guiDuyetTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("thuhoi")]
        [Authorize(Roles = "Duyệt")]
        public async Task<IActionResult> thuHoiTaiLieu(XuLyTaiLieu request)
        {
            var result = await _TaiLieuService.thuHoiTaiLieu(request);
            return makeOutput(result);
        }

        [HttpGet("matailieu")]
        [Authorize(Roles ="Biên tập,Duyệt,Khách hàng")]
        public async Task<IActionResult> getMaTaiLieu(string matailieu)
        {
            var result = await _TaiLieuService.searchMaTaiLieuTaiLieu(matailieu);
            return hienThiTimKiemMaTaiLieu(result);
        }

        [HttpGet("ltailieu")]
        [Authorize(Roles = "Biên tập")]
        public async Task<IActionResult> getLTaiLieu()
        {
            var result = await _TaiLieuService.hienThiLoaiTaiLieu();
            return hienThiLTaiLieu(result);
        }

        [HttpGet("spham")]
        [Authorize(Roles ="Biên tập")]
        public async Task<IActionResult> getSPham()
        {
            var result = await _TaiLieuService.hienThiSanPham();
            return hienThiSPham(result);
        }

        [HttpPost("huy")]
        [Authorize(Roles ="Biên tập")]
        public async Task<IActionResult> huyTaiLieu(XuLyTaiLieu request)
        {
            var result = await _TaiLieuService.huyTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("trangthai-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiTrangThaiTheoQuyen(TenQuyen tenquyen)
        {
            var result = await _TaiLieuService.danhSachSoLuongTrangThai(tenquyen);
            return hienThiTrangThaiTheoQuyen(result);
        }

        [HttpPost("tailieu-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiTaiLieuTheoQuyen(TenQuyenPaging tenquyen)
        {
            var result = await _TaiLieuService.danhSachTaiLieuTheoQuyen(tenquyen);
            var count = result.Count;
            result = PagedList<TaiLieuResponse>.GetPagedList(result.AsQueryable(), tenquyen.pagenumber, tenquyen.pagesize);
            return hienThiTaiLieuTheoQuyen(result,count);
        }

        [HttpGet("tailieu-trangthai")]
        [Authorize]
        public async Task<IActionResult> timKiemTaiLieuTheoTrangThai(string matrangthai, int pagenumber,int pagesize, string maBienTap, string maSanPham)
        {
            var result = await _TaiLieuService.getTaiLieuTrangThai(matrangthai, maBienTap, maSanPham);
            var count = result.Count;
            result = PagedList<TaiLieuResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiTaiLieuTheoQuyen(result,count);
        }

        [HttpGet("listlink")]
        [Authorize(Roles ="Khách hàng")]
        public async Task<IActionResult> listLinkDownload(string masanpham)
        {
            var result = await _TaiLieuService.listLinkDownload(masanpham);
            return hienThiListLink(result);
        }

        public ObjectResult SanPhamTheoKhachHang(List<ProductDTO> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                output.data = data;
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return makeOutput(output);
            }

            output.isSuccess = true;
            output.message = "";
            return makeOutput(output);
        }

        [HttpGet("khachhang-sanpham")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> SanPhamTheoKhachHang()
        {
            var result = await _TaiLieuService.SanPhamTheoKhachHang();
            return SanPhamTheoKhachHang(result);
        }

        [HttpGet("delete-tailieu")]
        [Authorize(Roles = "Duyệt")]
        public async Task<IActionResult> deleteQuangCao(string maTaiLieu)
        {
            var result = await _TaiLieuService.deleteTaiLieu(maTaiLieu);
            return makeOutput(result);

        }
    }
}
