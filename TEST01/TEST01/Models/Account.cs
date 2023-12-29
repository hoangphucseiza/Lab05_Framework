using System.ComponentModel.DataAnnotations;

namespace TEST01.Models
{
    public class Account
    {
        [Key]
        public string UserName { get; set; }
        public string MaKH { get; set; }
        public string PassWord { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
