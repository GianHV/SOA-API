using Resource2.Models;

namespace Resource2.Services.Internal
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetOrderItems();
        OrderItem GetOrderItem(int id);
        int AddOrderItems(OrderItem item);
        void UpdateOrderItem(OrderItem item);
        void DeleteOrderItem(int id);
    }
}
