using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MNSX
{
    public class NSXService : INSXService
    {
        private readonly CSKHContext _CSKHContext;
        public NSXService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string themMaNSX()
        {
            var check = from HD in _CSKHContext.Nsxes
                        select HD;
            if (check.ToList().Count == 0)
            {
                return "SX0_00000001";
                //HD00000001
            }
            var query = (from MHD in _CSKHContext.Nsxes
                         orderby MHD.MaNsx descending
                         select MHD.MaNsx).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 2);
            int so = Int32.Parse(join_lastrecord.Substring(4, join_lastrecord.Length - 4));
            so += 1;
            string tempso = so.ToString();

            int middle = Int32.Parse(join_lastrecord.Substring(2, 1));
            if (tempso == "100000000")
            {
                middle = middle + 1;
                tempso = "00000001";
            }
            string tempmiddle = middle.ToString();
            join_lastrecord = chu + tempmiddle + "_" + tempso.PadLeft(8, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> addNSX(NSXRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var nsx = new Nsx()
                {
                    Mota = request.mota,
                    TenNsx = request.tennsx,
                    MaNsx= themMaNSX()
                };
                _CSKHContext.Nsxes.Add(nsx);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            output.message = "";
            output.isSuccess = true;
            output.data = foo;
            return output;
        }

        public async Task<List<NSXResponse>> getNSX()
        {
            var list = new List<NSXResponse>();
            try
            {
                var query = from NSX in _CSKHContext.Nsxes
                            select new { NSX };
                foreach(var item in query)
                {
                    list.Add(new NSXResponse()
                    {
                        mansx = item.NSX.MaNsx,
                        mota = item.NSX.Mota,
                        tennsx = item.NSX.TenNsx
                    });
                }
            }
            catch
            {
                return new List<NSXResponse>();
            }
            return list;
        }

        public async Task<ResponseOutput> suaNSX(NSXRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check = _CSKHContext.Nsxes.FirstOrDefault(s=>s.MaNsx==request.mansx);
                if (check == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                check.Mota = request.mota;
                check.TenNsx = request.tennsx;
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.message = ex.ToString();
                output.isSuccess = false;
                return output;
            }
            output.message = "";
            output.isSuccess = true;
            output.data = foo;
            return output;
        }
    }
}
