using Marketplace.Models;

namespace Store.Contracts.DTOs
{
    public record GoodCategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}