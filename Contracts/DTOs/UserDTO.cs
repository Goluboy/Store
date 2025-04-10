namespace Store.Contracts.DTOs
{
    public record UserDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}