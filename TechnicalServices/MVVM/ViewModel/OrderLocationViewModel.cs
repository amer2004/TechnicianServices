namespace TechnicalServices.MVVM.ViewModel
{
    public partial class OrderLocationViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Order _Order;
        public OrderLocationViewModel(Order order) : base()
        {
            Order = order;
        }

    }
}
