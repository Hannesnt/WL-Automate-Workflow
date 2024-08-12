using kyx_demo.Models;

namespace kyx_demo.Services
{
    public interface ISalesforceService
    {
        Task<OrderData> GetDataAsync(string caseNumber);

        Task<SalesforceToken> GetSalesforceTokenAsync();
    }
}
