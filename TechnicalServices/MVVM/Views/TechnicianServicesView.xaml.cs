using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;
public partial class TechnicianServicesView : ContentPage
{
    public TechnicianServicesView(string Token, Technician technician)
    {
        InitializeComponent();
        BindingContext = new TechnicianServicesViewModel(Token, technician);
    }
}