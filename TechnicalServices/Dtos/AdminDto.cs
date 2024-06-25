namespace TechnicalServices.Dtos
{
    public class AdminDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public int AccessLevel { get; set; }
        public string SocialSecurityNumber { get; set; } = "";
    }
}
