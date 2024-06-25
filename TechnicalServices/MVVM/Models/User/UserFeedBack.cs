namespace TechnicalServices.Models.User
{
    public class UserFeedBack
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public string message { get; set; } = "";
    }
}
