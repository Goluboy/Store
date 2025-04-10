namespace Store.Contracts.Requests
{
    public class GoodRequest
    {
        public Guid GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
