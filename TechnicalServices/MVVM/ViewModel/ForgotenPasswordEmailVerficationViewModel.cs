using System.Diagnostics;
using System.Timers;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class ForgotenPasswordEmailVerficationViewModel : BaseViewModel
    {
        [ObservableProperty]
        public bool _isresendEnabled;

        [ObservableProperty]
        public int? _code = null;

        private int TheCode;

        private string email;

        Thread waiter;
        public ForgotenPasswordEmailVerficationViewModel(string email, int code) : base()
        {
            TheCode = code;
            this.email = email;
            IsresendEnabled = false;
            waiter = new Thread(SetIsResendTrue);
            waiter.Start();
        }
        public void SetIsResendTrue()
        {
            Thread.Sleep(150000);
            IsresendEnabled = true;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            if (Code is not null)
            {
                if (TheCode != 0)
                {
                    if (Code == TheCode)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new ForgotenPasswordResetView(email));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E108")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task ResendCode()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;
            TheCode = await _userService.SendVerificationcode(email);
            if (TheCode == 0)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
            }

            IsresendEnabled = false;
            IsBesy = false;
        }

    }
}
