using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManageSystem.SessionAPI;

namespace HoTroKhachHang.Application.MIdentity
{
    public static class IdentityGlobalServiceToken
    {
        public static SessionAPIAsync session = new SessionAPIAsync();
        public static string Token { get; set; }
        public static Guid UserID { get; set; }
        public static Guid CustomerID { get; set; }
    }
}
