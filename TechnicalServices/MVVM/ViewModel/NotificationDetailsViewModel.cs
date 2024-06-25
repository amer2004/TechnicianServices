namespace TechnicalServices.MVVM.ViewModel
{
    public partial class NotificationDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public UserNotifcation _Notifcation;
        public NotificationDetailsViewModel(UserNotifcation notifcation, string Token) : base(Token)
        {
            this.Notifcation = notifcation;
        }
    }
}
