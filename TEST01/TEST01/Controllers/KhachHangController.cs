using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST01.Context;
using TEST01.Models;

namespace TEST01.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly FoodShippingDbContext _context;

        public KhachHangController(FoodShippingDbContext context)
        {
            _context = context;
        }



        // Sau khi đăng nhập sẽ chuyển đến trang này
        [HttpGet]
        public IActionResult Index(string MaKH)
        {

            if(string.IsNullOrEmpty(MaKH))
            {
                // Xử lý khi MaKH không có giá trị
                return RedirectToAction("DangNhap", "Account"); // Chẳng hạn, chuyển hướng đến trang chính
            }

            var khachHang = _context.KhachHangs.FirstOrDefault(x => x.MaKH == MaKH);


            if (khachHang == null)
            {
                // Xử lý khi không tìm thấy khách hàng
                return RedirectToAction("DangNhap", "Account"); // Chẳng hạn, chuyển hướng đến trang chính
            }

            return View(khachHang);
        }


        [HttpGet]
        public IActionResult XemHoaDon()
        {
            // Lấy danh sách KhachHangs kèm theo HoaDons từ database
            var khachhangvahoadon = _context.KhachHangs.Include(x => x.HoaDons).ToList();

            // Tạo một danh sách mới chứa thông tin cần thiết
            var hoaDonInfoList = khachhangvahoadon
                .SelectMany(kh => kh.HoaDons.Select(hd => new HoaDonViewModel
                {
                    KhachHangId = kh.MaKH,  // Lấy MaKH từ KhachHang
                    TenKhachHang = $"{kh.Ho} {kh.Ten}",  // Kết hợp Ho và Ten để tạo tên khách hàng
                    MaHD = hd.MaHD,
                    NgayHD = hd.NgayHD
                }))
                .ToList();

            // Truyền danh sách mới đến view
            return View(hoaDonInfoList);
        }



    }
}
