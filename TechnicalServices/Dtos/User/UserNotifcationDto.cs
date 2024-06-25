namespace TechnicalServices.Dtos.User
{
    public class UserNotifcationDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }="";
        public string Message { get; set; } = "";
    }
}
