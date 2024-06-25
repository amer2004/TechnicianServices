namespace TechnicalServices.MVVM.Views;

public partial class OrderLocationView : ContentPage
{
	public OrderLocationView(Order order)
	{
		InitializeComponent();
		BindingContext = new OrderLocationViewModel(order);
	}
}