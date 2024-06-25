namespace TechnicalServices.Services
{
    public class TechnicianService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public TechnicianService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }
        public TechnicianService()
        {
            Client = new HttpClient();
            this.Token = "";
        }
        public async Task<Technician> GetTechnician(int TechId)
        {
            if (Token == "")
                return new Technician();

            string url = $"{BaseUrl}/Technicians/GetTechnicianById/{TechId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<Technician>();
                if (res != null)
                {
                    return res;
                }
            }
            return new Technician();
        }
       
        public async Task<Technician> GetTechnicianByUserId(int UserId)
        {
            if (Token == "")
                return new Technician();

            string url = $"{BaseUrl}/Technicians/GetByUserId/{UserId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<Technician>();
                if (res != null)
                {
                    return res;
                }
            }
            return new Technician();
        }

        public async Task<bool> IsUniqueUserName(string UserName)
        {
            string url = $"{BaseUrl}/Technicians/GetIsUniqueUserName/{UserName}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return new bool();
        }
        
        public async Task<bool> IsUniqueSSN(string SSN)
        {
            string url = $"{BaseUrl}/Technicians/GetIsUniqueSSN/{SSN}";
            var responce = await Client.GetAsync(url);
            if (responce.IsSuccessStatusCode)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            return new bool();
        }
        public async Task<Technician> AddTechnican(TechnicianDto dto)
        {
            string url = $"{BaseUrl}/Technicians/AddTechnican";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var Tec = await response.Content.ReadFromJsonAsync<Technician>();

                if (Tec is not null)
                {
                    return Tec;
                }
            }
            return new Technician();
        }
    }
}
