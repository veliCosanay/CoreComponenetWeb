using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreCommerce.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        public string name { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        public string description { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        [Range(1, int.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public int price { get; set; }
        [Required(ErrorMessage = "Alan boş bırakılamaz")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz")]
        public int stock { get; set; }

        [Column(TypeName = "varbinary(max)")]
        [Required(ErrorMessage = "Ürün fotoğrafı zorunludur")]
        public byte[]? image { get; set; }
    }
}
