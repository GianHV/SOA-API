using Resource3.Models;

namespace Resource3.Services.Internal
{
    public interface IProductReportService
    {
        int AddProductReport(ProductReport productReport);
        IEnumerable<ProductReport> GetProductReports();
        ProductReport GetProductReport(int id);
        void DeleteProductReport(int id);
    }
}
