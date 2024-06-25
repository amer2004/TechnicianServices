namespace TechnicalServices.MVVM.Views;

public partial class UserPaymentsView : ContentPage
{
	public UserPaymentsView(string Token,int UserId)
	{
		InitializeComponent();
		BindingContext = new UserPaymentsViewModel(Token,UserId);
	}
}