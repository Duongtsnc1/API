using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MGiaCa
{
    public class GiaCaService : IGiaCaService
    {
        private readonly CSKHContext _CSKHContext;
        public GiaCaService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }       
      
        public async Task<ResponseOutput> addGiaCa(GiaCaRequest request)
        {

            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var gia = new Gium
                {
                    Gia = request.gia
                };
                _CSKHContext.Gia.Add(gia);
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

        public async Task<List<GiaCaResponse>> GetMucGia()
        {
            
            var list = new List<GiaCaResponse>();
            try {
                var allGia = (from G in _CSKHContext.Gia
                              select new { G }).ToList();
                allGia.Sort((a, b) => Int64.Parse(a.G.Gia.ToString()).CompareTo(Int64.Parse(b.G.Gia.ToString())));
                foreach (var item in allGia)
                {
                    list.Add(new GiaCaResponse()
                    {
                        Id = item.G.Id,
                        Gia = item.G.Gia
                    });
                }
            }
            catch
            {
                return new List<GiaCaResponse>();
            }            
            return list;
        }

        public async Task<ResponseOutput> suaGiaCa(GiaCaRequest request)
        {

            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update =  _CSKHContext.Gia.FirstOrDefault(s => s.Id == request.Id);
                if (update == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                update.Gia = request.gia;
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
            }
            output.data = foo;
            output.isSuccess = true;
            output.message = "";
            return output;
        }

        public async Task<ResponseOutput> xoaGiaCa(int magia)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update = _CSKHContext.Gia.FirstOrDefault(s => s.Id == magia);
                if (update == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                
                 _CSKHContext.Gia.Remove(update);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
            }
            output.data = foo;
            output.isSuccess = true;
            output.message = "";
            return output;
        }
    }
}
