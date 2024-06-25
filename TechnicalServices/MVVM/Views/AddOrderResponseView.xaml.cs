using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class AddOrderResponseView : ContentPage
{
	public AddOrderResponseView(string Token,Technician technician,Order order)
	{
		InitializeComponent();
		BindingContext=new AddOrderResponseViewModel(Token,technician,order);
	}
}