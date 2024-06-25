namespace TechnicalServices.Services
{
    public class NotificationService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public NotificationService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }
        public NotificationService()
        {
            Client = new HttpClient();
            Token = "";
        }
        //-----------------------Notifcations------------------------//
        public async Task<List<UserNotifcation>> GetUserNotifcation(int id)
        {
            if (Token == "")
                return new List<UserNotifcation>();

            string url = $"{BaseUrl}/UserNotifcations/GetUserNotifcation/{id}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Notifications = await response.Content.ReadFromJsonAsync<List<UserNotifcation>>();
                if (Notifications is not null)
                {
                    return Notifications;
                }
            }
            return new List<UserNotifcation>();
        }

        public async Task<bool> AddNotification(UserNotifcationDto dto)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/UserNotifcations/AddUserNotifcation";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var UserPayment = await response.Content.ReadFromJsonAsync<UserNotifcation>();
                if (UserPayment is not null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> MarkNotificatonAsRead(int id)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/UserNotifcations/MarkAsRead";
            var json = JsonSerializer.Serialize(id);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<UserNotifcation>();
                if (res != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> AddNewOrderNotifcations(int OrderExtendServiceId)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/UserNotifcations/AddNewOrderNotifcations";
            var json = JsonSerializer.Serialize(OrderExtendServiceId);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var Notification = await response.Content.ReadFromJsonAsync<List<UserNotifcation>>();
                if (Notification is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
