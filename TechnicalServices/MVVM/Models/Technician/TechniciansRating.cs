namespace TechnicalServices.Models.Technician
{
    public class TechniciansRating
    {
        public int id { get; set; }
        public int ratingTypeId { get; set; }
        public RatingType? ratingType { get; set; }
        public int technicianId { get; set; }
        public double value { get; set; }
    }
}
