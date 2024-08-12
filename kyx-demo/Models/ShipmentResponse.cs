namespace kyx_demo.Models;
using System.Text.Json.Serialization;

public class ShipmentResponse
{
    [JsonPropertyName("shipmentTrackingNumber")]
    public string ShipmentTrackingNumber { get; set; }

    [JsonPropertyName("trackingUrl")]
    public string TrackingUrl { get; set; }

    [JsonPropertyName("packages")]
    public List<Package> Packages { get; set; }

    [JsonPropertyName("documents")]
    public List<Document> Documents { get; set; }
}

public class ResponsePackage
{
    [JsonPropertyName("referenceNumber")]
    public int ReferenceNumber { get; set; }

    [JsonPropertyName("trackingNumber")]
    public string TrackingNumber { get; set; }

    [JsonPropertyName("trackingUrl")]
    public string TrackingUrl { get; set; }
}

public class Document
{
    [JsonPropertyName("imageFormat")]
    public string ImageFormat { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }
}
