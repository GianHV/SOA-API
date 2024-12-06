using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource2.Models;
using Resource2.Services.Internal;

namespace Resource2.Controllers
{
    [Route("api/order_items")]
    [Authorize]
    [ApiController]
    public class OrderItemController : ApiControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public IActionResult Gets()
        {
            var data = _orderItemService.GetOrderItems();
            return APIResponse(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _orderItemService.GetOrderItem(id);
            return APIResponse(data);
        }
        [HttpPost]
        public IActionResult Add(OrderItemDTO request)
        {
            var orderItem = new OrderItem()
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
            };
            var data = _orderItemService.AddOrderItems(orderItem);
            return APIResponse(data);
        }
        [HttpPut]
        public IActionResult Put([FromBody] OrderItem request)
        {
            _orderItemService.UpdateOrderItem(request);
            return APIResponse(string.Empty);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderItemService.DeleteOrderItem(id);
            return APIResponse(string.Empty);
        }
    }
}
