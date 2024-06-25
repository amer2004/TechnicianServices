namespace TechnicalServices.MVVM.Views;

public partial class TechnicaianHomePageView : TabbedPage
{
	public TechnicaianHomePageView(User User, string Token)
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel(User,Token,this);
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}