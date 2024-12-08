using Dapper;
using System.Data.SqlClient;
using System.Data;
using Resource2.Models;
using Resource2.Enum;
using Resource2.Services.External;

namespace Resource2.Services.Internal
{
    public class OrderService : IOrderService
    {
        private readonly string _connectionString;
        public OrderService(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetSection("ConnectionStrings:DbConnectionString")
                .Get<string>() ?? string.Empty;
        }
        public int CreateOrder(OrderDTO order)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var itemsTable = new DataTable();
                itemsTable.Columns.Add("ProductId", typeof(int));
                itemsTable.Columns.Add("ProductName", typeof(string));
                itemsTable.Columns.Add("Quantity", typeof(int));
                itemsTable.Columns.Add("Price", typeof(decimal));

                foreach (var item in order.Items)
                {
                    itemsTable.Rows.Add(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
                }

                var parameters = new DynamicParameters();
                parameters.Add("@customerName", order.CustomerName);
                parameters.Add("@customerEmail", order.CustomerEmail);
                parameters.Add("@status", StatusOrder.Pending);
                parameters.Add("@Items", itemsTable.AsTableValuedParameter("OrderItemType"));
                parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = conn.Execute("sp_add_order", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        public void DeleteOrder(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_order", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public Order GetOrder(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.QueryMultiple("sp_get_order_by_id", parameters, commandType: CommandType.StoredProcedure);
                var order = result.ReadSingleOrDefault<Order>();
                order.Items = result.Read<OrderItem>().AsList();
                return order;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<Order>("sp_get_orders", null, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public void UpdateStatusOrder(Order order)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", order.Id);
                parameters.Add("@status", order.Status);

                conn.Execute("sp_update_status_order", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
