namespace TechnicalServices.MVVM.Views;

public partial class ForgotenPasswordView : ContentPage
{
	public ForgotenPasswordView()
	{
		InitializeComponent();
        BindingContext=new ForgotenPasswordViewModel();
    }
}