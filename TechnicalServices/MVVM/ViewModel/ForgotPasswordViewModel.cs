namespace TechnicalServices.MVVM.ViewModel
{
    public partial class ForgotenPasswordViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _Email = "";

        [RelayCommand]
        public async Task Confirm()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;
            IsBesy = true;

            if (Email != "")
            {
                if (Email.Contains("@gmail.com"))
                {
                    if (!await _userService.IsUniqueEmail(Email))
                    {
                        var code = await _userService.SendVerificationcode(Email);
                        if (code != 0)
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new ForgotenPasswordEmailVerficationView(Email,code));
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E111")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E112")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }
    }
}
