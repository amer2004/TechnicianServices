using TechnicalServices.Models.Technician;

namespace TechnicalServices.MVVM.ViewModel
{
    public partial class TechnicianServicesViewModel : BaseViewModel
    {
        public Technician Technician { get; }
        public ObservableCollection<ExtendService> ExtendServices { get; set; } = new ObservableCollection<ExtendService>();
        public TechnicianServicesViewModel(string Token, Technician technician) : base(Token)
        {
            Technician = technician;
            OnLoad();
        }
        public async void OnLoad()
        {
            await GetTechnicianServices();
        }
        public async Task GetTechnicianServices()
        {
            if (IsBesy)
                return;

            IsBesy = true;
            var res = await _mainServiceService.GetTechnicianServices(Technician.id);
            foreach (var item in res)
            {
                item.extendService.PicName = $"{item.extendService.name.ToLower().Replace(" ", "_")}.png";
                item.extendService.name = LangHelper.GetString(item.extendService.name.Replace(" ", ""));
                ExtendServices.Add(item.extendService);
            }

            IsBesy=false;
        }

    }
}
