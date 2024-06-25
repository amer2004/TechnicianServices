namespace TechnicalServices.MVVM.Views;

public partial class NotificationDetailsView : ContentPage
{
	public NotificationDetailsView(UserNotifcation notifcation,string Token)
	{
		InitializeComponent();
		BindingContext=new NotificationDetailsViewModel(notifcation,Token);
	}
}