using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Functions
{
    public class RandomPassword
    {
        public string randomString()
        {
            Random random = new Random();
            var stringChars = new char[8];
            string src = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = src[random.Next(src.Length)];
            }
            string stringrandom = new string(stringChars);
            return stringrandom;
        }
    }
}
