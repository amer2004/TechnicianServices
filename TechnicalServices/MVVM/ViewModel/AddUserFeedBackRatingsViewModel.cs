namespace TechnicalServices.MVVM.ViewModel
{
    public partial class AddUserFeedBackRatingsViewModel : BaseViewModel
    {
        public UserFeedBack FeedBack { get; }
        public List<RatingType>? Ratings { get; set; }
        public ObservableCollection<UserFeedBackRating> RatingDtos { get; set; } = [];
        public AddUserFeedBackRatingsViewModel(string Token, UserFeedBack feedBack) : base(Token)
        {
            FeedBack = feedBack;
            OnLoad();
        }

        public async void OnLoad()
        {
            Ratings = await _ratingService.GetRatingsType();
            if (Ratings.Count != 0)
            {
                
                for (int i = 0; i < Ratings.Count; i++)
                {

                    RatingDtos.Add(new UserFeedBackRating
                    {
                        ratingTypeId = Ratings[i].id,
                        ratingType = Ratings[i],
                        userFeedBackId = FeedBack.id,
                        value=5
                    });
                    RatingDtos[i].ratingType.name = LangHelper.GetString(RatingDtos[i].ratingType.name);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
            }
        }

        [RelayCommand]
        public async Task Confirm()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            var Dtos = new List<UserFeedBackRatingDto>();
            foreach (var item in RatingDtos) 
            {
                Dtos.Add ( new UserFeedBackRatingDto { 
                Value=item.value,
                UserFeedBackId = FeedBack.id,
                RatingTypeId = item.ratingTypeId,
                });
            }
            var res= await _ratingService.AddUserFeedBackRatings(Dtos);
            if (res)
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Successfully")}", $"{LangHelper.GetString("S103")}", $"{LangHelper.GetString("Ok")}");
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert($"{LangHelper.GetString("Error")}", $"{LangHelper.GetString("E106")}", $"{LangHelper.GetString("Ok")}");
            }
            IsBesy=false;
        }
    }
}
