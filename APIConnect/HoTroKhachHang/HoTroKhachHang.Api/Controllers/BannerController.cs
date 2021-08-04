using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MBanner;
using HoTroKhachHang.Application.fRepository.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _BannerService;
        public BannerController(IBannerService BannerService)
        {
            _BannerService = BannerService;
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

        public ObjectResult hienThiBanner(List<BannerResponse> data)
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
        [Authorize(Roles = "Admin, Khách hàng")]
        public async Task<IActionResult> getAll()
        {
            var result = await _BannerService.getAll();
            return hienThiBanner(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addBanner([FromBody] BannerRequest request)
        {
            var result = await _BannerService.addBanner(request);
            return makeOutput(result);
        }

        [HttpGet("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> deleteBanner(int id)
        {
            var result = await _BannerService.deleteBanner(id);
            return makeOutput(result);
        }
    }
}
