using Microsoft.CodeAnalysis;
using System.Collections;

using System.Drawing;

namespace TechnicalServices.Models.User
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public bool isActive { get; set; }
        public string phoneNumber { get; set; } = "";
        public double balance { get; set; }
        public bool isTechnician { get; set; }
        public DateTime signUpDate { get; set; }
        public double xLocation { get; set; }
        public double yLocation { get; set; }
        public List<UserNotifcation>? userNotifcations { get; set; }
    }
}
