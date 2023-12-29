using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST01.Context;
using TEST01.Models;

namespace TEST01.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly FoodShippingDbContext _context;

        public HoaDonController(FoodShippingDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult DatMonAn( string MaKH)
        {
          var DatMonAn = new DatMonAnModel();
            DatMonAn.MaKH = MaKH;

            return View(DatMonAn);
        }

        [HttpPost]
        public async Task<IActionResult>  DatMonAn(DatMonAnModel model)
        {
            var HoaDon = new HoaDon();
            HoaDon.MaHD = model.MaHoaDon;
            HoaDon.MaKH = model.MaKH;
            HoaDon.NgayHD = model.NgayHD;
            HoaDon.THT = model.ThanhTien;

            var CTHD = new CTHD();
            CTHD.MaHD = model.MaHoaDon;
            CTHD.MaMA = model.MaMonAn;
            CTHD.SL = model.SoLuong;
            CTHD.MAK = "";
            
            var MonAn = new MonAn();
            MonAn.MaMA = model.MaMonAn;
            MonAn.TenMA = model.TenMonAn;
            MonAn.DonGia = model.DonGia;
            MonAn.LoaiMA = "";

            await _context.HoaDons.AddAsync(HoaDon);
            await _context.CTHDs.AddAsync(CTHD);
             await _context.MonAns.AddAsync(MonAn);
              _context.SaveChanges();

            return RedirectToAction("Index", "KhachHang", new { MaKH = model.MaKH });

        }


       
        [HttpPost]
        public async Task<IActionResult> XoaHoaDon(string MaHD)
        {
          var hoadon = await _context.HoaDons.FirstOrDefaultAsync(x => x.MaHD == MaHD);
            var cthd = await _context.CTHDs.FirstOrDefaultAsync(x => x.MaHD == MaHD);
            var monan = await _context.MonAns.FirstOrDefaultAsync(x => x.MaMA == cthd.MaMA);

            _context.HoaDons.Remove(hoadon);
            _context.CTHDs.Remove(cthd);
            _context.MonAns.Remove(monan);
            await _context.SaveChangesAsync();
            return RedirectToAction("XemHoaDon", "KhachHang");
        }


        [HttpGet]
        public async Task<IActionResult> XemChiTietHoaDon(string MaHD)
        {
            var XemChiTietHoaDon = new XemChiTietHoaDonModel();
            
            var hoadon = await _context.HoaDons.FirstOrDefaultAsync(x => x.MaHD == MaHD);
            var cthd = await _context.CTHDs.FirstOrDefaultAsync(x => x.MaHD == MaHD);
            var khachhang = await _context.KhachHangs.FirstOrDefaultAsync(x => x.MaKH == hoadon.MaKH);
            var monan = await _context.MonAns.FirstOrDefaultAsync(x => x.MaMA == cthd.MaMA);

         
            
            XemChiTietHoaDon.MaHD = hoadon.MaHD;
            XemChiTietHoaDon.MaKH = hoadon.MaKH;
            XemChiTietHoaDon.TenKH = khachhang.Ten;
            XemChiTietHoaDon.TenMonAn = monan.TenMA;
            XemChiTietHoaDon.SoLuong = cthd.SL;
            XemChiTietHoaDon.DonGia = monan.DonGia;
            XemChiTietHoaDon.ThanhTien = cthd.SL * monan.DonGia;
            XemChiTietHoaDon.THT = hoadon.THT;
            XemChiTietHoaDon.NgayHD = hoadon.NgayHD;





            return View(XemChiTietHoaDon);
        }
    }
}
