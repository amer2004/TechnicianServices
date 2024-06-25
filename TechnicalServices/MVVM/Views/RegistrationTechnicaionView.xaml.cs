namespace TechnicalServices.MVVM.Views;

public partial class RegistrarionTechnicaionView : ContentPage
{
	public RegistrarionTechnicaionView(UserDto dto)
	{
		InitializeComponent();
		BindingContext=new RegistrarionTechnicaionViewModel(dto);
	}
}