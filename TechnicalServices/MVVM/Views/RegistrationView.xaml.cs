namespace TechnicalServices.MVVM.Views;
public partial class RegistrationView : ContentPage
{   
    public RegistrationView()
    {
        InitializeComponent();
        BindingContext =new RegistrationViewModel();
    }
}