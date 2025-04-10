namespace Store.Contracts.DTOs
{
    public record SellerDTO
    {
        public Guid SellerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}