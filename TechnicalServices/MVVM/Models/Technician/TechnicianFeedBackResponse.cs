namespace TechnicalServices.Models.Technician
{
    public class TechnicianFeedBackResponse
    {
        public int id { get; set; }

        public int adminId { get; set; }
        public int technicianFeedBackId { get; set; }
        public string message { get; set; } = "";
    }
}
