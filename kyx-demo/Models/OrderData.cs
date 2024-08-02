namespace kyx_demo.Models;

public class OrderData
{
    public string Package { get; set; }

    public string Country { get; set; }

    public string CustomerNumber { get; set; }

    public string CustomerName { get; set; }

    public string Address { get; set; }

    public string City { get; set; }
    public string PostalCode { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public List<string> LicenseNumber { get; set; }

    public string TerminalModel { get; set; }

}
