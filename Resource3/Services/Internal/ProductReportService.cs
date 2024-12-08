using Dapper;
using System.Data.SqlClient;
using System.Data;
using Resource3.Models;

namespace Resource3.Services.Internal
{
    public class ProductReportService : IProductReportService
    {
        private readonly string _connectionString;
        public ProductReportService(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetSection("ConnectionStrings:DbConnectionString")
                .Get<string>() ?? string.Empty;
        }
        public int AddProductReport(ProductReport productReport)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@orderReportId", productReport.OrderReportId);
                parameters.Add("@productId", productReport.ProductId);
                parameters.Add("@totalSold", productReport.TotalSold);
                parameters.Add("@revenue", productReport.Revenue);
                parameters.Add("@cost", productReport.Cost);

                parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = conn.Execute("sp_add_product_report", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        public void DeleteProductReport(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_product_report", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public ProductReport GetProductReport(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query< ProductReport>("sp_get_product_report_by_id", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<ProductReport> GetProductReports()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<ProductReport>("sp_get_product_reports", null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
