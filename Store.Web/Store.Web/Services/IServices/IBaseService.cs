using Store.Web.Models;

namespace Store.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
