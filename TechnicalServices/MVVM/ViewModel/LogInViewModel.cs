using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class LogInViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _email = "";

        [ObservableProperty]
        public string _password = "";

        public LogInViewModel() : base() {}

        [RelayCommand]
        public async Task LogeIn()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;

            if (Email != "" && Password != "")
            {
                if (Email.Contains("@gmail.com"))
                {
                    var Token = await _userService.LogIn(Email, Password);
                    if (Token != string.Empty)
                    {
                        var user = await _userService.LogInCheack(Email, Password);
                        if (user.isActive == true)
                        {
                            if (user.isTechnician)
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new TechnicaianHomePageView(user, Token)));
                            }
                            else
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new UserHomePageView(user,Token)));
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E104")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E103")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E102")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }

            IsBesy = false;
        }

        [RelayCommand]
        public async Task Register()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new RegistrationView());
        }

        [RelayCommand]
        public async Task ForgotPassword()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new ForgotenPasswordView());
        }

        [RelayCommand]
        public async Task Visitor()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new VisitorHomePageView());
        }
    }
}
