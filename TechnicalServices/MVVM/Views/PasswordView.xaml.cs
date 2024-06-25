namespace TechnicalServices.MVVM.Views;

public partial class PasswordView : ContentPage
{
	public PasswordView(UserDto dto)
	{
		InitializeComponent();
		BindingContext = new PasswordViewModel(dto);
	}
    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}