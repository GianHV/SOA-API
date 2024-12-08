using System.Data;
using System.Data.SqlClient;
using Dapper;
using Resource3.Models;

namespace Resource3.Services.Internal
{
    public class OrderReportService : IOrderReportService
    {
        private readonly string _connectionString;
        public OrderReportService(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetSection("ConnectionStrings:DbConnectionString")
                .Get<string>() ?? string.Empty;
        }
        public int AddOrderReport(int orderId)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var parameters = new DynamicParameters();
                parameters.Add("@orderId", orderId);

                parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = conn.Execute("sp_add_order_report", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        public void DeleteOrderReport(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_order_report", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public OrderReport GetOrderReport(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<OrderReport>("sp_get_order_report_by_id", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<OrderReport> GetOrderReports()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<OrderReport>("sp_get_order_reports", null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public void UpdateOrderReport(OrderReport orderReport)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", orderReport.Id);
                parameters.Add("@totalRevenue", orderReport.TotalRevenue);
                parameters.Add("@totalCost", orderReport.TotalCost);

                conn.Execute("sp_update_order_report", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
