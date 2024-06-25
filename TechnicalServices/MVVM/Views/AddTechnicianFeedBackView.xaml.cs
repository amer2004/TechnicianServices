namespace TechnicalServices.MVVM.Views;

public partial class AddTechnicianFeedBackView : ContentPage
{
	public AddTechnicianFeedBackView(string Token,Order order)
	{
		InitializeComponent();
		BindingContext=new AddTechnicianFeedBackViewModel(Token,order);
	}
}