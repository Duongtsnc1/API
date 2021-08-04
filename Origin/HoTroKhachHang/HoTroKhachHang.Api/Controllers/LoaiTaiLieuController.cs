using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MLoaiTaiLieu;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiTaiLieuController : ControllerBase
    {
        private readonly  ILoaiTaiLieuService _LoaiTaiLieuService;
        public LoaiTaiLieuController(ILoaiTaiLieuService LoaiTaiLieuService)
        {
            _LoaiTaiLieuService = LoaiTaiLieuService;
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

        public ObjectResult getAllLoaiTaiLieu(List<LoaiTaiLieuResponse> data)
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

        [HttpGet]
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản")]
        public async Task<IActionResult> getLoaiTaiLieu()
        {
            var result = await _LoaiTaiLieuService.getAll();
            return getAllLoaiTaiLieu(result);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getLoaiTaiLieuAdmin(int pagenumber, int pagesize)
        {
            var result = await _LoaiTaiLieuService.getAllAdmin();
            result = PagedList<LoaiTaiLieuResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getAllLoaiTaiLieu(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addLoaiTaiLieu([FromBody] LoaiTaiLieuRequest request)
        {
            var result = await _LoaiTaiLieuService.addLoaiTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("capnhat")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateLoaiTaiLieu([FromBody] LoaiTaiLieuRequest request)
        {
            var result = await _LoaiTaiLieuService.updateLoaiTaiLieu(request);
            return makeOutput(result);
        }

        [HttpPost("trangthai")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> trangThaiLoaiTaiLieu([FromBody] LoaiTaiLieuRequest request)
        {
            var result = await _LoaiTaiLieuService.trangThaiLoaiTaiLieu(request);
            return makeOutput(result);
        }

        [HttpGet("loaitl-maloaitl")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> searchLoaiTaiLieuMaLoaiTaiLieu(string maloaitailieu)
        {
            var result = await _LoaiTaiLieuService.searchLoaiTaiLieuMaLoaiTaiLieu(maloaitailieu);
            return getAllLoaiTaiLieu(result);
        }
    }
}
