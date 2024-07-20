using System.ComponentModel.DataAnnotations;

namespace ShopApplication.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
