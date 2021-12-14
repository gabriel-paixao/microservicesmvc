using static Store.Web.Constants;

namespace Store.Web.Models;
public class ApiRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string Token { get; set; }
}
