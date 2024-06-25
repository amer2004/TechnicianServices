namespace TechnicalServices.MVVM.Views;

public partial class ForgotenPasswordEmailVerficationView : ContentPage
{
	public ForgotenPasswordEmailVerficationView(string email,int code)
	{
		InitializeComponent();
		BindingContext=new ForgotenPasswordEmailVerficationViewModel(email,code);
	}
}