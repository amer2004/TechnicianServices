namespace TechnicalServices.MVVM.Views;

public partial class UserHomePageView : TabbedPage
{
	public UserHomePageView(User User,string Token)
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel(User,Token, this);
	}

    protected override bool OnBackButtonPressed()
	{
		return true;
    }
}