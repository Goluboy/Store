namespace Store.Contracts.Requests
{
    public class UserRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
