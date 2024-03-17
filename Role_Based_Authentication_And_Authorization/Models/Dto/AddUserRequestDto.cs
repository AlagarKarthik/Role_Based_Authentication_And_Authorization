namespace Role_Based_Authentication_And_Authorization.Models.Dto
{
    public class AddUserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
