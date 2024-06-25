using TechnicalServices.Dtos.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class RegistrarionTechnicaionViewModel : BaseViewModel
    {
        public UserDto Dto { get; }

        [ObservableProperty]
        public string _UserName = "";

        [ObservableProperty]
        public string _SSN = "";

        [ObservableProperty]
        public bool _IsComp;
        public RegistrarionTechnicaionViewModel(UserDto dto) : base()
        {
            Dto = dto;
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
            if (!string.IsNullOrEmpty(SSN) || !string.IsNullOrEmpty(UserName))
            {
                if (SSN.Length == 11)
                {
                    if (await _tichnicianService.IsUniqueSSN(SSN))
                    {
                        if (UserName.Length > 6 || UserName.Length < 20)
                        {
                            if (await _tichnicianService.IsUniqueUserName(UserName))
                            {
                                var user = await _userService.AddUser(Dto);
                                if (user != null)
                                {
                                    var Tec = new TechnicianDto
                                    {
                                        SocialSecurityNumber = SSN,
                                        UserName = UserName,
                                        UserId = user.id,
                                        AccountType = IsComp ? 1 : 0,
                                    };

                                    var res= await _tichnicianService.AddTechnican(Tec);
                                    if (res != null)
                                    {
                                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S105")}", $"{LangHelper.GetString("Ok")}");
                                        await App.Current.MainPage.Navigation.PopToRootAsync();
                                    }
                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E113")}", $"{LangHelper.GetString("Ok")}");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E114")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E115")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E116")}", $"{LangHelper.GetString("Ok")}");
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
