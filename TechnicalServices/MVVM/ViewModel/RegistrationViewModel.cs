namespace TechnicalServices.MVVM.ViewModel
{
    partial class RegistrationViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string _firstName = "";

        [ObservableProperty]
        public string _lastName = "";

        [ObservableProperty]
        public string _email = "";

        [ObservableProperty]
        public string _phoneNumber = "";

        [ObservableProperty]
        public bool isTech;

        public RegistrationViewModel() : base()
        {

        }

        [RelayCommand]
        public async Task Continue()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;
            if (FirstName != "" && LastName != "" && PhoneNumber != "" && Email != "")
            {
                if (Email.Contains("@gmail.com"))
                {
                    if (isvalidPhoneNumber(PhoneNumber))
                    {
                        if (await _userService.IsUniqueEmail(Email))
                        {
                            if (await _userService.IsUniquePhoneNumber(PhoneNumber))
                            {
                                UserDto user = new UserDto
                                {
                                    Email = Email,
                                    PhoneNumber = PhoneNumber,
                                    FirstName = FirstName,
                                    LastName = LastName,
                                    IsTechnician = IsTech,
                                };

                                var code = await _userService.SendVerificationcode(Email);
                                if (code != 0)
                                {
                                    await App.Current.MainPage.Navigation.PushAsync(new EmailVerificationView(user, code), true);
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E117")}", $"{LangHelper.GetString("Ok")}");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E118")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E119")}", $"{LangHelper.GetString("Ok")}");
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
        private bool isvalidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            if (PhoneNumber.Length != 10)
            {
                return false;
            }
            if (PhoneNumber[2] != '9' && PhoneNumber[2] != '6' && PhoneNumber[2] != '3' && PhoneNumber[2] != '8' && PhoneNumber[2] != '5' && PhoneNumber[2] != '4')
            {
                return false;
            }
            return true;
        }
    }
}
