namespace TechnicalServices.MVVM.ViewModel
{
    public partial class ExtendServiceViewModel : BaseViewModel
    {
        public MainService Service { get; set; }
        public string Token { get; } = "";
        public User User { get; } = new();
        public ObservableCollection<ExtendService> ExtendServices { get; set; } = new ObservableCollection<ExtendService>();
        public bool IsVisitor { get; }

        public ExtendServiceViewModel(MainService service) : base()
        {
            this.Service = service;
            IsVisitor = true;
            OnLoad();
        }

        public ExtendServiceViewModel(MainService service, string Token, User user) : base(Token)
        {
            this.Service = service;
            this.Token = Token;
            User = user;
            IsVisitor = false;
            OnLoad();
        }

        public async void OnLoad()
        {
            await GetExtendServices();
        }

        public async Task GetExtendServices()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            ExtendServices = new ObservableCollection<ExtendService>();
            var extendServices = await _mainServiceService.GetExtendServices(Service.id);
            foreach (var item in extendServices)
            {
                item.PicName = $"{item.name.ToLower().Replace(" ", "_")}.png";
                item.name = LangHelper.GetString(item.name.Replace(" ",""));
                ExtendServices.Add(item);
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToAddOrder(ExtendService service)
        {
            if (IsVisitor)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A101")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            var res = await _userService.GetIsAllowedToOrder(User.id);
            if (!res)
            {
                await App.Current.MainPage.DisplayAlert(LangHelper.GetString("Error"), $"{LangHelper.GetString("E120")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            await App.Current.MainPage.Navigation.PushAsync(new CreateOrderView(User, Token, service.id));
        }
    }
}
