namespace TechnicalServices.MVVM.Views;

public partial class ExtendServiceView : ContentPage
{
	public ExtendServiceView(MainService mainService)
	{
		InitializeComponent();
		BindingContext=new ExtendServiceViewModel(mainService);
	}

    public ExtendServiceView(MainService mainService,string Token,User user)
    {
        InitializeComponent();
        BindingContext = new ExtendServiceViewModel(mainService,Token,user);
    }
}