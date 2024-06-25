namespace TechnicalServices.MVVM.Views;

public partial class ResponseDetailsView : ContentPage
{
	public ResponseDetailsView(string Token,Response response,Order order)
	{
		InitializeComponent();
		BindingContext=new ResponseDetailsViewModel(Token,response,order);
	}
}