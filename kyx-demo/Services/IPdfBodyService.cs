using kyx_demo.Models;

namespace kyx_demo.Services
{
    public interface IPdfBodyService
    {
       Task<ShipmentRequest> CreatePdfBodyAsync(OrderData orderData);
    }
}
