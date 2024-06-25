using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Order _Order;

        [ObservableProperty]
        public Technician _Technician = new Technician();
        public string Token { get; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFinished))]
        public bool _IsNotfineshed = true;

        public bool IsFinished => !IsNotfineshed;
        public OrderDetailsViewModel(Order Order, string Token) : base(Token)
        {
            this.Order = Order;
            this.Token = Token;
            OnLoad();
        }

        public async void OnLoad()
        {
            if (this.Order.chosenResponseId != null)
            {
                await GetTechnicianDetails();
            }

            if (Order.statusId == 1 | Order.statusId == 8)
            {
                IsNotfineshed = false;
            }

            if (Order.statusId == 6)
            {
                var res = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A103")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
                if (res)
                {
                    var resulte = await _orderService.ChangeOrderStatus(Order.id, 7);// 7 is the user is good with the order
                    if (resulte)
                    {
                        await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S106")}", $"{LangHelper.GetString("Ok")}");
                        IsNotfineshed = false;
                        var ans = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A104")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
                        if (ans)
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new AddUserFeedBackView(Token, Order));
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
                    var ans = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A105")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
                    if (ans)
                    {
                        var resulte = await _orderService.ChangeOrderStatus(Order.id, 8);// 8 is the admin review status
                        if (resulte)
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S107")}", $"{LangHelper.GetString("Ok")}");

                            await _notificationService.AddNotification(new UserNotifcationDto
                            {
                                UserId = Technician.userId,
                                Title = "Order is been review",
                                Message = $" the order with the number {Order.orderNumber} is been reviewed by an admin to insure that the order is finished"
                            });

                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
                        }
                    }
                }
            }
        }

        public async Task GetTechnicianDetails()
        {
            if (IsBesy)
                return;
            IsBesy = true;

            var res = await _tichnicianService.GetTechnician(Order.ChosenResponse.technicianId);
            if (res != null)
            {
                Technician = res;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToTechnicianDetails()
        {
            if (IsBesy)
                return;
            IsBesy = true;

            if (Technician.id != 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new TechnicianDetailsView(Token, Technician));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A106")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToOrderLocation()
        {
            if (IsBesy)
                return;

            await App.Current.MainPage.Navigation.PushAsync(new OrderLocationView(Order));
        }

        [RelayCommand]
        public async Task DeleteOrder()
        {
            if (IsBesy)
                return; 
            
            IsBesy = true;

            if (Order.statusId == 3)
            {
                var res = await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A107")}", $"{LangHelper.GetString("Yes")}", $"{LangHelper.GetString("No")}");
                if (!res)
                    return;

                var resulte = await _orderService.ChangeOrderStatus(Order.id, 1); //1 is the deleted by user status
                if (resulte)
                {
                    var type = await _paymentService.GetPyment(3);
                    if (type != null)
                    {
                        var result = await _paymentService.AddUserPayment(new UserPaymentDto
                        {
                            UserId = Order.userId,
                            PaymentAmount = type.amount,
                            PaymentTypeId = type.id,
                        });

                        if (result != null)
                        {
                            await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S108")}", $"{LangHelper.GetString("Ok")}");
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
            }
            IsBesy = false;
        }

        [RelayCommand]
        public async Task GoToOrderResponses()
        {
            if (IsBesy)
                return;

            if (Order.chosenResponseId != null)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Alert")}", $"{LangHelper.GetString("A108")}", $"{LangHelper.GetString("Ok")}");
                return;
            }

            await App.Current.MainPage.Navigation.PushAsync(new OrderResponsesView(Token, Order));
        }

    }
}
