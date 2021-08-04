using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Functions
{
    public class ConvertDateTime
    {
        public string convertStringDateTime(string dt)
        {
            //MM/dd/yyyy hh:mm:ss tt
            //------12/1/2020 12:00:00 AM - 06/12/2020 12:00:00 PM - 9/2/2021 12:00:00 AM
            string res = "";
            if (dt.Substring(1, 1) == "/" && dt.Length == 20)
            {
                dt = dt.PadLeft(21, '0');
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(2, 1) == "/" && dt.Length == 21)
            {
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(1, 1) == "/" && dt.Length == 21)
            {
                res = dt.PadLeft(22, '0');
            }
            else
            {
                res = dt;
            }
            return res;
        }
    }
}
