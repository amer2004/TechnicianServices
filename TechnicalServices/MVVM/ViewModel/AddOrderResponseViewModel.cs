using TechnicalServices.Dtos;
using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class AddOrderResponseViewModel : BaseViewModel
    {
        public Technician Technician { get; }
        public Order Order { get; }

        [ObservableProperty]
        public DateTime _Date = new DateTime();

        [ObservableProperty]
        public DateTime _MaxDate = new DateTime();

        [ObservableProperty]
        public double _ESPrice;

        [ObservableProperty]
        public double _ESTime;

        [ObservableProperty]
        public TimeSpan _Time;
        public AddOrderResponseViewModel(string Token, Technician technician, Order order) : base(Token)
        {
            Technician = technician;
            Order = order;
            Date = DateTime.Now.AddDays(1);
            MaxDate = new DateTime(order.date, new TimeOnly()).AddDays(7);
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (IsBesy)
                return;

            IsBesy = true;

            if (ESPrice != 0 && ESTime != 0)
            {
                if (ESTime <= 8)
                {
                    var Response = new ResponseDto
                    {
                        OrderId = Order.id,
                        TechnicianId = Technician.id,
                        EstimatedTime = ESTime,
                        EstimatedPrice = ESPrice,
                        Time = TimeOnly.FromTimeSpan(Time),
                        Date = DateOnly.FromDateTime(Date),
                    };

                    var res = await _orderService.AddOrderResponse(Response);
                    if (res)
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S102")}", $"{LangHelper.GetString("Ok")}");
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E107")}", $"{LangHelper.GetString("Ok")}");
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
