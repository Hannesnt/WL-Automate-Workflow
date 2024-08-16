using kyx_demo.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace kyx_demo.Services;

public class LabelService : ILabelService
{
    //private readonly IHttpClientFactory _httpClientFactory;
    //public LabelService(IHttpClientFactory httpClientFactory)
    //{
    //    _httpClientFactory = httpClientFactory;
    //}
    public async Task<ShipmentResponse> CreateLabelAsync(ShipmentRequest shipment)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YXBIMnZaN3RLN2NXMmQ6REAzelQhMnNHQDBmT0A3Yw==");


            var json = JsonSerializer.Serialize(shipment);

            var response = await client.PostAsync(
                "https://express.api.dhl.com/mydhlapi/test/shipments",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var shipmentResponse = JsonSerializer.Deserialize<ShipmentResponse>(responseBody);
                Console.WriteLine("Shipment created successfully!");
                return shipmentResponse;
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine(responseBody);
                return null;
            }
            
        }
    
}


}
