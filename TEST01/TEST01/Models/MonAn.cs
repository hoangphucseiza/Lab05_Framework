using System.ComponentModel.DataAnnotations;

namespace TEST01.Models
{
    public class MonAn
    {
        [Key]
        public string MaMA { get; set; }
        public string TenMA { get; set; }
        public decimal DonGia { get; set; }
        public string LoaiMA { get; set; }

        public virtual ICollection<CTHD> CTHDs { get; } = new List<CTHD>();
    }
}
