namespace TechnicalServices.MVVM.Views;

public partial class OrderResponsesView : ContentPage
{
	public OrderResponsesView(string Token,Order order)
	{
		InitializeComponent();
		BindingContext = new OrderResponsesViewModel(Token, order);
	}
}