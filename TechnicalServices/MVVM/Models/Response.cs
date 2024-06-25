namespace TechnicalServices.Models
{
    public class Response
    {
        public int id { get; set; }
        public Technician.Technician? technician { get; set; }
        public int technicianId { get; set; }
        public int OrderId { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
        public double EstimatedTime { get; set; }
        public double EstimatedPrice { get; set; }
    }
}
