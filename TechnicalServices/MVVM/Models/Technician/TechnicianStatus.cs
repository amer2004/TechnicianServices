using System.ComponentModel.DataAnnotations;

namespace TechnicalServices.Models.Technician
{
    public class TechnicianStatus
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string description { get; set; } = "";

    }
}
