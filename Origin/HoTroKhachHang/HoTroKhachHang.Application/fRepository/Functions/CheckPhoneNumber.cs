using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Functions
{
    public class CheckPhoneNumber
    {
        public bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+?[0-9]{10,13})$").Success;
        }

    }
}
