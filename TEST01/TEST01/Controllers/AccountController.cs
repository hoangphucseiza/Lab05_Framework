using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TEST01.Context;
using TEST01.Models;

namespace TEST01.Controllers
{
    public class AccountController : Controller
    {
        private readonly FoodShippingDbContext _context;

        public AccountController(FoodShippingDbContext context)
        {
           this._context = context;
        }

       
        // Lấy danh sách user
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var account = await _context.Accounts.ToListAsync();
               return View(account);
        }


        [HttpGet]
        public IActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(Account model)
        {
            
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.PassWord == model.PassWord);
                 return RedirectToAction("Index", "KhachHang", new { MaKH = account.MaKH });
        }
        [HttpGet]
        public IActionResult DangKy()
        {

            return View();
        }
        private string GenerateRandomMaKH()
        {
            // Tạo mã ngẫu nhiên, ví dụ: "KH" + số ngẫu nhiên
            var random = new Random();
            var randomNumber = random.Next(1000, 9999);
            return "KH" + randomNumber.ToString();
        }
        [HttpPost]
        public async Task<IActionResult> DangKy(DangKyModel model)
        {
            if (ModelState.IsValid)
            {
                var khachhang = _context.KhachHangs.Find(model.UserName);
                if (khachhang != null)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại");
                    return View(model);
                }
                else
                {
                    var newKhachHang = new KhachHang
                    {
                        MaKH = GenerateRandomMaKH(),
                        Ho = model.Ho,
                        Ten = model.Ten,
                        NgaySinh = model.NgaySinh,
                        DiaChi = model.DiaChi,
                        SDT = model.SDT
                    };
                    var newAccount = new Account
                    {
                        UserName = model.UserName,
                        PassWord = model.PassWord,
                        MaKH = newKhachHang.MaKH
                    };

                    await _context.KhachHangs.AddAsync(newKhachHang);
                    _context.Accounts.Add(newAccount);
                    _context.SaveChanges();

                }
            }
            return RedirectToAction("DangNhap");
        }
    }
}

