using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MGiaCa
{
    public interface IGiaCaService
    {
        public Task<ResponseOutput> addGiaCa ( GiaCaRequest request);
        public Task<List<GiaCaResponse>> GetMucGia ();
        public Task<ResponseOutput> suaGiaCa(GiaCaRequest request);
        public Task<ResponseOutput> xoaGiaCa(int magia);

    }
}
