namespace TechnicalServices.MVVM.Views;

public partial class LogInView : ContentPage
{
	public LogInView()
	{
		InitializeComponent();
		BindingContext =new LogInViewModel();
	}
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}