using System.ComponentModel.DataAnnotations;

namespace TEST01.Models
{
    public class HoaDon
    {
        [Key]
        public string MaHD { get; set; }
        public string MaKH { get; set; }

        public decimal THT { get; set; }
        public DateTime NgayHD { get; set; }

        public ICollection<CTHD> CTHDs { get; } = new List<CTHD>();
        public virtual KhachHang KhachHang { get; set; }
    }
}
