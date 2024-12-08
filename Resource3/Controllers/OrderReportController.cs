using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Resource3.Models;
using Resource3.Services.External;
using Resource3.Services.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Resource3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderReportController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IOrderReportService _orderReportService;
        private readonly IProductReportService _productReportService;
        public OrderReportController(
            IOrderService orderService,
            IOrderReportService orderReportService,
            IProductService productService,
            IProductReportService productReportService)
        {
            _orderService = orderService;
            _orderReportService = orderReportService;
            _productService = productService;
            _productReportService = productReportService;
        }
        [HttpGet]
        public IActionResult Get() {
            var data = _orderReportService.GetOrderReports();
            return APIResponse(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _orderReportService.GetOrderReport(id);
            return APIResponse(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] int orderId)
        {
            decimal totalRevenue = 0;
            decimal totalCost = 0;

            // 1. get detail order with orderId
            var productDetail = getDetail(orderId).Result.Items;
            // 2. get distinct product id
            var productIds = productDetail
                                    .Select(x => x.ProductId)
                                    .Distinct()
                                    .ToList();

            // 3. get product with id
            var products = await GetProducts(productIds);

            // 4. create a order report
            var orderReportId = _orderReportService.AddOrderReport(orderId);

            // 5. add product reports
            foreach (var productId in productIds) {
                var totalSold = productDetail
                                    .Where(x => x.ProductId == productId)
                                    ?.Sum(x => x.Quantity) ?? 0;

                var revenue = productDetail
                                    .Where(x => x.ProductId == productId)
                                    ?.Sum(x => x.TotalPrice) ?? 0;
                totalRevenue += revenue;

                var cost = products.
                    Where(x => x.Id == productId).FirstOrDefault()
                    ?.Price * totalSold ?? 0;
                totalCost += cost;

                var data = new ProductReport()
                {
                    OrderReportId = orderReportId,
                    ProductId = productId,
                    TotalSold = totalSold,
                    Revenue = revenue,
                    Cost = cost,
                    profit = revenue - cost,
                };

                _productReportService.AddProductReport(data);
            }

            // 7. update order report again
            var orderReport = new OrderReport()
            {
                Id = orderReportId,
                TotalRevenue = totalRevenue,
                TotalCost = totalCost,
            };
            _orderReportService.UpdateOrderReport(orderReport);

            return APIResponse(string.Empty);
        }

        private async Task<List<Product>> GetProducts(List<int> ids)
        {
            List<Product> products = new List<Product>();
            var token = HttpContext.Items["BearerToken"]?.ToString();
            foreach (var id in ids)
            {
                var response = await _productService.GetDetail<ApiResponse>(id, token);
                if (response != null && response.IsSuccess)
                {
                    var data = JsonConvert.DeserializeObject<Product>(Convert.ToString(response.Result));
                    products.Add(data);
                }
            }
            return products;
        }

        private async Task<Order> getDetail(int orderId)
        {
            Order data = null;
            var token = HttpContext.Items["BearerToken"]?.ToString();
            var response = await _orderService.GetDetail<ApiResponse>(orderId, token);
            if (response != null && response.IsSuccess)
            {
                data = JsonConvert.DeserializeObject<Order>(Convert.ToString(response.Result));
            }
            return data; 
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderReportService.DeleteOrderReport(id);
            return APIResponse(string.Empty);
        }
    }
}
