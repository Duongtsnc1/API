using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MLoaiHoiDap;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiHoiDapController : ControllerBase
    {
        private readonly ILoaiHoiDapService _LoaiHoiDapService;
        public LoaiHoiDapController(ILoaiHoiDapService LoaiHoiDapService)
        {
            _LoaiHoiDapService = LoaiHoiDapService;
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

        public ObjectResult getAllLoaiHoiDap(List<LoaiHoiDapResponse> data)
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

        public ObjectResult getAllLoaiHoiDapSupport(List<LHoiDap> data)
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
        public async Task<IActionResult> getLoaiHoiDap()
        {
            var result = await _LoaiHoiDapService.getAll();
            return getAllLoaiHoiDap(result);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getLoaiHoiDapAdmin(int pagenumber,int pagesize)
        {
            var result = await _LoaiHoiDapService.getAllAdmin();
            result = PagedList<LoaiHoiDapResponse>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
            return getAllLoaiHoiDap(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addLoaiHoiDap([FromBody] LoaiHoiDapRequest request)
        {
            var result = await _LoaiHoiDapService.addLoaiHoiDap(request);
            return makeOutput(result);
        }

        [HttpPost("capnhat")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateLoaiHoiDap([FromBody] LoaiHoiDapRequest request)
        {
            var result = await _LoaiHoiDapService.updateLoaiHoiDap(request);
            return makeOutput(result);
        }

        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> trangThaiLoaiHoiDap([FromBody] LoaiHoiDapRequest request)
        {
            var result = await _LoaiHoiDapService.deleteLoaiHoiDap(request);
            return makeOutput(result);
        }

        [HttpGet("loaihd-tenloaihd")]
        [Authorize(Roles = "Biên tập,Duyệt,Xuất bản")]
        public async Task<IActionResult> getMaLoaiHoiDapTenLoaiHoiDap()
        {
            var result = await _LoaiHoiDapService.getMaLoaiHoiDapTenLoaiHoiDap();
            return getAllLoaiHoiDapSupport(result);
        }

        [HttpGet("loaihd-maloaihd")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> searchLoaiHoiDapMaLoaiHoiDap(string maloaihoidap)
        {
            var result = await _LoaiHoiDapService.searchLoaiHoiDapMaLoaiHoiDap(maloaihoidap);
            return getAllLoaiHoiDap(result);
        }
    }
}
