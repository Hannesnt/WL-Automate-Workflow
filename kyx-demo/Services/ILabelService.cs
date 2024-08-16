using kyx_demo.Models;

namespace kyx_demo.Services;

public interface ILabelService
{
    Task<ShipmentResponse> CreateLabelAsync(ShipmentRequest shipment);
}
