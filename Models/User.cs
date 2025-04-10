using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}