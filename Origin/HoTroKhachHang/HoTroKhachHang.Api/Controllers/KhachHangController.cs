using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MKhachHang.DangNhap;
using HoTroKhachHang.Application.MKhachHang.DangKi;
using HoTroKhachHang.Application.MKhachHang.KhachHangDB;
using System.Dynamic;
using HoTroKhachHang.Application.fRepository.Responses;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IDangNhapService _DangNhapService;
        private readonly IDangKiService _DangKiService;
        private readonly IKhachHangDBService _KhachHangDBService;
        public KhachHangController(IDangNhapService DangNhapService, IDangKiService DangKiService,IKhachHangDBService KhachHangDBService)
        {
            _DangNhapService = DangNhapService;
            _DangKiService = DangKiService;
            _KhachHangDBService = KhachHangDBService;
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

        public ObjectResult hienThiThongTinKhachHang(List<KhachHangDBInformations> data)
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
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DangKiRequest request)
        {
            var result = await _DangKiService.Register(request);
            return makeOutput(result);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] DangNhapRequest request)
        {
            var result = await _DangNhapService.LoginAsync(request);
            return makeOutput(result);
        }

        [Route("informations")]
        [HttpPost]
        [Authorize(Roles ="Khách hàng")]
        public async Task<IActionResult> updateInformations([FromBody] KhachHangDBInformations request)
        {
            var result = await _KhachHangDBService.updateInformation(request);
            return makeOutput(result);
        }

        [HttpGet("getinformations")]
        [Authorize(Roles ="Khách hàng")]
        public async Task<IActionResult> getInformations(string makhachhang)
        {
            var result = await _KhachHangDBService.getInformations(makhachhang);
            return hienThiThongTinKhachHang(result);
        }
    }
}
