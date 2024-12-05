using SOA_API.Models;

namespace SOA_API.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int id);
        int AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(int id);
    }
}
