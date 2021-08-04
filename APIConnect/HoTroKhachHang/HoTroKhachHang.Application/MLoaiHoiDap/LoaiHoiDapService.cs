using HoTroKhachHang.Application.fRepository.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.MLoaiHoiDap;
using System.Globalization;
using System.Dynamic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HoTroKhachHang.Application.MLoaiHoiDap
{
    public class LoaiHoiDapService : ILoaiHoiDapService
    {
        private readonly CSKHContext _CSKHContext;
        public LoaiHoiDapService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string themMaLoaiHoiDap()
        {
            var check = from LHD in _CSKHContext.LoaiHoiDaps
                        select LHD;
            if (check == null)
            {
                return "LHD001";
            }
            var query = (from MLHD in _CSKHContext.LoaiHoiDaps
                         orderby MLHD.MaLoai descending
                         select MLHD.MaLoai).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 3);
            int so = Int32.Parse(join_lastrecord.Substring(3, join_lastrecord.Length - 3));
            so += 1;
            string tempso = so.ToString();
            join_lastrecord = chu + tempso.PadLeft(3, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> addLoaiHoiDap(LoaiHoiDapRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var lhd = new LoaiHoiDap
                {
                    MaLoai = themMaLoaiHoiDap(),
                    TenLoai = request.TenLoai,
                    MoTa = request.MoTa
                };
                _CSKHContext.LoaiHoiDaps.Add(lhd);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
            output.isSuccess = true;
            output.message = "";
            output.data = foo;
            return output;
        }

        public async Task<ResponseOutput> deleteLoaiHoiDap(LoaiHoiDapRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var deleteLoaiHoiDap = await _CSKHContext.LoaiHoiDaps.FindAsync(request.MaLoai);
                if (deleteLoaiHoiDap == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                var deleteHoiDap = (from HD in _CSKHContext.HoiDaps
                                    where HD.MaLoai == request.MaLoai
                                    select HD).ToList();
                foreach (var item in deleteHoiDap)
                {
                    var deleteNvHoiDap = (from NVHD in _CSKHContext.NvHoiDaps
                                          where NVHD.MaHoiDap == item.MaHoiDap
                                          select NVHD).ToList();
                    foreach (var del in deleteNvHoiDap)
                    {
                        _CSKHContext.NvHoiDaps.Remove(del);
                    }

                }
                foreach (var item in deleteHoiDap)
                {
                    _CSKHContext.HoiDaps.Remove(item);
                }
                _CSKHContext.LoaiHoiDaps.Remove(deleteLoaiHoiDap);

                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
            }
            output.isSuccess = true;
            output.message = "";
            output.data = foo;
            return output;
        }

        public async Task<List<LoaiHoiDapResponse>> getAll()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LHD in _CSKHContext.LoaiHoiDaps
                        select new { LHD };
            var temp = await query.Select(x => new LoaiHoiDapResponse()
            {
                MaLoai = x.LHD.MaLoai,
                TenLoai = x.LHD.TenLoai,
                MoTa = x.LHD.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<List<LoaiHoiDapResponse>> getAllAdmin()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LHD in _CSKHContext.LoaiHoiDaps
                        select new { LHD };
            var temp = await query.Select(x => new LoaiHoiDapResponse()
            {
                MaLoai = x.LHD.MaLoai,
                TenLoai = x.LHD.TenLoai,
                MoTa = x.LHD.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> updateLoaiHoiDap(LoaiHoiDapRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update_loaihoidap = await _CSKHContext.LoaiHoiDaps.FindAsync(request.MaLoai);
                if(update_loaihoidap == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                update_loaihoidap.TenLoai = request.TenLoai;
                update_loaihoidap.MoTa = request.MoTa;
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

        public async Task<List<LHoiDap>> getMaLoaiHoiDapTenLoaiHoiDap()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LHD in _CSKHContext.LoaiHoiDaps
                        select new { LHD };
            var temp = await query.Select(x => new LHoiDap()
            {
                MaLoai = x.LHD.MaLoai,
                TenLoai = x.LHD.TenLoai
            }).ToListAsync();
            return temp;
        }

        public async Task<List<LoaiHoiDapResponse>> searchLoaiHoiDapMaLoaiHoiDap(string maLoaiHoiDap)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LHD in _CSKHContext.LoaiHoiDaps
                        where LHD.MaLoai.Contains(maLoaiHoiDap)
                        select new { LHD };
            var temp = await query.Select(x => new LoaiHoiDapResponse()
            {
                MaLoai = x.LHD.MaLoai,
                TenLoai = x.LHD.TenLoai,
                MoTa = x.LHD.MoTa
            }).ToListAsync();
            return temp;
        }
    }
}
