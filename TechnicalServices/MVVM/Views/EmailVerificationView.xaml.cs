namespace TechnicalServices.MVVM.Views;

public partial class EmailVerificationView : ContentPage
{
    public EmailVerificationView(UserDto dto,int code)
    {
        InitializeComponent();
        BindingContext = new EmailVerificationViewModel(dto,code);
    }
    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}