namespace TechnicalServices.Models.Technician
{
    public class TechniciansServices
    {
        public int id { get; set; }
        public int extendServiceId { get; set; }
        public ExtendService? extendService { get; set; }
        public int technicianId { get; set; }
    }
}
