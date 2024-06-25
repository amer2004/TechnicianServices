using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.Views;

public partial class TechnicianOrdersView : ContentPage
{
    public TechnicianOrdersView(string Token, Technician technician)
    {
        InitializeComponent();
        BindingContext = new TechnicianOrdersViewModel(Token, technician);
    }
}