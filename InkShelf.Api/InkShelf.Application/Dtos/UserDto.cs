namespace InkShelf.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Firstname { get; set; }
        public required string Email { get; set; }
        public required string AccessToken { get; set; }
    }
}
