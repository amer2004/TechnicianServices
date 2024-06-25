namespace TechnicalServices.Models.User
{
    public class UserFeedBackResponse
    {
        public int id { get; set; }
        public int adminId { get; set; }
        public int userFeedBackId { get; set; }
        public string message { get; set; } = "";
    }
}