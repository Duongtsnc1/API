using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Responses
{
    public class WebService
    {
        public string ServerToken { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public List<string> GroupRoles { get; set; }
    } 
}
