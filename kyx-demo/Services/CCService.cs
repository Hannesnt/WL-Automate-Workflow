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
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNjLWxvZ2lzdGljcyIsIlVzZXIiOiJoYW5uZXMubmlsc3NvbnRlbmduYXNAd29ybGRsaW5lLmNvbSIsIkFjY291bnRJZCI6MTY5LCJBY2NvdW50IjoiY2MtbG9naXN0aWNzIiwibmJmIjoxNzIzMTg5NTgyLCJleHAiOjE3MjMyMjU1ODIsImlhdCI6MTcyMzE4OTU4MiwiaXNzIjoiSW5zdG9yZUFQSSIsImF1ZCI6Ikluc3RvcmVBdWRpZW5jZSJ9.bKrigfE7RVI0O_t9pPP0GvmzNfCjsyAZcgJ_nrzpNrWtCkGbjsZBpeVbDeW0ibW8PlgoMBOTl0CYjhnVxfZmA4bTQop1d-jznSjK7VYx47hcX3xXkl12BhITSY-nPiDeBgkxhXslWhxEMDrqmdZg8200lNHi9ba-cv3-c2Gc-5Tsuhla4rN4rgBVqNYs31XCShvKNQSYAjRaU0p4ye4xX9tvslQoELLxKMqjBzt4U_KzRgAaQcskG792H4NCcoQwF1b_6Xs8y_j59Yv1KpFcjmkXy2t56kEv4fl5MzZeoRT6TZDYh4SWHstNufcFBoNZkZy1fMFrdRNpj5xM3slX3g");

        var response = await client.PostAsJsonAsync($"https://api.instore.bambora.com/api/terminals/{terminalId}/deliver", deliverData);
        return response;
    }
}
