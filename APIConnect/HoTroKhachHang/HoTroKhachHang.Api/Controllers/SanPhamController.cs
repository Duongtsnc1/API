using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MSanPham;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using AccountManageSystem.SessionAPI;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _SanPhamService;

        public SanPhamController(ISanPhamService SanPhamService)
        {
            _SanPhamService = SanPhamService;
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

        public ObjectResult hienThiSanPham(List<SanPhamResponse> data)
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

        public ObjectResult hienThiSanPhamVCS(List<ProductDTO> data)
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
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản,Admin")]
        public async Task<IActionResult> getSanPham()
        {
            var result = await _SanPhamService.getAll();
            return hienThiSanPhamVCS(result);
        }

        [HttpGet("masanpham")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> getLoaiTaiLieuSanPham(string masanpham)
        {
            var result = await _SanPhamService.getMaSanPham(masanpham);
            return hienThiSanPham(result);
        }
    }
}
