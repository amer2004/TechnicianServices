namespace TechnicalServices.MVVM.ViewModel
{
    public partial class HomePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public User _User;

        [ObservableProperty]
        public Technician? _Technician;

        private string Token;
        private readonly TabbedPage page;

        [ObservableProperty]
        public bool _IsActive = false;

        public HomePageViewModel(User user, string Token, TabbedPage page) : base(Token)
        {
            this._User = user;
            this.Token = Token;
            this.page = page;
            OnLoad();
        }

        public async void OnLoad()
        {
            if (User.userNotifcations != null)
            {
                Notifcations = new ObservableCollection<UserNotifcation>(User.userNotifcations.OrderByDescending(n => n.dateTime));
            }
            else
            {
                await GetNotification();
            }

            await GetMainServices();
            await GetTechnichen();
            await GetUserOrders();

            var p = App.Current.MainPage.Navigation.NavigationStack.ElementAtOrDefault(0);
            App.Current.MainPage.Navigation.InsertPageBefore(page, p);
            App.Current.MainPage.Navigation.RemovePage(p);
        }
        public async Task GetTechnichen()
        {
            if (!User.isTechnician)
                return;

            IsBesy = true;
            var res = await _tichnicianService.GetTechnicianByUserId(User.id);
            if (res != null)
            {
                Technician = res;
                if (Technician.statusId == 2)
                {
                    IsActive = true;
                }
            }
            IsBesy = false;
        }

        //---------------Notifications----------------//

        [ObservableProperty]
        public bool _IsRefreshing = false;
        public ObservableCollection<UserNotifcation>? Notifcations { get; set; }

        [RelayCommand]
        public async Task GetNotification()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;
            Notifcations.Clear();
            var usernotifcations = await _notificationService.GetUserNotifcation(User.id);

            foreach (var item in usernotifcations.OrderByDescending(n => n.dateTime))
            {
                Notifcations.Add(item);
            }

            IsRefreshing = false;
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToNotificationDetails(UserNotifcation notifcation)
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new NotificationDetailsView(notifcation, Token));
            await MarkAsRead(notifcation);
        }

        [RelayCommand]
        public async Task MarkAsRead(UserNotifcation notifcation)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            if (notifcation.status)
            {
                var res = await _notificationService.MarkNotificatonAsRead(notifcation.id);
                await GetNotification();
            }
        }

        //------------------Services------------------//

        public ObservableCollection<MainService> MainServices { get; set; } = new ObservableCollection<MainService>();

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
                item.PicName = $"{item.name.ToLower().Replace(" ", "_")}.png";
                item.name = LangHelper.GetString(item.name.Replace(" ", ""));
                MainServices.Add(item);
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToExtendService(MainService mainService)
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new ExtendServiceView(mainService, Token, User));
        }

        //-------------------Orders-------------------//

        [ObservableProperty]
        public bool _IsOrderRefreshing = false;
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        [RelayCommand]
        public async Task GetUserOrders()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            Orders.Clear();

            IsBesy = true;
            var orders = await _orderService.GetUserOrders(User.id);
            foreach (var item in orders.OrderByDescending(o => o.date))
            {
                item.ExtendService.name = LangHelper.GetString(item.ExtendService.name.Replace(" ",""));
                item.Status.name = LangHelper.GetString(item.Status.name.Replace(" ", ""));
                Orders.Add(item);
            }

            IsOrderRefreshing = false;
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToOrderDetails(Order order)
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new OrderDetailsView(Token, order));
        }

        //------------------Response------------------//


        [RelayCommand]
        public async Task GoToMyOrders()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new TechnicianOrdersView(Token, Technician));
        }

        [RelayCommand]
        public async Task GoToNewOrders()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new TechnicianNewOrdersView(Token, Technician));
        }

        //------------------Settings------------------//

        [RelayCommand]
        public async Task LogOut()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LogInView());
        }

        [RelayCommand]
        public async Task GoToChangePassword()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new ForgotenPasswordView());
        }

        [RelayCommand]
        public async Task GoToUserPayments()
        {
            if (IsBesy)
                return;
            await App.Current.MainPage.Navigation.PushAsync(new UserPaymentsView(Token, User.id));
        }

        [RelayCommand]
        public async Task GoToTechnicianRatings()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new TechnicianRatingsView(Token, Technician));

        }

        [RelayCommand]
        public async Task GoToTechnicianServices()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new TechnicianServicesView(Token, Technician));
        }

        [RelayCommand]
        public async Task GoToUserIncomes()
        {
            if (IsBesy)
                return;
            await App.Current.MainPage.Navigation.PushAsync(new UserIncomesView(Token, User.id));
        }
    }
}