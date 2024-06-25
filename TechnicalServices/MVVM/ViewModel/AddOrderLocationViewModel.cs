using TechnicalServices.Dtos;
namespace TechnicalServices.MVVM.ViewModel
{
    public partial class AddOrderLocationViewModel : BaseViewModel
    {
        public OrderDto Order { get; }
        public AddOrderLocationViewModel(string Token, OrderDto order) : base(Token)
        {
            Order = order;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (IsBesy)
                return;

            IsBesy = true;

            //for testing
            Order.XLocation = 36.6666;
            Order.YLocation = 36.6666;

            var res = await _orderService.AddNewOrder(Order);
            if (res != null)
            {
                await _notificationService.AddNewOrderNotifcations(Order.ExtendServiceId);

                var type = await _paymentService.GetPyment(2); //2 is new order payment
                if (type != null)
                {
                    var result = await _paymentService.AddUserPayment(new UserPaymentDto
                    {
                        UserId = Order.UserId,
                        PaymentAmount = type.amount,
                        PaymentTypeId = type.id,
                    });

                    if (result!=null)
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S101")}", $"{LangHelper.GetString("Ok")}");
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }
    }
}
