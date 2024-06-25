namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianOrderDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Order _Order;
        public string Token { get; }

        [ObservableProperty]
        public bool _IsNotFinished = true;
        public TechnicianOrderDetailsViewModel(string Token, Order order) : base(Token)
        {
            this.Token = Token;
            Order = order;
            if (Order.statusId is 6 or 7 or 8)
            {
                IsNotFinished = false;
            }
        }

        [RelayCommand]
        public async Task MarkAsFinished()
        {
            if (IsBesy)
                return;
            IsBesy = true;

            var result= await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}",$"{LangHelper.GetString("A110")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
            if (result)
            {
                var res = await _orderService.ChangeOrderStatus(Order.id, 6);
                if (res)
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S110")}", $"{LangHelper.GetString("Ok")}");
                    await _notificationService.AddNotification(new UserNotifcationDto
                    {
                        UserId = Order.userId,
                        Title = $"order:{Order.orderNumber} is finished",
                        Message = $"your order with the number:{Order.orderNumber} is marked as finished by the technician go to the order details to finish the order"

                    });
                    var ans = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A104")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
                    if (ans)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new AddTechnicianFeedBackView(Token, Order));
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            IsBesy = false;
        }
    }
}
