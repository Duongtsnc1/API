using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.MNSX;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NSXController : Controller
    {
        private readonly INSXService _NSXService;
        public NSXController(INSXService iNSXService)
        {
            _NSXService = iNSXService;
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
        public ObjectResult hienThiNSX(List<NSXResponse> data)
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
        [HttpPost("themNSX")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addNSX([FromBody] NSXRequest request)
        {
            var result = await _NSXService.addNSX(request);
            return makeOutput(result);
        }
        [HttpPost("suaNSX")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> suaNSX([FromBody] NSXRequest request)
        {
            var result = await _NSXService.suaNSX(request);
            return makeOutput(result);
        }
        [HttpGet("allNSX")]
        [Authorize(Roles = "Admin,Biên tập,Duyệt,Xuất bản,Khách hàng")]
        public async Task<IActionResult> getNSX()
        {
            var result = await _NSXService.getNSX();
            return hienThiNSX(result);
        }
    }
}
