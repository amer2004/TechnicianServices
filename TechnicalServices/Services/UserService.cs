
namespace TechnicalServices.Services
{
    public class UserService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public UserService(string token)
        {
            Client = new HttpClient();
            Token = token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }
        public UserService()
        {
            Client = new HttpClient();
            Token = "";
        }

        public async Task<string> LogIn(string Email, string Password)
        {
            string url = $"{BaseUrl}/Users/LogeIn/{Email}/{Password}";
            var resp = await Client.GetAsync(url);
            if (resp.IsSuccessStatusCode)
            {
                var temp = await resp.Content.ReadAsStringAsync();
                if (temp != null)
                {
                    return temp;
                }
            }
            return string.Empty;
        }

        public async Task<User> LogInCheack(string Email, string Password)
        {
            string url = $"{BaseUrl}/Users/LogeInCheack/{Email}/{Password}";
            var resp = await Client.GetAsync(url);
            if (resp.IsSuccessStatusCode)
            {
                var temp = await resp.Content.ReadFromJsonAsync<User>();
                if (temp != null)
                {
                    return temp;
                }
            }
            return new User();
        }

        public async Task<bool> GetIsAllowedToOrder(int UserId)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/Users/GetIsAllowedToOrder/{UserId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return new bool();
        }

        public async Task<List<UserIncom>> GetUserIncoms(int id)
        {
            if (Token == "")
                return new List<UserIncom>();

            string url = $"{BaseUrl}/UserIncoms/GetUserIncoms/{id}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Types = await response.Content.ReadFromJsonAsync<List<UserIncom>>();
                if (Types is not null)
                {
                    return Types;
                }
            }
            return new List<UserIncom>();
        }

        public async Task<List<UserPayment>> GetUserPayments(int id)
        {
            if (Token == "")
                return new List<UserPayment>();

            string url = $"{BaseUrl}/UserPayments/GetUserPayments/{id}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Types = await response.Content.ReadFromJsonAsync<List<UserPayment>>();
                if (Types is not null)
                {
                    return Types;
                }
            }
            return new List<UserPayment>();

        }
        public async Task<int> SendVerificationcode(string Email)
        {
            string url = $"{BaseUrl}/Users/SendVerificationCode/{Email}";
            var resp = await Client.GetAsync(url);
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }

        public async Task<bool> IsUniquePhoneNumber(string PhoneNumber)
        {
            string url = $"{BaseUrl}/Users/GetIsUniquePhoneNumber/{PhoneNumber}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return new bool();
        }

        public async Task<bool> IsUniqueEmail(string Email)
        {
            string url = $"{BaseUrl}/Users/GetIsUniqueEmail/{Email}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return new bool();
        }

        public async Task<bool> ChangePassword(string Email, string newpassword)
        {
            string url = $"{BaseUrl}/Users/UpdateUserPassword/{Email}";
            var json = JsonSerializer.Serialize(newpassword);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var User = await response.Content.ReadFromJsonAsync<User>();
                if (User is not null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<User> AddUser(UserDto dto)
        {
            string url = $"{BaseUrl}/Users/AddUser";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var User = await response.Content.ReadFromJsonAsync<User>();
                if (User is not null)
                {
                    return User;
                }
            }
            return new User();
        }

        public async Task<bool> UpdateUser(int id, UserDto dto)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/Users/UpdateUser/{id}";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var User = await response.Content.ReadFromJsonAsync<User>();
                if (User is not null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
