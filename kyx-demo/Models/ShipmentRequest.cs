namespace kyx_demo.Models;

using System;
using System.Collections.Generic;

public class ShipmentRequest
{
    public string plannedShippingDateAndTime { get; set; }
    public Pickup pickup { get; set; }
    public string productCode { get; set; }
    public List<Account> accounts { get; set; }
    public OutputImageProperties outputImageProperties { get; set; }
    public CustomerDetails customerDetails { get; set; }
    public Content content { get; set; }
}

public class Pickup
{
    public bool isRequested { get; set; }
}

public class Account
{
    public string typeCode { get; set; }
    public string number { get; set; }
}

public class OutputImageProperties
{
    public string encodingFormat { get; set; }
    public List<ImageOption> imageOptions { get; set; }
    public bool splitTransportAndWaybillDocLabels { get; set; }
}

public class ImageOption
{
    public string typeCode { get; set; }
    public string templateName { get; set; }
    public bool isRequested { get; set; }
}

public class CustomerDetails
{
    public ShipperDetails shipperDetails { get; set; }
    public ReceiverDetails receiverDetails { get; set; }
}

public class ShipperDetails
{
    public PostalAddress postalAddress { get; set; }
    public ContactInformation contactInformation { get; set; }
}

public class ReceiverDetails
{
    public PostalAddress postalAddress { get; set; }
    public ContactInformation contactInformation { get; set; }
}

public class PostalAddress
{
    public string postalCode { get; set; }
    public string cityName { get; set; }
    public string countryCode { get; set; }
    public string addressLine1 { get; set; }
    public string addressLine2 { get; set; }
    public string addressLine3 { get; set; }
}

public class ContactInformation
{
    public string email { get; set; }
    public string phone { get; set; }
    public string mobilePhone { get; set; }
    public string companyName { get; set; }
    public string fullName { get; set; }
}

public class Content
{
    public List<Package> packages { get; set; }
    public bool isCustomsDeclarable { get; set; }
    public double declaredValue { get; set; }
    public string declaredValueCurrency { get; set; }
    public ExportDeclaration exportDeclaration { get; set; }
    public string description { get; set; }
    public string incoterm { get; set; }
    public string unitOfMeasurement { get; set; }
}

public class Package
{
    public double weight { get; set; }
    public Dimensions dimensions { get; set; }
}

public class Dimensions
{
    public int length { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ExportDeclaration
{
    public List<LineItem> lineItems { get; set; }
    public Invoice invoice { get; set; }
    public List<AdditionalCharge> additionalCharges { get; set; }
    public string placeOfIncoterm { get; set; }
    public string exportReason { get; set; }
    public string exportReasonType { get; set; }
    public string shipmentType { get; set; }
}

public class LineItem
{
    public int number { get; set; }
    public string description { get; set; }
    public double price { get; set; }
    public Quantity quantity { get; set; }
    public List<CommodityCode> commodityCodes { get; set; }
    public string exportReasonType { get; set; }
    public string manufacturerCountry { get; set; }
    public Weight weight { get; set; }
}

public class Quantity
{
    public int value { get; set; }
    public string unitOfMeasurement { get; set; }
}

public class CommodityCode
{
    public string typeCode { get; set; }
    public string value { get; set; }
}

public class Weight
{
    public double netValue { get; set; }
    public double grossValue { get; set; }
}

public class Invoice
{
    public string number { get; set; }
    public string date { get; set; }
    public string signatureName { get; set; }
    public string signatureTitle { get; set; }
}

public class AdditionalCharge
{
    public double value { get; set; }
    public string typeCode { get; set; }
    public string caption { get; set; }
}
