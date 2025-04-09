using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Models
{
    public class Good
    {
        [Key]
        public int GoodId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey("GoodCategory")]
        public int CategoryId { get; set; }
        public GoodCategory Category { get; set; }
    }
}