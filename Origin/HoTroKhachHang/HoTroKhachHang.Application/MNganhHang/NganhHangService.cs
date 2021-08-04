using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HoTroKhachHang.Application.MNganhHang
{
    public class NganhHangService : INganhHangService
    {
        private readonly CSKHContext _CSKHContext;
        public NganhHangService(CSKHContext CSKHContext)
        {
            _CSKHContext = CSKHContext;
        }
        private string convertStringDateTime(string dt)
        {
            //------12/1/2020 12:00:00 AM - 6/12/2020 12:00:00 PM - 9/2/2021 12:00:00 AM
            string res = "";
            if (dt.Substring(1, 1) == "/" && dt.Length == 20)
            {
                dt = dt.PadLeft(21, '0');
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(2, 1) == "/" && dt.Length == 21)
            {
                res = dt.Insert(3, "0");
            }
            else if (dt.Substring(1, 1) == "/" && dt.Length == 21)
            {
                res = dt.PadLeft(22, '0');
            }
            else
            {
                res = dt;
            }
            return res;
        }
        private string ShowDate(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(convertStringDateTime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy");
            }
            else return "";
        }
        private string themMaNganhHang()
        {
            var check = from HD in _CSKHContext.NganhHangs
                        select HD;
            if (check.ToList().Count == 0)
            {
                return "NH0_00000001";
                //HD00000001
            }
            var query = (from MHD in _CSKHContext.NganhHangs
                         orderby MHD.MaNganhHang descending
                         select MHD.MaNganhHang).First();
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

        private string fullDatetime(string dt)
        {
            //dd/MM/yyyy HH:mm:ss tt

            while (dt.Length != 22)
            {
                if (dt[2] != '/')
                {
                    dt = dt.Insert(0, "0");
                }
                else if (dt[5] != '/')
                {
                    dt = dt.Insert(3, "0");
                }
                else if (dt[13] != ':')
                {
                    dt = dt.Insert(11, "0");
                }
                else if (dt[16] != ':')
                {
                    dt = dt.Insert(14, "0");
                }
                else if (dt[19] != ' ')
                {
                    dt = dt.Insert(17, "0");
                }
            }
            return dt;
        }
        private string getDatetime(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(fullDatetime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)).ToString("dd-MM-yyyy HH:mm:ss tt");
            }
            else return null;
        }
        private DateTime? InPutDateTime(string check)
        {
            if (check != "")
            {
                return (DateTime.ParseExact(fullDatetime(check), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
            }
            else return null;
        }
        public async Task<ResponseOutput> addNganhHang(NganhHangRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var NH = new NganhHang()
                {
                    MaNganhHang = themMaNganhHang(),
                    TenNganhHang = request.TenNganhHang,
                    MoTa = request.MoTa
                };
                _CSKHContext.NganhHangs.Add(NH);
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

        public async Task<List<NganhHangResponse>> GetAll()
        {
            var query = from NH in _CSKHContext.NganhHangs
                        select new { NH };
            var temp = await query.Select(x => new NganhHangResponse()
            {
                MaNganhHang = x.NH.MaNganhHang,
                TenNganhHang=x.NH.TenNganhHang,
                MoTa=x.NH.MoTa
            }).ToListAsync();
            return temp;
        }

        public async Task<ResponseOutput> updateNganhHang(NganhHangRequest request)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var update = await _CSKHContext.NganhHangs.FirstOrDefaultAsync(s => s.MaNganhHang == request.MaNganhHang);
                if (update == null)
                {
                    throw new InvalidOperationException("Không tồn tại dữ liệu tìm kiếm");
                }
                update.TenNganhHang = request.TenNganhHang;
                update.MoTa = request.MoTa;
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
        public async Task<List<NganhHangExpand>> GetNganhHang_QuangCao()
        {
            List<NganhHangExpand> list = new List<NganhHangExpand>();
            try
            {
                var query = await (from NH in _CSKHContext.NganhHangs
                                   select NH.MaNganhHang).ToListAsync();

                foreach (var item in query)
                {
                    NganhHangExpand ds = await (from NH in _CSKHContext.NganhHangs
                                                where NH.MaNganhHang.Contains(item)
                                                select new NganhHangExpand
                                                {
                                                    MaNganhHang = NH.MaNganhHang,
                                                    MoTaNganhHang = NH.MoTa,
                                                    TenNganhHang = NH.TenNganhHang
                                                }).FirstOrDefaultAsync();
                    var temp = from QC in _CSKHContext.QuangCaos
                               join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                               where QC.MaNganhHang.Contains(item) && QC.MaTrangThai.Contains("TT005") && DateTime.Compare(QC.NgayHetHan, DateTime.Now) > 0
                               select new { QC, NSX };

                    
                    if (temp.ToList().Count != 0)
                    {
                        ds.QuangCaos = new List<fRepository.Models.QuangCao>();                        
                        foreach (var jtem in temp)
                        {
                            var anh = _CSKHContext.AnhQuangCaos.FirstOrDefault(s => s.MaQuangCao == jtem.QC.MaQuangCao).TenAnh;
                            ds.QuangCaos.Add(new fRepository.Models.QuangCao()
                            {
                                DiaDiem = jtem.QC.DiaDiem,
                                Email = jtem.QC.Email,
                                DuongDan = new List<string> { anh },
                                GiaCa = jtem.QC.GiaCa,
                                MaNganhHang = jtem.QC.MaNganhHang,
                                MaQuangCao = jtem.QC.MaQuangCao,
                                MaTrangThai = jtem.QC.MaTrangThai,
                                MoTaQuangCao = jtem.QC.MoTaQuangCao,
                                NgayDang = getDatetime(jtem.QC.NgayDang.ToString()),
                                NgayHetHan= ShowDate( jtem.QC.NgayHetHan.ToString()),
                                NguoiDeNghi=jtem.QC.NguoiDeNghi,
                                Sdt=jtem.QC.NguoiDeNghi,
                                TenQuangCao=jtem.QC.TenQuangCao,
                                XuatXu = jtem.NSX.TenNsx,
                                NgayNhan=getDatetime(jtem.QC.NgayNhan.ToString())
                            }) ;
                        }
                        ds.QuangCaos.Sort((a, b) => DateTime.ParseExact(b.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                        foreach (var jtem in ds.QuangCaos)
                        {
                            jtem.NgayDang = jtem.NgayDang.Substring(0, 16);
                        }                       
                    }
                    list.Add(ds);
                }
            }
            catch (Exception ex)
            {
                return new List<NganhHangExpand>();
            }
            return list;
        }
        private int getNextGiaCa(int maGia)
        {
            var allGia = (from G in _CSKHContext.Gia
                     select new { G }).ToList();
            
            allGia.Sort((a, b) => Int64.Parse(a.G.Gia.ToString()).CompareTo(Int64.Parse(b.G.Gia.ToString())));
            for(var i = 0; i < allGia.Count; i++)
            {
                if (allGia[i].G.Id == maGia && allGia[i+1]!=null)
                {
                    return allGia[i + 1].G.Gia;
                }
            }
            return Int32.MaxValue;
        }
        public async Task<List<NganhHangExpand>> Search(string maNganhHang, string diaDiem, string maNSX, int maGia,int status)
        {
            var list = new List<NganhHangExpand>();
                    
            try
            {
                var query = from NH in _CSKHContext.NganhHangs
                            select new { NH };
                if (!string.IsNullOrEmpty(maNganhHang)) // MaNganhHang not null
                {
                    query = from a in query
                            where a.NH.MaNganhHang.Contains(maNganhHang)
                            select new { a.NH };
                }
                foreach (var item in query)
                {
                    list.Add(new NganhHangExpand()
                    {
                        MaNganhHang = item.NH.MaNganhHang,
                        MoTaNganhHang = item.NH.MaNganhHang,
                        TenNganhHang = item.NH.TenNganhHang,
                        QuangCaos = new List<fRepository.Models.QuangCao>()
                    });
                }
                foreach (var item in list)
                {
                    var quangcao = from QC in _CSKHContext.QuangCaos
                                   join NSX in _CSKHContext.Nsxes on QC.MaNsx equals NSX.MaNsx
                                   where QC.MaNganhHang == item.MaNganhHang && QC.MaTrangThai=="TT005" && QC.NgayHetHan > DateTime.Now                                   
                                   select new { QC ,NSX};
                    if (quangcao.ToList().Count == 0)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(diaDiem))
                    {
                        quangcao = from a in quangcao
                                   where a.QC.DiaDiem.Contains(diaDiem)
                                   select new { a.QC ,a.NSX};
                    }
                    if (!string.IsNullOrEmpty(maNSX))
                    {
                        quangcao = from a in quangcao
                                   where a.QC.MaNsx.Contains(maNSX)
                                   select new { a.QC,a.NSX };
                    }                   
                    
                    if (maGia!=-1)
                    {
                        int x, y;
                        if (maGia == 0)
                        {
                            y = _CSKHContext.Gia.FirstOrDefault(a => a.Id == maGia).Gia;
                            x = 0;
                        }
                        else
                        {
                            x = _CSKHContext.Gia.FirstOrDefault(a => a.Id == maGia).Gia;
                            y = getNextGiaCa(maGia);
                        }
                        
                        quangcao = from a in quangcao
                                   where Convert.ToInt32(a.QC.GiaCa) >= x && Convert.ToInt32(a.QC.GiaCa) <= y                               
                                   select new { a.QC,a.NSX };
                    }
                    if (quangcao.ToList().Count != 0)
                    {
                        foreach(var jtem in quangcao)
                        {
                            var anh = _CSKHContext.AnhQuangCaos.FirstOrDefault(s => s.MaQuangCao == jtem.QC.MaQuangCao).TenAnh;
                            item.QuangCaos.Add(new fRepository.Models.QuangCao()
                            {
                                DiaDiem = jtem.QC.DiaDiem,
                                Email = jtem.QC.Email,
                                DuongDan = new List<string> { anh },
                                GiaCa = jtem.QC.GiaCa,
                                MaNganhHang = jtem.QC.MaNganhHang,
                                MaQuangCao = jtem.QC.MaQuangCao,
                                MaTrangThai = jtem.QC.MaTrangThai,
                                MoTaQuangCao = jtem.QC.MoTaQuangCao,
                                NgayDang = getDatetime(jtem.QC.NgayDang.ToString()),
                                NgayHetHan = ShowDate(jtem.QC.NgayHetHan.ToString()),
                                NguoiDeNghi = jtem.QC.NguoiDeNghi,
                                Sdt = jtem.QC.NguoiDeNghi,
                                TenQuangCao = jtem.QC.TenQuangCao,
                                XuatXu = jtem.NSX.TenNsx,
                                NgayNhan = getDatetime(jtem.QC.NgayNhan.ToString()),
                            });
                        };
                        if(status == 0)
                        {
                            item.QuangCaos.Sort((a, b) => DateTime.ParseExact(b.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(a.NgayDang, "dd-MM-yyyy HH:mm:ss tt", CultureInfo.InvariantCulture)));
                            foreach (var jtem in item.QuangCaos)
                            {
                                jtem.NgayDang = jtem.NgayDang.Substring(0, 16);
                            }
                        }
                        if(status == 1)
                        {
                            item.QuangCaos.Sort((a, b) =>Int64.Parse(a.GiaCa).CompareTo(Int64.Parse(b.GiaCa)));
                        }
                        else if (status == -1)
                        {
                            item.QuangCaos.Sort((a, b) => Int64.Parse(b.GiaCa).CompareTo(Int64.Parse(a.GiaCa)));
                        }
                    }                              
                }
            }
            catch
            {
                return new List<NganhHangExpand>();
            }
            return list.Where(s=>s.QuangCaos.Count!=0).ToList();
        }

        public async Task<ResponseOutput> deleteNganhHang(string maNganhHang)
        {
            ResponseOutput output = new ResponseOutput();
            dynamic foo = new ExpandoObject();
            try
            {
                var check_quangcao = (from QC in _CSKHContext.QuangCaos
                                      where QC.MaNganhHang == maNganhHang
                                      select QC).ToList();
                if (check_quangcao.Count != 0)
                {
                    foreach (var item in check_quangcao)
                    {
                        var check_nvqc = (from NVQC in _CSKHContext.NvQuangCaos
                                          where NVQC.MaQuangcao == item.MaQuangCao
                                          select NVQC).ToList();
                        if (check_nvqc.Count != 0)
                        {
                            foreach (var jtem in check_nvqc)
                            {
                                _CSKHContext.NvQuangCaos.Remove(jtem);
                            }

                        }
                        var check_aqc = (from AQC in _CSKHContext.AnhQuangCaos
                                         where AQC.MaQuangCao == item.MaQuangCao
                                         select AQC).ToList();
                        if (check_aqc.Count != 0)
                        {
                            foreach (var jtem in check_aqc)
                            {
                                _CSKHContext.AnhQuangCaos.Remove(jtem);
                            }
                        }
                        _CSKHContext.QuangCaos.Remove(item);
                    }
                }
                var nh = _CSKHContext.NganhHangs.Find(maNganhHang);
                _CSKHContext.NganhHangs.Remove(nh);
                foo.change = await _CSKHContext.SaveChangesAsync();

                output.isSuccess = true;
                output.message = "Xóa thành công, các thông tin có liên quan đến câu hỏi này đều bị xóa";
                output.data = foo;
                return output;

            }
            catch (Exception ex)
            {
                output.isSuccess = false;
                output.message = ex.ToString();
                return output;
            }
        }
    }
}
