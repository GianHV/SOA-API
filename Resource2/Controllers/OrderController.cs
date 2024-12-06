using System.Text.Json;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Resource2.Models;
using Resource2.Services.External;
using Resource2.Services.Internal;

namespace Resource2.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        [HttpGet]
        public IActionResult Gets()
        {
            var data = _orderService.GetOrders();
            return APIResponse(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _orderService.GetOrder(id);
            return APIResponse(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderDTO request)
        {
            var products = request
                            .Items
                            .AsEnumerable()
                            .ToDictionary(i => i.ProductId, i => i.Quantity);
            if (!await IsStockAvailable(products))
            {
                var response = new ApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessages = new List<string>() { "Product is not available" }
                };
                return StatusCode((int)response.StatusCode, response);
            }

            await ReduceQuantityProduct(products);
            var data = _orderService.CreateOrder(request);
            return APIResponse(data);
        }

        [HttpPut]
        public IActionResult Put([FromBody] OrderPutDTO request)
        {
            Order order = new Order() { Id = request.Id, Status = request.Status};
            _orderService.UpdateStatusOrder(order);
            return APIResponse(string.Empty);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return APIResponse(string.Empty);
        }

        private async Task ReduceQuantityProduct(Dictionary<int, int> products)
        {
            var token = HttpContext.Items["BearerToken"]?.ToString();
            foreach (var product in products) {
                var tmp = new ProductDTO()
                {
                    Id = product.Key,
                    Name = null,
                    Description = null,
                    Price = null,
                    Quantity = product.Value*-1,
                };
                var response = await _productService.UpdateProduct<ApiResponse>(tmp, token);
            }
        }

        private async Task<bool> IsStockAvailable(Dictionary<int,int> products)
        {
            var token = HttpContext.Items["BearerToken"]?.ToString();
            foreach (var product in products) {
                var response = await _productService.GetProductById<ApiResponse>(product.Key, token);
                if (response != null && response.IsSuccess)
                {
                    ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                    if (model.Quantity < product.Value) return false;
                }
            }
            return true;
        }
    }
}
