namespace TechnicalServices.Services
{
    public class PaymentService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public PaymentService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }
        public PaymentService()
        {
            Client = new HttpClient();
            this.Token = "";
        }
        public async Task<PaymentType> GetPyment(int id)
        {
            if (Token == "")
                return new PaymentType();

            string url = $"{BaseUrl}/PaymentTypes/GetPaymentType/{id}";
            var responce = await Client.GetAsync(url);
            if (responce.IsSuccessStatusCode)
            {
                var res = await responce.Content.ReadFromJsonAsync<PaymentType>();
                if (res != null)
                {
                    return res;
                }
            }
            return new PaymentType();
        }

        public async Task<UserPayment> AddUserPayment(UserPaymentDto dto)
        {
            if (Token == "")
                return new UserPayment();

            string url = $"{BaseUrl}/UserPayments/AddUserPaymnet";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var payment = await response.Content.ReadFromJsonAsync<UserPayment>();

                if (payment is not null)
                {
                    return payment;
                }
            }
            return new UserPayment();
        }
    }
}
