namespace TechnicalServices.MVVM.ViewModel
{
    public partial class UserPaymentsViewModel : BaseViewModel
    {
        public int UserId { get; }
        [ObservableProperty]
        public bool _IsRefreshing = false;
        public ObservableCollection<UserPayment> Payments { get; set; } = new ObservableCollection<UserPayment>();
        public UserPaymentsViewModel(string Token, int UserId) : base(Token)
        {
            this.UserId = UserId;
            OnLoad();
        }
        public async void OnLoad()
        {
            await GetUserPayments();
        }

        [RelayCommand]
        public async Task GetUserPayments()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            IsRefreshing = true;

            Payments.Clear();

            var res = await _userService.GetUserPayments(UserId);
            foreach (var item in res)
            {
                item.paymentType.name = LangHelper.GetString(item.paymentType.name.Replace(" ", ""));
                Payments.Add(item);
            }
            IsBesy = false;
            IsRefreshing = false;
        }
    }
}
