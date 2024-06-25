namespace TechnicalServices.Models.Technician
{
    public class TechnicianFeedBack
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public string message { get; set; } = "";
    }
}
