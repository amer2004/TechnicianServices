using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Technician _Technician;
        public ObservableCollection<TechniciansRating> Ratings { get; set; } = new ObservableCollection<TechniciansRating>();

        public TechnicianDetailsViewModel(string Token,Technician technician):base(Token)
        {
            Technician = technician;
            OnLoad();
        }

        public async void OnLoad()
        {
            await GetTechnicianRatings();
        }

        public async Task GetTechnicianRatings()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            var res = await _ratingService.GetTechnicianRatings(Technician.id);
            foreach (var rating in res)
            {
                rating.ratingType.name = LangHelper.GetString(rating.ratingType.name);
                Ratings.Add(rating);
            }

            IsBesy = false;
        }

    }
}
