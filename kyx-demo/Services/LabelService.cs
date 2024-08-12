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
    public async Task<ShipmentResponse> CreateLabel(ShipmentRequest shipment)
    {
        using (var client = new HttpClient())
        {
            // Set headers
            //client.DefaultRequestHeaders.Add("Authorization", "Basic YXBIMnZaN3RLN2NXMmQ6REAzelQhMnNHQDBmT0A3Yw==");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YXBIMnZaN3RLN2NXMmQ6REAzelQhMnNHQDBmT0A3Yw==");

            // Define your request body
            var requestBody = new
            {
                plannedShippingDateAndTime = "2024-08-12T16:00:31GMT+01:00",
                pickup = new { isRequested = false },
                productCode = "B",
                accounts = new[]
                {
                    new { typeCode = "shipper", number = "201270389" }
                },
                outputImageProperties = new
                {
                    encodingFormat = "pdf",
                    imageOptions = new ImageOption[]
                    {
                        new ImageOption { typeCode = "label", templateName = "ECOM26_84_A4_001", isRequested = true },
                        new ImageOption { typeCode = "waybillDoc", templateName = "ARCH_8X4_A4_002", isRequested = true },
                        new ImageOption { typeCode = "invoice", templateName = "COMMERCIAL_INVOICE_P_10", isRequested = true }
                    },
                    splitTransportAndWaybillDocLabels = true
                },
                customerDetails = new
                {
                    shipperDetails = new
                    {
                        postalAddress = new
                        {
                            postalCode = "30232",
                            cityName = "Halmstad",
                            countryCode = "SE",
                            addressLine1 = "address1",
                            addressLine2 = "addres2",
                            addressLine3 = "addres3"
                        },
                        contactInformation = new
                        {
                            email = "test@ddhl.com",
                            phone = "+1123456789",
                            mobilePhone = "239458585858",
                            companyName = "Worldline",
                            fullName = "Hannes Testarlite"
                        }
                    },
                    receiverDetails = new
                    {
                        postalAddress = new
                        {
                            postalCode = shipment.customerDetails.receiverDetails.postalAddress.postalCode,
                            cityName = shipment.customerDetails.receiverDetails.postalAddress.cityName,
                            countryCode = "SE",
                            addressLine1 = shipment.customerDetails.receiverDetails.postalAddress.addressLine1,
                            addressLine2 = shipment.customerDetails.receiverDetails.postalAddress.addressLine2,
                            addressLine3 = shipment.customerDetails.receiverDetails.postalAddress.addressLine3

                        },
                        contactInformation = new
                        {
                            email = shipment.customerDetails.receiverDetails.contactInformation.email,
                            phone = shipment.customerDetails.receiverDetails.contactInformation.phone,
                            mobilePhone = shipment.customerDetails.receiverDetails.contactInformation.phone,
                            companyName = shipment.customerDetails.receiverDetails.contactInformation.companyName,
                            fullName = "..",
                        }
                    }
                    //receiverDetails = new
                    //{
                    //    postalAddress = new
                    //    {
                    //        postalCode = "56435",
                    //        cityName = "Bankeryd",
                    //        countryCode = "SE",
                    //        addressLine1 = "Domsandsvägen 8",
                    //        addressLine2 = "addres2",
                    //        addressLine3 = "addres3"
                    //    },
                    //    contactInformation = new
                    //    {
                    //        email = "viadukten.bil@hotmail.se",
                    //        phone = "+46700386848",
                    //        mobilePhone = "239458585858",
                    //        companyName = "Bankeryd Bilvård Handelsbolag",
                    //        fullName = ".."
                    //    }
                    //}
                },
                content = new
                {
                    packages = new[]
                    {
                        new { weight = 22.5, dimensions = new { length = 15, width = 15, height = 40 } },
                        new { weight = 22.5, dimensions = new { length = 15, width = 15, height = 40 } }
                    },
                    isCustomsDeclarable = true,
                    declaredValue = 1500,
                    declaredValueCurrency = "SEK",
                    exportDeclaration = new
                    {
                        lineItems = new[]
                        {
                            new
                            {
                                number = 1,
                                description = "line item description 1",
                                price = 500,
                                quantity = new { value = 1, unitOfMeasurement = "BOX" },
                                commodityCodes = new[]
                                {
                                    new { typeCode = "inbound", value = "123456" },
                                    new { typeCode = "outbound", value = "123456" }
                                },
                                exportReasonType = "permanent",
                                manufacturerCountry = "SG",
                                weight = new { netValue = 10, grossValue = 10 }
                            },
                            new
                            {
                                number = 2,
                                description = "line item description 2",
                                price = 1000,
                                quantity = new { value = 1, unitOfMeasurement = "BOX" },
                                commodityCodes = new[]
                                {
                                    new { typeCode = "inbound", value = "123456" },
                                    new { typeCode = "outbound", value = "123456" }
                                },
                                exportReasonType = "permanent",
                                manufacturerCountry = "SG",
                                weight = new { netValue = 10, grossValue = 10 }
                            }
                        },
                        invoice = new
                        {
                            number = "12345-ABC",
                            date = "2024-08-08",
                            signatureName = "Brewer",
                            signatureTitle = "Mr."
                        },
                        additionalCharges = new[]
                        {
                            new { value = 10.0, typeCode = "freight", caption = "freight charge" },
                            new { value = 50.0, typeCode = "insurance", caption = "insurance charge" }
                        },
                        placeOfIncoterm = "Sweden",
                        exportReason = "Sales",
                        exportReasonType = "permanent",
                        shipmentType = "commercial"
                    },
                    description = "shipment description",
                    incoterm = "FCA",
                    unitOfMeasurement = "metric"
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

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
