using System.Net.Http;
using Common.Base;
using Newtonsoft.Json.Linq;
using Resource2.Models;

namespace Resource2.Services.External
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
        public Task<T> GetProductById<T>(int productId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                Url = productUrl + $"/api/product/{productId}",
                Token = token
            });
        }

        public Task<T> UpdateProduct<T>(ProductDTO product, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = product,
                Url = productUrl + $"/api/product",
                Token = token,
            });
        }
    }
}
