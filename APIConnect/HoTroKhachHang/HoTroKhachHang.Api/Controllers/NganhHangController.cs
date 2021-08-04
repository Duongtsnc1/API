using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.MNganhHang;
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
    public class NganhHangController : Controller
    {
        private readonly INganhHangService _nganhHangService;
        public NganhHangController(INganhHangService nganhHangService)
        {
            _nganhHangService = nganhHangService;
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
        
        public ObjectResult hienThiNganhHang(List<NganhHangResponse> data)
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
        public ObjectResult hienThiNganhHangExpand(List<NganhHangExpand> data)
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
        [HttpGet("allNganhHang")]
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản")]
        public async Task<IActionResult> getNganhHang()
        {
            var result = await _nganhHangService.GetAll();
            return hienThiNganhHang(result);
        }

        [HttpPost("addNganhHang")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addNganhHang(NganhHangRequest request)
        {
            var result = await _nganhHangService.addNganhHang(request);
            return makeOutput(result);
        }

        [HttpPost("updateNganhHang")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateNganhHang(NganhHangRequest request)
        {
            var result = await _nganhHangService.updateNganhHang(request);
            return makeOutput(result);
        }
        [HttpGet("nganhhang-quangcao")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> GetNganhHang_QuangCao()
        {
            var result = await _nganhHangService.GetNganhHang_QuangCao();
            return hienThiNganhHangExpand(result);
        }

        [HttpGet("Search")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> Search(string maNSX,string maNganhHang, string diaDiem,int status, int maGia = 0)
        {
            var result = await _nganhHangService.Search( maNganhHang,diaDiem,maNSX, maGia, status);
            return hienThiNganhHangExpand(result);
        }

        [HttpGet("delete")]
        [Authorize(Roles = "Admin,Khách hàng")]
        public async Task<IActionResult> deleteNganhHang(string manganhhang)
        {
            var result = await _nganhHangService.deleteNganhHang(manganhhang);
            return makeOutput(result);
        }
    }
}
