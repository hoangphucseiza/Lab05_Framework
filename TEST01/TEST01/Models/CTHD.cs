using System.ComponentModel.DataAnnotations;

namespace TEST01.Models
{
    public class CTHD
    {
        public string MaHD { get; set; }
        public string MaMA { get; set; }
        public string MAK { get; set; }
        public int SL { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual MonAn MonAn { get; set; }
    }
}
