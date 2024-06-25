using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class TechnicianRatingsView : ContentPage
{
	public TechnicianRatingsView(string Token,Technician technician)
	{
		InitializeComponent();
		BindingContext = new TechnicianRatingsViewModel(Token,technician);
	}
}