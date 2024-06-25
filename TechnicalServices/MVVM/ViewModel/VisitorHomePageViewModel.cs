namespace TechnicalServices.MVVM.ViewModel
{
    public partial class VisitorHomePageViewModel : BaseViewModel
    {
        public ObservableCollection<string>Messages { get; set; }
        public ObservableCollection<MainService> MainServices { get; set; } = new ObservableCollection<MainService>();
        public VisitorHomePageViewModel() : base()
        {
            Messages = new ObservableCollection<string>{
            new string($"{LangHelper.GetString("M101")}"),
            new string($"{LangHelper.GetString("M102")}"),
            //to be complated
            };
            OnLoad();
        }

        public async void OnLoad()
        {
           await GetMainServices();
        }

        [RelayCommand]
        public async Task GetMainServices()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;
            var mainservices = await _mainServiceService.GetMainServices();
            foreach (var item in mainservices)
            {
                if (item.name != "Unknown Problem")
                {
                    item.PicName = $"{item.name.ToLower().Replace(" ", "_")}.png";
                    item.name = LangHelper.GetString(item.name.Replace(" ", ""));
                    MainServices.Add(item);
                }
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToExtendService(MainService mainService)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ExtendServiceView(mainService));
        }
    }
}
