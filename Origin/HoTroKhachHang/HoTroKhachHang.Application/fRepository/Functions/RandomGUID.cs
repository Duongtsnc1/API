using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.fRepository.Functions
{
    public class RandomGUID
    {
        public Guid GUID()
        {
            Guid messageId = System.Guid.NewGuid();
            return messageId;
        }
    }
}
