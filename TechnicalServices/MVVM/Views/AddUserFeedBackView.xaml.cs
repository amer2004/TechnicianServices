namespace TechnicalServices.MVVM.Views;

public partial class AddUserFeedBackView : ContentPage
{
	public AddUserFeedBackView(string Token,Order order)
	{
		InitializeComponent();
		BindingContext = new AddUserFeedBackViewModel(Token,order);
	}
}