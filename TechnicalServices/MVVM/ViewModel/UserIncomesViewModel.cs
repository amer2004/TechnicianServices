namespace TechnicalServices.MVVM.ViewModel
{
    public partial class UserIncomesViewModel : BaseViewModel
    {
        public int UserId { get; }

        [ObservableProperty]
        public bool _IsRefreshing = false;
        public ObservableCollection<UserIncom> UserIncoms { get; set; } = new ObservableCollection<UserIncom>();
        public UserIncomesViewModel(string Token, int UserId) : base(Token)
        {
            this.UserId = UserId;
            OnLoad();
        }
        public async void OnLoad()
        {
            await GetUserIncomes();
        }
        [RelayCommand]
        public async Task GetUserIncomes()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            IsRefreshing = true;

            UserIncoms.Clear();

            var res = await _userService.GetUserIncoms(UserId);
            foreach (var item in res)
            {
                UserIncoms.Add(item);
            }

            IsBesy = false;
            IsRefreshing = false;
        }

    }
}
