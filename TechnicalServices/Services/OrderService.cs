namespace TechnicalServices.Services
{
    public class OrderService
    {
        HttpClient Client;
        string BaseUrl = "http://technicalservices.somee.com/api";
        public string Token { get; set; }
        public OrderService(string Token)
        {
            Client = new HttpClient();
            this.Token = Token;
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Token);
        }

        public OrderService()
        {
            Client = new HttpClient();
            Token = "";
        }

        public async Task<Order> AddNewOrder(OrderDto dto)
        {
            if (Token == "")
                return new Order();

            string url = $"{BaseUrl}/Orders/AddOrder";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<Order>();
                if (order is not null)
                {
                    // await AddNewOrderNotifcations(order.extendServiceId);
                    return order;
                }
            }
            return new Order();
        }

        public async Task<bool> AddOrderResponse(ResponseDto dto)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/Responses/AddResponse";
            var json = JsonSerializer.Serialize(dto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<Response>();
                if (order is not null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ChangeOrderStatus(int orderId, int StatusId)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/Orders/UpdateOrderStatus/{orderId}";
            var json = JsonSerializer.Serialize(StatusId);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<Order>();
                if (order is not null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Order>> GetTechnicianNewOrders(int TechId)
        {
            if (Token == "")
                return new List<Order>();

            string url = $"{BaseUrl}/Orders/GetTechNewOrders/{TechId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<Order>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<Order>();
        }

        public async Task<List<Order>> GetTechnicianOrders(int TechId)
        {
            if (Token == "")
                return new List<Order>();

            string url = $"{BaseUrl}/Orders/GetTechOrders/{TechId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<Order>>();
                if (res.Count != 0)
                {
                    return res;
                }
            }
            return new List<Order>();
        }

        public async Task<List<Response>> GetOrderResponses(int orderId)
        {
            if (Token == "")
                return new List<Response>();

            string url = $"{BaseUrl}/Responses/GetAllResponses/{orderId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<List<Response>>();
            }
            return new List<Response>();
        }

        public async Task<bool> ChooseOrderResponse(int orderid, int Responseid)
        {
            if (Token == "")
                return false;

            string url = $"{BaseUrl}/Orders/ChooseResponse/{orderid}";
            var json = JsonSerializer.Serialize(Responseid);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<Order>();
                if (order is not null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Order>> GetUserOrders(int UserId)
        {
            if (Token == "")
                return new List<Order>();

            string url = $"{BaseUrl}/Orders/GetUserOrders/{UserId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var Orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                if (Orders is not null)
                {
                    return Orders;
                }
            }
            return new List<Order>();
        }
    }
}
