namespace TechnicalServices.Classes
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBesy))]
        public bool _IsBesy;

        public bool IsNotBesy => !IsBesy;

        public UserService _userService;
        public OrderService _orderService;
        public MainServiceService _mainServiceService;
        public TechnicianService _tichnicianService;
        public NotificationService _notificationService;
        public FeedBackService _feedbackService;
        public PaymentService _paymentService;
        public RatingService _ratingService;

        public BaseViewModel()
        {
            _userService = new UserService();
            _ratingService=new RatingService();
            _orderService = new OrderService();
            _feedbackService = new FeedBackService();
            _mainServiceService= new MainServiceService();
            _paymentService= new PaymentService();
            _tichnicianService= new TechnicianService();
            _notificationService= new NotificationService();
        }

        public BaseViewModel(string Token)
        {
            _userService = new UserService(Token);
            _ratingService=new RatingService(Token);
            _orderService = new OrderService(Token);
            _feedbackService = new FeedBackService(Token);
            _mainServiceService = new MainServiceService(Token);
            _paymentService = new PaymentService(Token);
            _tichnicianService = new TechnicianService(Token);
            _notificationService = new NotificationService(Token);
        }
    }
}
