namespace TechnicalServices.MVVM.Views;
public partial class UserIncomesView : ContentPage
{
    public UserIncomesView(string Token, int UserId)
    {
        InitializeComponent();
        BindingContext = new UserIncomesViewModel(Token, UserId);
    }
}