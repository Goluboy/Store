namespace Store.Contracts.Requests
{
    public class SellerRequest
    {
        public Guid SellerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
