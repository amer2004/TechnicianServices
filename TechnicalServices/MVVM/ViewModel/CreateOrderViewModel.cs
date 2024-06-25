using TechnicalServices.Dtos;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class CreateOrderViewModel : BaseViewModel
    {
        public User User { get; }
        public string Token { get; }
        public int ExtendServiceId { get; }
        public bool IsDone { get; set; } = false;

        [ObservableProperty]
        public string _Desc = "";

        [ObservableProperty]
        public DateTime _Date = DateTime.Now.AddDays(1);

        [ObservableProperty]
        public DateTime _MaxDate = DateTime.Now.AddDays(7);

        public CreateOrderViewModel(User user, string Token, int ExtendServiceId) : base(Token)
        {
            User = user;
            this.Token = Token;
            this.ExtendServiceId = ExtendServiceId;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E105")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            if (IsBesy)
                return;

            IsBesy = true;
            if (Desc != "")
            {
                var Order = new OrderDto
                {
                    UserId = User.id,
                    ExtendServiceId = ExtendServiceId,
                    Description = Desc,
                    Date = DateOnly.FromDateTime(Date),
                };
                await App.Current.MainPage.Navigation.PushAsync(new AddOrderLocationView(Token, Order));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }
    }
}
