using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianNewOrdersViewModel : BaseViewModel
    {
        public string Token { get; }
        public Technician Technician { get; }
        [ObservableProperty]
        public bool _IsNewTechOrderRefreshing = false;
        public ObservableCollection<Order> NewOrders { get; set; } = new ObservableCollection<Order>();

        public TechnicianNewOrdersViewModel(string Token, Technician technician) : base(Token)
        {
            this.Token = Token;
            Technician = technician;
            OnLoad();
        }
        public async void OnLoad()
        {
            await GetNewOrders();
        }

        [RelayCommand]
        public async Task GetNewOrders()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            IsNewTechOrderRefreshing = true;
            IsBesy = true;

            NewOrders.Clear();
            var res = await _orderService.GetTechnicianNewOrders(Technician.id);
            foreach (var item in res.OrderByDescending(o=>o.date))
            {
                item.ExtendService.name = LangHelper.GetString(item.ExtendService.name.Replace(" ", ""));
                item.Status.name = LangHelper.GetString(item.Status.name.Replace(" ", ""));
                NewOrders.Add(item);
            }

            IsNewTechOrderRefreshing = false;
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToAddOrderResponce(Order order)
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new TechnicianNewResponseOrderDetailsView(Token, Technician, order));
        }
    }
}
