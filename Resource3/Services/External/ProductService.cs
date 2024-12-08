
using Common.Base;

namespace Resource3.Services.External
{
    public class ProductService : HttpService,IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string productUrl;
        public ProductService(
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
            : base(clientFactory)
        {
            _clientFactory = clientFactory;
            productUrl = configuration.GetValue<string>("ServiceUrls:ProductAPI");
        }
        public Task<T> GetDetail<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                Url = productUrl + $"/api/products/{id}",
                Token = token
            });
        }
    }
}
