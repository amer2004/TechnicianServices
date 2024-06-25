using TechnicalServices.Dtos.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class AddTechnicianFeedBackViewModel : BaseViewModel
    {
        public Order Order { get; }

        [ObservableProperty]
        public string _Message = "";

        public AddTechnicianFeedBackViewModel(string Token, Order order):base(Token)
        {
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
                var feedback = new TechnicianFeedBackDto
                {
                    Message = Message,
                    OrderId = Order.id,
                };

                var res = await _feedbackService.AddTechnicianFeedBack(feedback);
                if (res != null)
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
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E101")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }
    }
}
