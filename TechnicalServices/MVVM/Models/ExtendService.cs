using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalServices.Models
{
    public class ExtendService
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public int mainServiceId { get; set; }
        public string PicName { get; set; } = "";
    }
}