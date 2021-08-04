using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MQuangCao;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.Models.Pagination;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuangCaoController : Controller
    {
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
        private readonly IQuangCaoService _QuangCaoService;
        public QuangCaoController(IQuangCaoService QuangCaoService)
        {
            _QuangCaoService = QuangCaoService;
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
        public ObjectResult hienThiThongTinQuangCao(List<QuangCaoResponse> data)
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

        public ObjectResult hienThiThongTinQuangCaoExpand(List<QuangCaoExpand> data,long count)
        {
            dynamic foo = new ExpandoObject();
            ResponseOutputPaging output = new ResponseOutputPaging();
            try
            {
                output.data = data;
                output.count = count;
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
        public ObjectResult hienThiThongTinQuangCaoClient(List<QuangCaoClient> data)
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
        public ObjectResult hienThiQuangCao(List<string> data)
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

        public ObjectResult hienThiTrangThaiTheoQuyen(List<DanhSachSoLuongTheoTrangThai> data)
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

        // ----------GET--------------

        [HttpGet("getByID")]
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản,Admin")]
        public async Task<IActionResult> getByID(string maQuangCao)
        {
            try
            {
                var result = await _QuangCaoService.getById(maQuangCao);
                return hienThiThongTinQuangCao(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }

        }
        [HttpGet("delete-quangcao")]
        [Authorize(Roles = "Xuất bản")]
        public async Task<IActionResult> deleteQuangCao(string maQuangCao)
        {
            var result = await _QuangCaoService.deleteQuangCao(maQuangCao);
            return makeOutput(result);   
                     
        }

        [HttpGet("getQuangCaoClient")]
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản,Admin")]
        public async Task<IActionResult> getQuangCaoClient(string maQuangCao)
        {
            try
            {
                var result = await _QuangCaoService.getQuangCaoClient(maQuangCao);
                return hienThiThongTinQuangCao(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }
        }
        [HttpGet("quangcao-nganhhang")]
        [Authorize(Roles = "Khách hàng,Biên tập,Duyệt,Xuất bản,Admin")]
        public async Task<IActionResult> getByIdNganhHang(string manganhhang)
        {
            try
            {
                var result = await _QuangCaoService.getByIdNganhHang(manganhhang);
                return hienThiThongTinQuangCao(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }

        }

        [HttpGet("quangcao-trangthai")]
        [Authorize]
        public async Task<IActionResult> timKiemQuangCaoTheoTrangThai(string matrangthai, int pagenumber,int status, int pagesize,string manganhhang,string mabientap)
        {
            try
            {
                var result = await _QuangCaoService.getQuangCaoTrangThai(matrangthai,manganhhang,mabientap,status);
                var count = result.Count;
                result = PagedList<QuangCaoExpand>.GetPagedList(result.AsQueryable(), pagenumber, pagesize);
                return hienThiThongTinQuangCaoExpand(result,count);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }
        }

        [HttpGet("getHienThi")]
        [Authorize]
        public async Task<IActionResult> getHienThiQuangCao()
        {
            try
            {
                var result = await _QuangCaoService.getHienThiQuangCao();
                return hienThiThongTinQuangCao(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }
        }

        // ----------POST--------------
        [Route("themquangcao")]
        [HttpPost]
        [Authorize(Roles = "Khách hàng,Biên tập")]
        public async Task<IActionResult> addQuangCao([FromBody] QuangCaoRequest request)
        {
            try
            {
                var result = await _QuangCaoService.addQuangCao(request);
                return makeOutput(result);
            }catch(Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }
            
        }

        [Route("themquangcao-bientap")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> addQuangCaoBienTap([FromBody] QuangCaoRequest request)
        {
            try
            {
                var result = await _QuangCaoService.addQuangCaoBienTap(request);
                return makeOutput(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }

        }

        [HttpPost("trangthai-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiTrangThaiTheoQuyen(TenQuyen tenquyen)
        {
            var result = await _QuangCaoService.danhSachSoLuongTrangThai(tenquyen);
            return hienThiTrangThaiTheoQuyen(result);
        }

        [HttpPost("quangcao-quyen")]
        [Authorize]
        public async Task<IActionResult> hienThiQuangCaoTheoQuyen(TenQuyenPaging tenquyen)
        {
            var result = await _QuangCaoService.danhSachQuangCaoTheoQuyen(tenquyen);
            var count = result.Count;
            result = PagedList<QuangCaoExpand>.GetPagedList(result.AsQueryable(), tenquyen.pagenumber, tenquyen.pagesize);
            return hienThiThongTinQuangCaoExpand(result,count);
        }

        [HttpPost("xuly-quangcao")]
        [Authorize(Roles = "Biên tập,Duyệt,Xuất bản,Admin")]
        public async Task<IActionResult> xulyQuangCao(QuangCaoRequest request)
        {
            try
            {
                var result = await _QuangCaoService.xuLyQuangCao(request);
                return makeOutput(result);
            }
            catch (Exception ex)
            {
                dynamic foo = new ExpandoObject();
                foo.message = "Error";
                foo.error = ex.Message;
                foo.status = 400;
                foo.data = null;
                return Ok(foo);
            }
        }
        [HttpGet("cungNganhHang")]
        [Authorize(Roles = "Khách hàng")]
        public async Task<IActionResult> SameNganhHang(string maQuangCao)
        {
            var result = await _QuangCaoService.SameNganhHang(maQuangCao);
            return hienThiThongTinQuangCaoClient(result);
        }

        

    }
}
