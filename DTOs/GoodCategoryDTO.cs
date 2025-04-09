using Marketplace.Models;

namespace Marketplace.DTOs
{
    public class GoodCategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}