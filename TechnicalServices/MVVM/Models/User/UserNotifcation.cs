namespace TechnicalServices.Models.User
{
    public class UserNotifcation
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; } = "";
        public string message { get; set; } = "";
        public DateTime dateTime { get; set; }
        //new notifcation = False
        public bool status { get; set; }
    }
}
