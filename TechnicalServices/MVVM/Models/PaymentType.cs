using TechnicalServices.Models.User;

namespace TechnicalServices.Models
{
    public class PaymentType
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";
        public double amount { get; set; }
    }
}
