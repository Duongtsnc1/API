using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MNganhHang
{
    public interface INganhHangService
    {
        public Task<List<NganhHangResponse>> GetAll();
        public Task<ResponseOutput> addNganhHang(NganhHangRequest request);
        public Task<ResponseOutput> updateNganhHang(NganhHangRequest request);
        public Task<List<NganhHangExpand>> GetNganhHang_QuangCao();
        public Task<List<NganhHangExpand>> Search(string maNganhHang, string diaDiem, string xuatXu, int maGia,int status);
        public Task<ResponseOutput> deleteNganhHang(string maNganhHang);
    }
}
