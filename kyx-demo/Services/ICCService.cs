using kyx_demo.Models;

namespace kyx_demo.Services;

public interface ICCService
{
    Task<HttpResponseMessage> SendDataAsync(CCDeliver data, string terminalId);
}
