namespace TechnicalServices.MVVM.Views;
public partial class CreateOrderView : ContentPage
{
	public CreateOrderView(User user,string Token,int ExtendServiceId)
	{
		InitializeComponent();
		BindingContext = new CreateOrderViewModel(user,Token,ExtendServiceId);
	}
}