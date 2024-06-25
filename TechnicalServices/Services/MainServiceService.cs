namespace TechnicalServices.Services
{
    public class MainServiceService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public MainServiceService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }

        public MainServiceService()
        {
            Client = new HttpClient();
            Token = "";
        }

        //------------------------Services-----------------------//
        public async Task<List<MainService>> GetMainServices()
        {
            string url = $"{BaseUrl}/MainServices/GetAllMainServices";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<MainService>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<MainService>();
        }

        public async Task<List<TechniciansServices>> GetTechnicianServices(int id)
        {
            if (Token == "")
                return new List<TechniciansServices>();

            string url = $"{BaseUrl}/TechnicionServises/GetTechnicianServices/{id}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<TechniciansServices>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<TechniciansServices>();
        }

        public async Task<List<ExtendService>> GetExtendServices(int MainServId)
        {
            string url = $"{BaseUrl}/ExtandServices/GetByMainServiceId/{MainServId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<ExtendService>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<ExtendService>();
        }
    }
}
