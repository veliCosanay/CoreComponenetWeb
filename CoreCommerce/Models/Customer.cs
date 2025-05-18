using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommerce.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Alan boş bırakılamaz")]
        public string name { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        public string surname { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        public string city { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        
        public string password { get; set; }

        [Column(TypeName="varbinary(max)")]
        public byte[]? image { get; set; }

        public bool isAdmin { get; set; } = false;
    }
}
