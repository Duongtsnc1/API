using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoTroKhachHang.Application.MForum;
using HoTroKhachHang.Application.fRepository.Responses;
using HoTroKhachHang.Application.fRepository.Models;
using HoTroKhachHang.Application.fRepository.ResponsesForum;
using HoTroKhachHang.Application.fRepository.ModelsForum;

namespace HoTroKhachHang.Application.MForum
{
    public interface IForumService
    {
        public Task<ForumTongQuan> getTongQuan();
        public Task<List<ForumNhomChuDe>> getNhomChuDe();
        public Task<List<ForumNhomChuDe>> getNhomChuDeA();
        public Task<List<ForumResponse>> getChuDe();
        public Task<List<ForumBaiDangComment>> getCommentBaiDang(string maBaiDang);
        public Task<List<ForumBaiDang>> getAllBaiDang();
        public Task<List<ForumBaiDang>> getBaiDangKhachHang(string maKhachHang);
        public Task<List<ForumBaiDang>> getBaiDangTieuDe(string tieuDe);
        public Task<ResponseOutput> addBinhLuan(ForumBinhLuanMoi request);
        public Task<ResponseOutput> updateHotTrend(ForumHotTrend request);
        public Task<ResponseOutput> updateSoLuong(string maBaiDang);
        public Task<ResponseOutput> updateChuDe(ForumChuDe request);
        public Task<ResponseOutput> deleteChuDe(ForumDeleteChuDe request);
        public Task<ResponseOutput> deleteBaiDang(string mabaidang);
        public Task<ResponseOutput> deleteBinhLuan(int id);
        public Task<ResponseOutput> addBaiDang(ForumBaiDangRequest request);
        public Task<ResponseOutput> addChuDe(ForumChuDeRequest request);
        public Task<ResponseOutput> addNhomChuDe(ForumNhomChuDeRequest request);
        public Task<List<ForumBaiDang>> getBaiDangMaBaiDang(string maBaiDang);
        public Task<List<ForumBaiDang>> getBaiDangMaChuDe(string maChuDe);
    }
}
