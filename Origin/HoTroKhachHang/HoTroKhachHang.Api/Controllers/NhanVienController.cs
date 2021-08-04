using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.MNhanVien;
using System.Dynamic;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _NhanVienService;

        public NhanVienController(INhanVienService NhanVienService)
        {
            _NhanVienService = NhanVienService;
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
        public ObjectResult hienThiDanhSachNhanVien(List<NhanVienResponse> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                foreach (NhanVienResponse item in data)
                {
                    if (item.NgayTuyenDung != null)
                    {
                        item.NgayTuyenDung = (DateTime.ParseExact(item.NgayTuyenDung, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    }
                    item.NgaySinh = (DateTime.ParseExact(item.NgaySinh, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
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

        public ObjectResult hienThiDanhSachQuyen(List<NvQuyen> data)
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
        public ObjectResult hienThiNhanVienExpand(List<NhanVienExpand> data)
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
        public ObjectResult nhanVienQuyen(List<NhanVienQuyen> data,long count)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutputPaging output = new ResponseOutputPaging();
            try
            {
                foreach (NhanVienQuyen item in data)
                {
                    if (item.NgayTuyenDung != null)
                    {
                        item.NgayTuyenDung = (DateTime.ParseExact(item.NgayTuyenDung, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    }
                    item.NgaySinh = (DateTime.ParseExact(item.NgaySinh, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                }
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

        [HttpGet("danhsach")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> danhSachNhanVien(int pagenumber,int pagesize)
        {
            var result = await _NhanVienService.getAll();
            result = PagedList<NhanVienResponse>.GetPagedList(result.AsQueryable(),pagenumber, pagesize);
            return hienThiDanhSachNhanVien(result);
        }
        [HttpGet("NVQuyen")]
        [Authorize(Roles = "Admin,Biên tập,Duyệt,Xuất bản")]
        public async Task<IActionResult> GetAllNVQuyen(string tenQuyen)
        {
            var result = await _NhanVienService.getAllNVQuyen(tenQuyen);           
            return hienThiNhanVienExpand(result);
        }

        [HttpGet("quyen")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> danhSachQuyen(string manhanvien)
        {
            var result = await _NhanVienService.quyenNhanVien(manhanvien);
            return hienThiDanhSachQuyen(result);
        }

        [HttpPost("xulyquyen")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> xuLyQuyen(XuLyQuyen request)
        {
            var result = await _NhanVienService.addAndUpdateQuyen(request);
            return makeOutput(result);
        }

        [HttpGet("nhanvienquyen")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> nhanVienQuyen(int pagenumber,int pagesize)
        {
            var result = await _NhanVienService.nhanVienQuyen();
            long count = result.Count();
            result = PagedList<NhanVienQuyen>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return nhanVienQuyen(result,count);
        }

        [HttpGet("danhsach-manhanvien")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> danhSachNhanVienMaNhanVien(string manhanvien)
        {
            var result = await _NhanVienService.getNhanVienMaNhanVien(manhanvien);
            return hienThiDanhSachNhanVien(result);
        }

        [HttpPost("capnhatquyen")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> capNhatQuyen([FromBody] CapNhatQuyen request)
        {
            var result = await _NhanVienService.capNhatQuyen(request);
            return makeOutput(result);
        }
    }
}
