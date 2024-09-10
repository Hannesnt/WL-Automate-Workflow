using kyx_demo.Models;
using System.Net.Http.Headers;

namespace kyx_demo.Services;

public class CCService : ICCService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CCService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage> SendDataAsync(CCDeliver deliverData, string terminalId)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "CC_BEARER");

        var response = await client.PostAsJsonAsync($"https://api.instore.bambora.com/api/terminals/{terminalId}/deliver", deliverData);
        return response;
    }
}
