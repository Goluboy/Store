namespace Store.Contracts.Requests
{
    public class GoodCategoryRequest
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
