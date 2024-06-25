namespace TechnicalServices.MVVM.Views;

public partial class AddUserFeedBackRatingsView : ContentPage
{
	public AddUserFeedBackRatingsView(string Token,UserFeedBack feedBack)
	{
		InitializeComponent();
		BindingContext = new AddUserFeedBackRatingsViewModel(Token,feedBack);
	}
}