using System.Data.SqlClient;
using System.Data;
using Resource2.Models;
using Dapper;

namespace Resource2.Services.Internal
{
    public class OrderItemService : IOrderItemService
    {
        private readonly string _connectionString;
        public OrderItemService(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetSection("ConnectionStrings:DbConnectionString")
                .Get<string>() ?? string.Empty;
        }
        public int AddOrderItems(OrderItem item)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@orderId", item.OrderId);
                parameters.Add("@productId", item.ProductId);
                parameters.Add("@productName", item.ProductName);
                parameters.Add("@quantity", item.Quantity);
                parameters.Add("@unitPrice", item.UnitPrice);
                parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = conn.Execute("sp_add_order_item", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        public void DeleteOrderItem(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_order_item", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public OrderItem GetOrderItem(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<OrderItem>("sp_get_order_item_by_id", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<OrderItem>("sp_get_order_items", null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public void UpdateOrderItem(OrderItem item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", item.Id);
                parameters.Add("@productId", item.ProductId);
                parameters.Add("@productName", item.ProductName);
                parameters.Add("@quantity", item.Quantity);
                parameters.Add("@unitPrice", item.UnitPrice);

                conn.Execute("sp_update_order_item", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
