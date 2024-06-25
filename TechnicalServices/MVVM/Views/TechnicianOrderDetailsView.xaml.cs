namespace TechnicalServices.MVVM.Views;

public partial class TechnicianOrderDetailsView : ContentPage
{
	public TechnicianOrderDetailsView(string Token,Order order)
	{
		InitializeComponent();
		BindingContext=new TechnicianOrderDetailsViewModel(Token,order);
	}
}