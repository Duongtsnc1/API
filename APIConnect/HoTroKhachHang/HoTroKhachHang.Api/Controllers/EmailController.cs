using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoTroKhachHang.Email;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Dynamic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HoTroKhachHang.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _EmailSender;
        public EmailController(IEmailSender EmailSender)
        {
            _EmailSender = EmailSender;
        }
        public OkObjectResult makeOutput()
        {
            dynamic foo = new ExpandoObject();
            foo.content = "Success!!";
            return Ok(foo);
        }

        [HttpGet]
        public async Task<IActionResult> sendEmail()
        {
            var message = new Message(new string[] { "riseofkingdom011@gmail.com" }, "aaaa","aaaa");

            await _EmailSender.SendEmailAsync(message);
            return makeOutput() ;
        }
    }
}
