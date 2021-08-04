using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Responses
{
    public class ResponseOutputPaging
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public long count { get; set; }
        public dynamic data { get; set; }
    }
}
