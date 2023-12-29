using Microsoft.EntityFrameworkCore;
using TEST01.Models;

namespace TEST01.Context
{
    public class FoodShippingDbContext : DbContext
    {
        public FoodShippingDbContext(DbContextOptions<FoodShippingDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; } // Đổi tên DbSet để tuân thủ quy ước
        public DbSet<CTHD> CTHDs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; } // Đổi tên DbSet để tuân thủ quy ước
        public DbSet<MonAn> MonAns { get; set; } // Đổi tên DbSet để tuân thủ quy ước
        public DbSet<HoaDon> HoaDons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tạo hai khóa chính trong bảng CTHD
            modelBuilder.Entity<CTHD>()
                .HasKey(cthd => new { cthd.MaHD, cthd.MaMA });

            // Tạo quan hệ 1 vs 1 giữa KhachHang và Account
            modelBuilder.Entity<Account>()
                .HasOne(a => a.KhachHang)
                .WithOne(k => k.Account)
                .HasForeignKey<Account>(a => a.MaKH)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa Khách hàng khi xóa Tài khoản

            // Tạo quan hệ 1-n giữa KhachHang và HoaDon
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.KhachHang)
                .WithMany(k => k.HoaDons)
                .HasForeignKey(h => h.MaKH)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa Khách hàng khi xóa Hóa đơn

            // Tạo quan hệ 1-n giữa HoaDon và CTHD
            modelBuilder.Entity<CTHD>()
                .HasOne(cthd => cthd.HoaDon)
                .WithMany(h => h.CTHDs)
                .HasForeignKey(cthd => cthd.MaHD)
                .OnDelete(DeleteBehavior.Cascade); // Xóa tất cả CTHD khi xóa Hóa đơn

            // Tạo quan hệ n-n giữa MonAn và CTHD
            modelBuilder.Entity<CTHD>()
                .HasOne(cthd => cthd.MonAn)
                .WithMany(ma => ma.CTHDs)
                .HasForeignKey(cthd => cthd.MaMA)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa Món ăn khi xóa CTHD
        }
    }
}
