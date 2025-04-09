using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class GoodCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public virtual GoodCategory ParentCategory { get; set; }
        public virtual ICollection<GoodCategory> ChildCategories { get; set; }

    }
}