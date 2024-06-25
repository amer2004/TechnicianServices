using System.Globalization;

namespace TechnicalServices.MVVM.ViewModel
{
    partial class PasswordViewModel : BaseViewModel
    {
        public UserDto User { get; set; }

        [ObservableProperty]
        public string _password="";

        [ObservableProperty]
        public string _confirmPassword="";
        public PasswordViewModel(UserDto dto) : base()
        {
            User = dto;
        }

        [RelayCommand]
        public async Task Continue()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            if (Password!="" &&ConfirmPassword!="")
            {
                if (Password==ConfirmPassword)
                {
                    if (IsValedPassword(Password))
                    {
                        User.Password = Password;
                        await App.Current.MainPage.Navigation.PushAsync(new LocationView(User));
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

            IsBesy =false;
        }

        private bool IsValedPassword(string password)
        {
            if(password.Length<8)
                return false;

            return true;
        } 
    }
}
