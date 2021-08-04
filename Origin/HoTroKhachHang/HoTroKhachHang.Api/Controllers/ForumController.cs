using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MForum;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.fRepository.Functions;
using HoTroKhachHang.Application.fRepository.ResponsesForum;
using System.Dynamic;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using HoTroKhachHang.Application.fRepository.ModelsForum;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _ForumService;
        public ForumController(IForumService IForumService)
        {
            _ForumService = IForumService;
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
        public ObjectResult getBaiDangPaging(List<ForumBaiDang> data, long count)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutputPaging output = new ResponseOutputPaging();
            try
            {
                foreach (ForumBaiDang item in data)
                {
                    item.NgayDang = (DateTime.ParseExact(item.NgayDang, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    if (!string.IsNullOrEmpty(item.LanBinhLuanCuoi))
                    {
                        item.LanBinhLuanCuoi = (DateTime.ParseExact(item.LanBinhLuanCuoi, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    }
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

        public ObjectResult getBaiDang(List<ForumBaiDang> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                foreach (ForumBaiDang item in data)
                {
                    item.NgayDang = (DateTime.ParseExact(item.NgayDang, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
                    if (!string.IsNullOrEmpty(item.LanBinhLuanCuoi))
                    {
                        item.LanBinhLuanCuoi = (DateTime.ParseExact(item.LanBinhLuanCuoi, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
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

        public ObjectResult getTongQuan(ForumTongQuan data)
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
        public ObjectResult getChuDe(List<ForumResponse> data)
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

        public ObjectResult getBinhLuanBaiDang(List<ForumBaiDangComment> data)
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
        public ObjectResult getNhomChuDe(List<ForumNhomChuDe> data)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutput output = new ResponseOutput();
            try
            {
                foreach(var item in data)
                {
                    item.NgayTao = (DateTime.ParseExact(item.NgayTao, "yyyy-MM-dd", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
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

        [HttpGet("baidang")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getAllBaiDang(int pagenumber, int pagesize)
        {
            var result = await _ForumService.getAllBaiDang();
            long count = result.Count();
            result = PagedList<ForumBaiDang>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getBaiDangPaging(result,count);
        }

        [HttpGet("baidang-khachhang")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getAllBaiDangKhachHang(string makhachhang,int pagenumber, int pagesize)
        {
            var result = await _ForumService.getBaiDangKhachHang(makhachhang);
            long count = result.Count();
            result = PagedList<ForumBaiDang>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getBaiDangPaging(result, count);
        }

        [HttpGet("baidang-tieude")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getAllBaiDangTieuDe(string tieude, int pagenumber, int pagesize)
        {
            var result = await _ForumService.getBaiDangTieuDe(tieude);
            long count = result.Count();
            result = PagedList<ForumBaiDang>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getBaiDangPaging(result, count);
        }

        [HttpGet("tongquan")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getTongQuan()
        {
            var result = await _ForumService.getTongQuan();
            return getTongQuan(result);
        }

        [HttpGet("nhomchude")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> getNhomChuDe()
        {
            var result = await _ForumService.getNhomChuDe();
            return getNhomChuDe(result);
        }

        [HttpGet("nhomchudea")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getNhomChuDeA()
        {
            var result = await _ForumService.getNhomChuDeA();
            return getNhomChuDe(result);
        }

        [HttpGet("chude")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> getChuDe()
        {
            var result = await _ForumService.getChuDe();
            return getChuDe(result);
        }

        [HttpGet("binhluan-baidang")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getBinhLuanBaiDang(string mabaidang)
        {
            var result = await _ForumService.getCommentBaiDang(mabaidang);
            return getBinhLuanBaiDang(result);
        }

        [HttpPost("binhluan")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> addBinhLuan([FromBody]ForumBinhLuanMoi request)
        {
            var result = await _ForumService.addBinhLuan(request);
            return makeOutput(result);
        }

        [HttpPost("hottrend")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateHottrend([FromBody] ForumHotTrend request)
        {
            var result = await _ForumService.updateHotTrend(request);
            return makeOutput(result);
        }

        [HttpPost("soluong")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> updateSoLuong( string mabaidang)
        {
            var result = await _ForumService.updateSoLuong(mabaidang);
            return makeOutput(result);
        }


        [HttpPost("xoabaidang")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteBaiDang(string mabaidang)
        {
            var result = await _ForumService.deleteBaiDang(mabaidang);
            return makeOutput(result);
        }

        [HttpPost("xoachude")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteChude(ForumDeleteChuDe request)
        {
            var result = await _ForumService.deleteChuDe(request);
            return makeOutput(result);
        }

        [HttpPost("xoabinhluan")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteBinhLuan(int id)
        {
            var result = await _ForumService.deleteBinhLuan(id);
            return makeOutput(result);
        }

        [HttpPost("baidang")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> addBaiDang([FromBody] ForumBaiDangRequest request)
        {
            var result = await _ForumService.addBaiDang(request);
            return makeOutput(result);
        }

        [HttpPost("chude")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addChuDe([FromBody] ForumChuDeRequest request)
        {
            var result = await _ForumService.addChuDe(request);
            return makeOutput(result);
        }

        [HttpPost("nhomchude")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addNhomChuDe([FromBody] ForumNhomChuDeRequest request)
        {
            var result = await _ForumService.addNhomChuDe(request);
            return makeOutput(result);
        }
        [HttpPost("updateChuDe")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateChuDe([FromBody] ForumChuDe request)
        {
            var result = await _ForumService.updateChuDe(request);
            return makeOutput(result);
        }
        [HttpGet("baidang-machude")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> getAllBaiDangMaChuDe(string machude, int pagenumber, int pagesize)
        {
            var result = await _ForumService.getBaiDangMaChuDe(machude);
            long count = result.Count();
            result = PagedList<ForumBaiDang>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getBaiDangPaging(result, count);
        }

        [HttpGet("baidang-mabaidang")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> getAllBaiDangMaBaiDang(string mabaidang)
        {
            var result = await _ForumService.getBaiDangMaBaiDang(mabaidang);
            return getBaiDang(result);
        }
    }
    
}
