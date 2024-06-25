namespace TechnicalServices.Models.User
{
    public class UserFeedBackRating
    {
        public int id { get; set; }
        public int userFeedBackId { get; set; }
        public int ratingTypeId { get; set; }
        public RatingType? ratingType { get; set; }
        public double value { get; set; }
    }
}