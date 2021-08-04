using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Functions
{
    public class RemoveSpace
    {
        public string loaiBoKhoangTrang(string chuoi)
        {
            string sent = chuoi;
            sent = sent.Trim(); // Xóa đầu cuối
            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
            sent = trimmer.Replace(sent, " ");
            return sent;
        }
    }
}
