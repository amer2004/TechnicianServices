namespace TechnicalServices.Models
{
    public class Order
    {
        public int id { get; set; }
        public int userId { get; set; }
        public User.User? user { get; set; }
        public long orderNumber { get; set; }
        public int extendServiceId { get; set; }
        public ExtendService? ExtendService { get; set; }
        public DateOnly date { get; set; }
        public double xLocation { get; set; }
        public double yLocation { get; set; }
        public int? chosenResponseId { get; set; }
        public Response? ChosenResponse { get; set; }
        public string description { get; set; } = "";
        public int statusId { get; set; }
        public OrderStatus? Status { get; set; }
        public List<Response>? Responses { get; set; }
    }
}
