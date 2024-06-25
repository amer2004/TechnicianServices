namespace TechnicalServices.Services
{
    public class FeedBackService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public FeedBackService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }

        public FeedBackService()
        {
            Client = new HttpClient();
            this.Token = "";
        }

        public async Task<UserFeedBack> AddUserFeedBack(UserFeedBackDto dto)
        {
            string url = $"{BaseUrl}/UserFeedBacks/AddFeedBack";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var feedBack = await response.Content.ReadFromJsonAsync<UserFeedBack>();

                if (feedBack is not null)
                {
                    return feedBack;
                }
            }
            return new UserFeedBack();
        }

        public async Task<TechnicianFeedBack> AddTechnicianFeedBack(TechnicianFeedBackDto dto)
        {
            if (Token == "")
                return new TechnicianFeedBack();

            string url = $"{BaseUrl}/TechnicianFeedBacks/AddTechnicianFeedBack";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var feedBack = await response.Content.ReadFromJsonAsync<TechnicianFeedBack>();

                if (feedBack is not null)
                {
                    return feedBack;
                }
            }
            return new TechnicianFeedBack();
        }
       
    }
}
