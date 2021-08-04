using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.MGiaCa;
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
    public class GiaCaController : Controller
    {
        private readonly IGiaCaService _giaCaService;
        public GiaCaController(IGiaCaService giaCaService)
        {
            _giaCaService = giaCaService;
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
        public ObjectResult hienThiGiaCa(List<GiaCaResponse> data)
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
       
        [HttpPost("themGia")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addGiaca([FromBody]GiaCaRequest request)
        {
            var result = await _giaCaService.addGiaCa(request);
            return makeOutput(result);
        }
        [HttpPost("suaGia")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> suaGiaca([FromBody] GiaCaRequest request)
        {
            var result = await _giaCaService.suaGiaCa(request);
            return makeOutput(result);
        }
        [HttpGet("allGia")]
        [Authorize(Roles = "Admin,Biên tập,Duyệt,Xuất bản,Khách hàng")]
        public async Task<IActionResult> getGiaCa()
        {
            var result = await _giaCaService.GetMucGia();
            return hienThiGiaCa(result);
        }
        [HttpPost("xoaGia")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> xoaGia(int maGia)
        {
            var result = await _giaCaService.xoaGiaCa(maGia);
            return makeOutput(result);
        }
    }
}
