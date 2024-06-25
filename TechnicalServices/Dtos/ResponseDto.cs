namespace TechnicalServices.Dtos
{
    public class ResponseDto
    {
        public int TechnicianId { get; set; }
        public int OrderId { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public double EstimatedTime { get; set; }

        public double EstimatedPrice { get; set; }
    }
}
