namespace TechnicalServices.Models.Technician
{
    public class Technician
    {
        public int id { get; set; }
        public User.User? User { get; set; }
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public DateOnly lastSigninDate { get; set; }
        public int accountType { get; set; }
        public int experienceLevel { get; set; }
        public string socialSecurityNumber { get; set; } = "";
        public int statusId { get; set; }
        public int? approvedById { get; set; }
    }
}