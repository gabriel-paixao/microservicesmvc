using Store.Web.Models;
using Store.Web.Services.IServices;

namespace Store.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.ApiType.POST,
                Data = product,
                Url = Constants.ProductApiBase + "api/products",
                Token = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.ApiType.DELETE,
                Url = Constants.ProductApiBase + $"api/products/{id}",
                Token = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.ApiType.GET,
                Url = Constants.ProductApiBase + $"api/products",
                Token = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.ApiType.GET,
                Url = Constants.ProductApiBase + $"api/products/{id}",
                Token = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = Constants.ApiType.PUT,
                Data = product,
                Url = Constants.ProductApiBase + "api/products",
                Token = ""
            });
        }
    }
}
