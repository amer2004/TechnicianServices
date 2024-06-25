using System.ComponentModel.DataAnnotations;

namespace TechnicalServices.Models
{
    public class OrderStatus
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";
    }
}
