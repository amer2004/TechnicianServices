namespace TechnicalServices.MVVM.Views;

public partial class ForgotenPasswordResetView : ContentPage
{
	public ForgotenPasswordResetView(string email)
	{
		InitializeComponent();
		BindingContext = new ForgotenPasswordResetViewModel(email);
	}
    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}