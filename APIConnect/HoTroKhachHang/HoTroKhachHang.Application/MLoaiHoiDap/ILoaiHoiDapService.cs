using System;
using System.Collections.Generic;
using System.Text;
using HoTroKhachHang.Application.MLoaiHoiDap;
using HoTroKhachHang.Data.Entities;
using HoTroKhachHang.Application.fRepository.Responses;
using System.Threading.Tasks;

namespace HoTroKhachHang.Application.MLoaiHoiDap
{
    public interface ILoaiHoiDapService
    {
        public Task<List<LoaiHoiDapResponse>> getAll();
        public Task<List<LoaiHoiDapResponse>> getAllAdmin();
        public Task<ResponseOutput> addLoaiHoiDap(LoaiHoiDapRequest request);
        public Task<ResponseOutput> updateLoaiHoiDap(LoaiHoiDapRequest request);
        public Task<ResponseOutput> deleteLoaiHoiDap(LoaiHoiDapRequest request);
        public Task<List<LHoiDap>> getMaLoaiHoiDapTenLoaiHoiDap();
        public Task<List<LoaiHoiDapResponse>> searchLoaiHoiDapMaLoaiHoiDap(string maLoaiHoiDap);
    }
}
