namespace TechnicalServices.Dtos.Technician
{
    public class TechnicianDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = "";
        public int AccountType { get; set; }
        public string SocialSecurityNumber { get; set; } = "";
    }
}
