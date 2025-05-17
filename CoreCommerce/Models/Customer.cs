using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommerce.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string city { get; set; }
        public string password { get; set; }

        [Column(TypeName="varbinary(max)")]
        public byte[]? image { get; set; }
    }
}
