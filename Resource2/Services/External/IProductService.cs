using Common.Base;
using Resource2.Models;

namespace Resource2.Services.External
{
    public interface IProductService
    {
        Task<T> GetProductById<T>(int productId, string token);
        Task<T> UpdateProduct<T>(ProductDTO product, string token);
    }
}
