namespace TechnicalServices.Models.User
{
    public class UserPayment
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int paymentTypeId { get; set; }
        public PaymentType? paymentType { get; set; }
        public double paymentAmount { get; set; }
        public DateTime dateTime { get; set; }
    }
}
