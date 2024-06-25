namespace TechnicalServices.MVVM.ViewModel
{
    public partial class ForgotenPasswordResetViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _password = "";

        [ObservableProperty]
        public string _confirmPassword = "";

        private string email = "";

        public ForgotenPasswordResetViewModel(string email) : base()
        {
            this.email = email;
        }

        [RelayCommand]
        public async Task Continue()
        {

            if (IsBesy)
                return;

            IsBesy = true;
            if (Password != "" && ConfirmPassword != "")
            {
                if (Password == ConfirmPassword)
                {
                    if (IsValedPassword(Password))
                    {
                        var res = await _userService.ChangePassword(email, Password);
                        if (res)
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S104")}", $"{LangHelper.GetString("Ok")}");
                            await App.Current.MainPage.Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E109")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E110")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }

            IsBesy = false;
        }

        private bool IsValedPassword(string password)
        {
            if (password.Length < 8)
                return false;

            return true;
        }
    }
}
