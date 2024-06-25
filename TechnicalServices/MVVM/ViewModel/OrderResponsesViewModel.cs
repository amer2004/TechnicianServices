namespace TechnicalServices.MVVM.ViewModel
{
    public partial class OrderResponsesViewModel : BaseViewModel
    {
        public string Token { get; }
        public Order Order { get; }
        public ObservableCollection<Response> Responses { get; set; } = new ObservableCollection<Response>();
        public OrderResponsesViewModel(string Token, Order order) : base(Token)
        {
            this.Token = Token;
            Order = order;
            OnLoad();
        }
        public void OnLoad()
        {
            foreach (var item in Order.Responses)
            {
                Responses.Add(item);
            }

        }

        [RelayCommand]
        public async Task GoToResponsesDetails(Response response)
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new ResponseDetailsView(Token, response,Order));
        }
    }
}
