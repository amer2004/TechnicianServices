namespace TechnicalServices.MVVM.ViewModel
{
    public partial class LocationViewModel : BaseViewModel
    {
        public UserDto User { get; set; }

        [ObservableProperty]
        public bool isRead;

        public LocationViewModel(UserDto dto):base()
        {
            User = dto;
        }

        [RelayCommand]
        public async Task Continue()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if(IsBesy)
                return;

            IsBesy = true;
            
            //for test only
            User.XLocation = 36.36665243;
            User.YLocation = 36.36665243;

            if (IsRead)
            {
                if (!User.IsTechnician)
                {
                    var res = await _userService.AddUser(User);
                    if (res is not null)
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("ُS105")}", $"{LangHelper.GetString("Ok")}");
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new RegistrarionTechnicaionView(User));
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A102")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy =false;
        }

    }
}
