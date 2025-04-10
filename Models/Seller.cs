using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class Seller
    {
        [Key]
        public Guid SellerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}