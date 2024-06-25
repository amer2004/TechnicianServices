namespace TechnicalServices.Dtos.User
{
    public class UserDto
    {
        public string FirstName { get; set; } = ""; 
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public bool IsActive { get; set; }
        public bool IsTechnician { get; set; }
        public double XLocation { get; set; }
        public double YLocation { get; set; }
    }
}
