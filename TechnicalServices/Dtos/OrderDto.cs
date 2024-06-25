namespace TechnicalServices.Dtos
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public int ExtendServiceId { get; set; }
        public DateOnly Date { get; set; }
        public double XLocation { get; set; }
        public double YLocation { get; set; }
        public string Description { get; set; } = "";
    }
}
