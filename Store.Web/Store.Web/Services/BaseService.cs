using Newtonsoft.Json;
using Store.Web.Models;
using Store.Web.Services.IServices;
using System.Text;

namespace Store.Web.Services;
public class BaseService : IBaseService
{
    protected readonly IHttpClientFactory _clientFactory;

    public BaseService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var client = _clientFactory.CreateClient("StoreApi");
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();
            if (request.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = null;

            switch (request.ApiType)
            {

                case Constants.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case Constants.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case Constants.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
            return apiResponseDto;

        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string>() { e.Message },
                IsSuccess = false
            };

            var res = JsonConvert.SerializeObject(dto);
            var apiResponse = JsonConvert.DeserializeObject<T>(res);
            return apiResponse;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
}
