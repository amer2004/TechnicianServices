namespace TechnicalServices.MVVM.Views;

public partial class LocationView : ContentPage
{
	public LocationView(UserDto dto)
	{
		InitializeComponent();
		BindingContext = new LocationViewModel(dto);
	}
    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}