using System.ComponentModel.DataAnnotations;

namespace TEST01.Models
{
    public class KhachHang
    {
        [Key]
        public string MaKH { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }

        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();
    }
}
