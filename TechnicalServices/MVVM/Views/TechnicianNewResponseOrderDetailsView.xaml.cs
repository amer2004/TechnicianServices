using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class TechnicianNewResponseOrderDetailsView : ContentPage
{
	public TechnicianNewResponseOrderDetailsView(string Token,Technician technician,Order order)
	{
		InitializeComponent();
		BindingContext=new TechnicianNewResponseOrderDetailsViewModel(Token, technician, order);
	}
}