using Common.Base;
using Resource3.Models;

namespace Resource3.Services.External
{
    public class OrderService : HttpService, IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string orderUrl;
        public OrderService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            orderUrl = configuration.GetValue<string>("ServiceUrls:OrderAPI");
        }

        public Task<T> GetDetail<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                Url = $"{orderUrl}/api/orders/{id}",
                Token = token
            });
        }
    }
}
