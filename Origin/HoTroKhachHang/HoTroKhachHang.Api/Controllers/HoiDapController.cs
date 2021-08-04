using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MHoiDap;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoidapController : ControllerBase
    {
        private readonly IHoiDapService _HoiDapService;
        public HoidapController(IHoiDapService HoiDapService)
        {
            _HoiDapService = HoiDapService;
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
        public ObjectResult hienThiTimKiemHoiDap(List<TimKiemHoiDap> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                foreach (TimKiemHoiDap item in data)
                {
                    item.NgayNhan = (DateTime.ParseExact(item.NgayNhan, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    if (item.NgayXuatBan != null)
                    {
                        item.NgayXuatBan = (DateTime.ParseExact(item.NgayXuatBan, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
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

        public ObjectResult hienThiThongTinHoiDap(List<HoiDapResponse> data)
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
        public ObjectResult hienThiThongTinHoiDapExpand(List<HoiDapExpand> data,long count)
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

        public ObjectResult hienThiThongTinHoiDapSupport(List<HoiDapSupport> data)
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

        public ObjectResult hienThiThongTinHoiDapPaging(List<HoiDapResponse> data, long count)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutputPaging output = new ResponseOutputPaging();
            try
            {
                output.count = count;
                output.data = data;
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

        public ObjectResult getMaSanPhamMaHoiDap(string data)
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

        [Route("khachhang")]
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getHoiDapKhachHang(string tenkhachhang,int pagenumber,int pagesize)
        {
            var result = await _HoiDapService.getHoiDapKhachHang(tenkhachhang);
            result = PagedList<TimKiemHoiDap>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiTimKiemHoiDap(result);
        }

        [Route("nhanvien")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getHoiDapNhanVien(string tennhanvien,int pagenumber, int pagesize)
        {
            var result = await _HoiDapService.getHoiDapNhanVien(tennhanvien);
            result = PagedList<TimKiemHoiDap>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiTimKiemHoiDap(result);
        }

        [Route("sanpham")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getHoiDapSanPham(string tensanpham, int pagenumber, int pagesize)
        {
            var result = await _HoiDapService.getHoiDapSanPham(tensanpham);
            result = PagedList<TimKiemHoiDap>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiTimKiemHoiDap(result);
        }

        [HttpPost]
        [Authorize(Roles ="Khách hàng")]
        public async Task<IActionResult> addCauHoi([FromBody] HoiDapRequest request)
        {
            var result = await _HoiDapService.addCauHoi(request);
            return makeOutput(result);
        }

        [Route("hoidap-xuly")]
        [HttpPost]
        [Authorize(Roles = "Biên tập, Duyệt, Xuất bản")]
        public async Task<IActionResult> xuLyCauHoi([FromBody] HoiDapRequest request)
        {
            var result = await _HoiDapService.xuLyCauHoi(request);
            return makeOutput(result);
        }

        [HttpPost("trangthai-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiTrangThaiTheoQuyen(TenQuyen tenquyen)
        {
            var result = await _HoiDapService.danhSachSoLuongTrangThai(tenquyen);
            return hienThiTrangThaiTheoQuyen(result);
        }

        [HttpPost("hoidap-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiCauHoiTheoQuyen(TenQuyenPaging tenquyen)
        {
            var result = await _HoiDapService.danhSachCauHoiTheoQuyen(tenquyen);
            var count = result.Count;
            result = PagedList<HoiDapExpand>.GetPagedList(result.AsQueryable(), tenquyen.pagenumber, tenquyen.pagesize);
            return hienThiThongTinHoiDapExpand(result,count);
        }

        [HttpGet("hoidap-trangthai")]
        [Authorize]
        public async Task<IActionResult> timKiemCauHoiTheoTrangThai(string matrangthai, int pagenumber,int pagesize,string mabientap, string maloaihoidap, string masanpham)
        {
            var result = await _HoiDapService.getHoiDapTrangThai(matrangthai,  mabientap,  maloaihoidap,  masanpham);
            result = PagedList<HoiDapExpand>.GetPagedList(result.AsQueryable(),pagenumber,pagesize);
            var count = result.Count;
            return hienThiThongTinHoiDapExpand(result,count);
        }

        [HttpGet("hoidap-mahoidap")]
        [Authorize(Roles ="Biên tập,Duyệt,Xuất bản,Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoMaHoiDap(string mahoidap)
        {
            var result = await _HoiDapService.getHoiDapMaHoiDap(mahoidap);
            return hienThiThongTinHoiDapSupport(result);
        }

        [HttpGet("hoidap-maloaihoidap")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoMaLoaiHoiDap(string maloaihoidap)
        {
            var result = await _HoiDapService.getHoiDapMaLoaiHoiDap(maloaihoidap);
            return hienThiThongTinHoiDap(result);
        }

        [HttpGet("hoidap-makhachhang")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoMaKhachHang(string makhachhang)
        {
            var result = await _HoiDapService.getHoiDapMaKhachHang(makhachhang);
            return hienThiThongTinHoiDap(result);
        }

        [HttpGet("hoidap-masanpham-maloaihoidap")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoMaSanPhamMaLoaiHoiDap(string masanpham,string maloaihoidap, int pagenumber, int pagesize)
        {
            var result = await _HoiDapService.getHoiDapMaSanPhamMaLoaiHoiDap(masanpham,maloaihoidap);
            long count = result.Count();
            result = PagedList<HoiDapResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiThongTinHoiDapPaging(result,count);
        }

        [HttpGet("hoidap-masanpham")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoMaSanPham(string masanpham,int pagenumber,int pagesize)
        {
            var result = await _HoiDapService.getHoiDapMaSanPham(masanpham);
            long count = result.Count();
            result = PagedList<HoiDapResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiThongTinHoiDapPaging(result,count);
        }

        [HttpGet("hoidap-tieude")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> timKiemCauHoiTheoTieuDe(string tieude, int pagenumber, int pagesize)
        {
            var result = await _HoiDapService.getHoiDapTieuDe(tieude);
            long count = result.Count();
            result = PagedList<HoiDapResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return hienThiThongTinHoiDapPaging(result,count);
        }

        [HttpGet("masanpham")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> hienThiMaSanPhamMaHoiDap(string mahoidap)
        {
            var result = await _HoiDapService.getMaSanPhamMaHoiDap(mahoidap);
            return getMaSanPhamMaHoiDap(result.ToString());
        }
        [HttpGet("hoidap-delete")]
        [Authorize(Roles = "Xuất bản")]
        public async Task<IActionResult> deleteHoiDap(string mahoidap)
        {
            var result = await _HoiDapService.deleteHoiDap(mahoidap);
            return makeOutput(result);
        }
    }
}
