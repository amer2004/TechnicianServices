namespace TechnicalServices.MVVM.Views;

public partial class OrderDetailsView : ContentPage
{
	public OrderDetailsView(string Token,Order Order)
	{
		InitializeComponent();
		BindingContext = new OrderDetailsViewModel(Order,Token);
	}
}