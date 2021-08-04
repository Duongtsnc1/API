using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MLoaiTaiLieu;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace HoTroKhachHang.Application.MLoaiTaiLieu
{
    public class LoaiTaiLieuService : ILoaiTaiLieuService
    {
        private readonly CSKHContext _CSKHContext;
        public LoaiTaiLieuService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }

        private string themMaLoaiTaiLieu()
        {
            var check = from LTL in _CSKHContext.LoaiTaiLieus
                        select LTL;
            if (check == null)
            {
                return "LTL001";
            }
            var query = (from MLTL in _CSKHContext.LoaiTaiLieus
                         orderby MLTL.MaLoaiTaiLieu descending
                         select MLTL.MaLoaiTaiLieu).First();
            var lastrecord = query.Select(x => x.ToString()).ToArray();
            string join_lastrecord = string.Join("", lastrecord);
            string chu = join_lastrecord.Substring(0, 3);
            int so = Int32.Parse(join_lastrecord.Substring(3, join_lastrecord.Length - 3));
            so += 1;
            string tempso = so.ToString();
            join_lastrecord = chu + tempso.PadLeft(3, '0');
            return join_lastrecord;
        }
        public async Task<ResponseOutput> addLoaiTaiLieu(LoaiTaiLieuRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var ltl = new LoaiTaiLieu
                {
                    MaLoaiTaiLieu = themMaLoaiTaiLieu(),
                    TenLoaiTaiLieu = request.TenLoaiTaiLieu,
                    TrangThai = request.TrangThai,
                    MoTa = request.MoTa
                };
                _CSKHContext.LoaiTaiLieus.Add(ltl);
                foo.change = await _CSKHContext.SaveChangesAsync();
            }
            catch (Exception ex)
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

        public async Task<List<LoaiTaiLieuResponse>> getAll()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LTL in _CSKHContext.LoaiTaiLieus
                        where LTL.TrangThai == true
                        select new { LTL };
            var temp = await query.Select(x => new LoaiTaiLieuResponse()
            {
                MaLoaiTaiLieu = x.LTL.MaLoaiTaiLieu,
                TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu,
                TrangThai = x.LTL.TrangThai,
                MoTa = x.LTL.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<List<LoaiTaiLieuResponse>> getAllAdmin()
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LTL in _CSKHContext.LoaiTaiLieus
                        select new { LTL };
            var temp = await query.Select(x => new LoaiTaiLieuResponse()
            {
                MaLoaiTaiLieu = x.LTL.MaLoaiTaiLieu,
                TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu,
                TrangThai = x.LTL.TrangThai,
                MoTa = x.LTL.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> trangThaiLoaiTaiLieu(LoaiTaiLieuRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update_ltl_tt = await _CSKHContext.LoaiTaiLieus.FindAsync(request.MaLoaiTaiLieu);
                if (update_ltl_tt == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                update_ltl_tt.TrangThai = request.TrangThai;
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

        public async Task<ResponseOutput> updateLoaiTaiLieu(LoaiTaiLieuRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update_loaitailieu = await _CSKHContext.LoaiTaiLieus.FindAsync(request.MaLoaiTaiLieu);
                if (update_loaitailieu == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                update_loaitailieu.TenLoaiTaiLieu = request.TenLoaiTaiLieu;
                update_loaitailieu.TrangThai = request.TrangThai;
                update_loaitailieu.MoTa = request.MoTa;
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

        public async Task<List<LoaiTaiLieuResponse>> searchLoaiTaiLieuMaLoaiTaiLieu(string maLoaiTaiLieu)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();

            var query = from LTL in _CSKHContext.LoaiTaiLieus
                        where LTL.MaLoaiTaiLieu.Contains(maLoaiTaiLieu)
                        select new { LTL };
            var temp = await query.Select(x => new LoaiTaiLieuResponse()
            {
                MaLoaiTaiLieu = x.LTL.MaLoaiTaiLieu,
                TenLoaiTaiLieu = x.LTL.TenLoaiTaiLieu,
                TrangThai = x.LTL.TrangThai,
                MoTa = x.LTL.MoTa
            }).ToListAsync();
            return temp;
        }
    }
}
