using kyx_demo.Models;
using System.Threading.Tasks;

namespace kyx_demo.Services
{
    public class PdfBodyService : IPdfBodyService
    {
        public async Task<ShipmentRequest> CreatePdfBodyAsync(OrderData orderData)
        {
            // Call the private method to get receiver details synchronously
            var receiverDetails = AddReceiverDetails(orderData);



            var shipment = new ShipmentRequest
            {
                plannedShippingDateAndTime = "2024-08-12T16:00:31GMT+01:00",
                pickup = new Pickup { isRequested = false },
                productCode = "B",
                accounts = new List<Account>
            {
                new Account { typeCode = "shipper", number = "201270389" }
            },
                outputImageProperties = new OutputImageProperties
                {
                    encodingFormat = "pdf",
                    imageOptions = new List<ImageOption>
                    {
                        new ImageOption { typeCode = "label", templateName = "ECOM26_84_A4_001",  isRequested = true  },
                        new ImageOption { typeCode = "waybillDoc", templateName = "ARCH_8X4_A4_002", isRequested = true },
                        new ImageOption { typeCode = "invoice", templateName = "COMMERCIAL_INVOICE_P_10", isRequested = true }
                    },
                    splitTransportAndWaybillDocLabels = true
                },
                customerDetails = new CustomerDetails
                {
                    shipperDetails = new ShipperDetails
                    {
                        postalAddress = new PostalAddress
                        {
                            postalCode = "30232",
                            cityName = "Halmstad",
                            countryCode = "SE",
                            addressLine1 = "Slottsjordsvägen 5",
                            addressLine2 = "addres2",
                            addressLine3 = "addres3"
                        },
                        contactInformation = new ContactInformation
                        {
                            email = "test@ddhl.com",
                            phone = "+1123456789",
                            mobilePhone = "239458585858",
                            companyName = "Worldline",
                            fullName = "Hannes Testarlite"
                        }
                    },
                    receiverDetails = receiverDetails
                },
                content = new Content
                {
                    packages = new List<Package>
                    {
                        new Package
                        {
                            weight = 22.5,
                            dimensions = new Dimensions { length = 15, width = 15, height = 40 }
                        },
                        new Package
                        {
                            weight = 22.5,
                            dimensions = new Dimensions { length = 15, width = 15, height = 40 }
                        }
                    },
                    isCustomsDeclarable = true,
                    declaredValue = 1500,
                    declaredValueCurrency = "SEK",
                    exportDeclaration = new ExportDeclaration
                    {
                        lineItems = new List<LineItem>
                        {
                            new LineItem
                            {
                                number = 1,
                                description = "line item description 1",
                                price = 500,
                                quantity = new Quantity { value = 1, unitOfMeasurement = "BOX" },
                                commodityCodes = new List<CommodityCode>
                                {
                                    new CommodityCode { typeCode = "inbound", value = "123456" },
                                    new CommodityCode { typeCode = "outbound", value = "123456" }
                                },
                                exportReasonType = "permanent",
                                manufacturerCountry = "SG",
                                weight = new Weight { netValue = 10, grossValue = 10 }
                            },
                            new LineItem
                            {
                                number = 2,
                                description = "line item description 2",
                                price = 1000,
                                quantity = new Quantity { value = 1, unitOfMeasurement = "BOX" },
                                commodityCodes = new List<CommodityCode>
                                {
                                    new CommodityCode { typeCode = "inbound", value = "123456" },
                                    new CommodityCode { typeCode = "outbound", value = "123456" }
                                },
                                exportReasonType = "permanent",
                                manufacturerCountry = "SG",
                                weight = new Weight { netValue = 10, grossValue = 10 }
                            }
                        },
                        invoice = new Invoice
                        {
                            number = "12345-ABC",
                            date = "2024-08-08",
                            signatureName = "Brewer",
                            signatureTitle = "Mr."
                        },
                        additionalCharges = new List<AdditionalCharge>
                        {
                            new AdditionalCharge { value = 10, typeCode = "freight", caption = "freight charge" },
                            new AdditionalCharge { value = 50, typeCode = "insurance", caption = "insurance charge" }
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

            return shipment;
        }




        private ReceiverDetails AddReceiverDetails(OrderData orderData)
        {
            var receiverDetails = new ReceiverDetails
            {
                
                postalAddress = new PostalAddress
                {
                    addressLine1 = orderData.Address,
                    cityName = orderData.City,
                    countryCode = "SE",
                    postalCode = orderData.PostalCode,
                    addressLine2 = "addres2",
                    addressLine3 = "addres3"
                    //addressLine1 = "Slottsjordsvägen 3",
                    //cityName = "Göteborg",
                    //countryCode = "SE",
                    //postalCode = "30232",
                    //addressLine2 = "",
                    //addressLine3 = ""
                },
                contactInformation = new ContactInformation
                {
                    companyName = orderData.CustomerName,
                    email = orderData.Email,
                    fullName = "..",
                    mobilePhone = "orderData.PhoneNumber",
                    phone = orderData.PhoneNumber
                    //companyName = "Västergök",
                    //email = "test@ddhl.com",
                    //fullName = "Ludwig Bergendahl",
                    //mobilePhone = "239458585858",
                    //phone = "+1123456789"
                }
            };

            return receiverDetails;
        }
    }

}