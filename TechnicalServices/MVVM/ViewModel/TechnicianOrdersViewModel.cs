using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianOrdersViewModel : BaseViewModel
    {
        public string Token { get; }
        public Technician Technician { get; }

        [ObservableProperty]
        public bool _IsTechOrderRefreshing = false;
        public ObservableCollection<Order> TechOrders { get; set; } = new ObservableCollection<Order>();

        public TechnicianOrdersViewModel(string Token, Technician technician) : base(Token)
        {
            this.Token = Token;
            Technician = technician;
            OnLoad();
        }

        public async void OnLoad()
        {
            await GetTechOrder();
        }

        [RelayCommand]
        public async Task GetTechOrder()
        {
            if (IsBesy)
                return;

            if (Technician == null)
                return;

            TechOrders.Clear();
            IsBesy = true;
            var res = await _orderService.GetTechnicianOrders(Technician.id);
            foreach (var item in res.OrderByDescending(o=>o.date))
            {
                item.ExtendService.name = LangHelper.GetString(item.ExtendService.name.Replace(" ", ""));
                item.Status.name = LangHelper.GetString(item.Status.name.Replace(" ", ""));
                TechOrders.Add(item);
            }

            IsBesy = false;
            IsTechOrderRefreshing = false;
        }

        [RelayCommand]
        public async Task GoToOrderDetails(Order order)
        {
            await App.Current.MainPage.Navigation.PushAsync(new TechnicianOrderDetailsView(Token,order));
        }
    }
}
