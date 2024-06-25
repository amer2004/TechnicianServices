namespace TechnicalServices.Services
{
    public class RatingService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public RatingService()
        {
            Client = new HttpClient();
            Token = "";
        }

        public RatingService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }

        public async Task<List<RatingType>> GetRatingsType()
        {
            string url = $"{BaseUrl}/RatingType/GetAllRatingTypes";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Types = await response.Content.ReadFromJsonAsync<List<RatingType>>();
                if (Types is not null)
                {
                    return Types;
                }
            }
            return new List<RatingType>();
        }
        public async Task<bool> AddUserFeedBackRatings(List<UserFeedBackRatingDto> dtos)
        {
            string url = $"{BaseUrl}/UserFeedBackRatings/AddUserFeedBackRatings";
            var json = JsonSerializer.Serialize(dtos);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<bool>();
                if (res)
                {
                    return res;
                }
            }
            return false;
        }
        public async Task<List<TechniciansRating>> GetTechnicianRatings(int TechId)
        {
            if (Token == "")
                return new List<TechniciansRating>();

            string url = $"{BaseUrl}/TechnicianRating/GetRatingByTechnicianId/{TechId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<TechniciansRating>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<TechniciansRating>();
        }

    }
}
