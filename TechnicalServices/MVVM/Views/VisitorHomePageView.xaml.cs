namespace TechnicalServices.MVVM.Views;

public partial class VisitorHomePageView : ContentPage
{
	public VisitorHomePageView()
	{
		InitializeComponent();
		BindingContext=new VisitorHomePageViewModel();
	}
}