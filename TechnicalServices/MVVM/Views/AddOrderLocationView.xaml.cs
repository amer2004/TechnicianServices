using TechnicalServices.Dtos;

namespace TechnicalServices.MVVM.Views;

public partial class AddOrderLocationView : ContentPage
{
	public AddOrderLocationView(string Token,OrderDto dto)
	{
		InitializeComponent();
		BindingContext = new AddOrderLocationViewModel(Token,dto);
	}
}