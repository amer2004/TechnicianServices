using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class ResponseDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Response _Response;
        public ObservableCollection<TechniciansRating> Ratings { get; set; } = new ObservableCollection<TechniciansRating>();
        public Order Order { get; }

        [ObservableProperty]
        public Technician? _Technician;
        public ResponseDetailsViewModel(string Token, Response response, Order order) : base(Token)
        {
            Response = response;
            Order = order;
            OnLoad();
        }
        public async void OnLoad()
        {
            await GetTechnicianRatings();
            await GetTechnicianDetails();
        }

        public async Task GetTechnicianDetails()
        {
            if (IsBesy)
                return;

            Technician = await _tichnicianService.GetTechnician(Response.technicianId);

            IsBesy = false;
        }

        public async Task GetTechnicianRatings()
        {
            if (IsBesy)
                return;
            IsBesy = true;
            var res = await _ratingService.GetTechnicianRatings(Response.technicianId);
            foreach (var rating in res)
            {
                Ratings.Add(rating);
            }

            IsBesy = false;
        }

        [RelayCommand]
        public async Task ChooseOrderResponse()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            var res = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A109")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
            if (res)
            {
                var result = await _orderService.ChooseOrderResponse(Response.OrderId, Response.id);
                if (result)
                {
                    await _orderService.ChangeOrderStatus(Order.id,4);// 4 is a responce hase been chosen status
                    await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S109")}", $"{LangHelper.GetString("Ok")}");
                    await _notificationService.AddNotification(new UserNotifcationDto
                    {
                        UserId = Technician.userId,
                        Title = "response is choosen",
                        Message = $"your response to the order with the number:{Order.orderNumber} has been choosen by the user"
                    });
                    await App.Current.MainPage.Navigation.PopToRootAsync();
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
