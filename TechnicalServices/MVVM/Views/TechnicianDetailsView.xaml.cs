using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class TechnicianDetailsView : ContentPage
{
	public TechnicianDetailsView(string Token,Technician technician)
	{
		InitializeComponent();
		BindingContext = new TechnicianDetailsViewModel(Token,technician);
	}
}