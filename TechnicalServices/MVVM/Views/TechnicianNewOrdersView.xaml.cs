using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class TechnicianNewOrdersView : ContentPage
{
	public TechnicianNewOrdersView(string Token,Technician technician)
	{
		InitializeComponent();
		BindingContext=new TechnicianNewOrdersViewModel(Token,technician);
	}
}