namespace Resource3.Services.External
{
    public interface IProductService
    {
        Task<T> GetDetail<T>(int id, string token);
    }
}
