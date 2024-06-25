namespace TechnicalServices.MVVM.ViewModel
{
    public partial class AddUserFeedBackViewModel : BaseViewModel
    {
        public string Token { get; }
        public Order Order { get; }

        [ObservableProperty]
        public string _Message = "";
        public AddUserFeedBackViewModel(string Token, Order order) : base(Token)
        {
            this.Token = Token;
            Order = order;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (IsBesy)
                return;

            IsBesy = true;

            if (Message != "")
            {
                var feedback = new UserFeedBackDto
                {
                    Message = Message,
                    OrderId = Order.id,
                };
                var res = await _feedbackService.AddUserFeedBack(feedback);
                if (res != null)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddUserFeedBackRatingsView(Token,res));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
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
