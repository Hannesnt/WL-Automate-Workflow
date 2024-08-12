using kyx_demo.Models;

namespace kyx_demo.Services
{
    public interface IPdfBodyService
    {
       Task<ShipmentRequest> CreatePdfBody(OrderData orderData);
    }
}
