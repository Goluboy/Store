namespace Store.Contracts.DTOs
{
    public record GoodDTO
    {
        public Guid GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}