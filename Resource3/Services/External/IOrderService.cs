using Resource3.Models;

namespace Resource3.Services.External
{
    public interface IOrderService
    {
        Task<T> GetDetail<T>(int id, string token);
    }
}
