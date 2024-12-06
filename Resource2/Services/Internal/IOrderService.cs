using Resource2.Models;

namespace Resource2.Services.Internal
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(int id);
        int CreateOrder(OrderDTO order);
        void UpdateStatusOrder(Order order);
        void DeleteOrder(int id);
    }
}
