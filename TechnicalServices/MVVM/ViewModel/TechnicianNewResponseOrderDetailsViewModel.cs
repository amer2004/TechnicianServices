using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianNewResponseOrderDetailsViewModel:BaseViewModel
    {
        public string Token { get; }
        public Technician Technician { get; }
        [ObservableProperty]
        public Order _Order;
        public TechnicianNewResponseOrderDetailsViewModel(string Token,Technician technician,Order order)
        {
            this.Token = Token;
            Technician = technician;
            Order = order;
        }

        [RelayCommand]
        public async Task GoToAddOrderResponce()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new AddOrderResponseView(Token, Technician, Order));
        }
    }
}
