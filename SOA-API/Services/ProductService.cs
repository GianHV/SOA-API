using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SOA_API.Models;

namespace SOA_API.Services
{
    public class ProductService : IProductService
    {
        private readonly string _connectionString;
        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
        }
        public int AddProduct(Product product)
        {
            int newId = 0;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@name", product.Name);
                parameters.Add("@description", product.Description);
                parameters.Add("@price", product.Price);
                parameters.Add("@quantity", product.Quantity);
                parameters.Add("@id", dbType:System.Data.DbType.Int32, direction:System.Data.ParameterDirection.Output);
                
                var result = conn.Execute("sp_add_product", parameters, null, commandType: CommandType.StoredProcedure);
                newId = parameters.Get<int>("@id");
            }
            return newId;
        }

        public void DeleteProduct(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var result = conn.Execute("sp_delete_product", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditProduct(Product product)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", product.Id);
                parameters.Add("@name", product.Name);
                parameters.Add("@description", product.Description);
                parameters.Add("@price", product.Price);
                parameters.Add("@quantity", product.Quantity);

                var result = conn.Execute("sp_edit_product", parameters, null, commandType: CommandType.StoredProcedure);
            }
        }

        public Product GetProduct(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                var result = conn.Query<Product>("sp_get_product_by_id", parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var result = conn.Query<Product>("sp_get_products", null,commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
