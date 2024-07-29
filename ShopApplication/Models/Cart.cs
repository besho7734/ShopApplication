using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApplication.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product? product { get; set; }
        public int Quantity { get; set; }

        public AppUser? UserCart { get; set; }
    }
}
