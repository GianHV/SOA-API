using Resource3.Models;

namespace Resource3.Services.Internal
{
    public interface IOrderReportService
    {
        int AddOrderReport(int orderId);
        void UpdateOrderReport(OrderReport orderReport);
        IEnumerable<OrderReport> GetOrderReports();
        OrderReport GetOrderReport(int id);
        void DeleteOrderReport(int id);
    }
}
