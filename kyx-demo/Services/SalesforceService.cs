using kyx_demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace kyx_demo.Services
{
    public class SalesforceService : ISalesforceService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SalesforceService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<OrderData> GetDataAsync(string caseNumber)
        {
            var salesForceToken = await GetSalesforceTokenAsync();
            
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(salesForceToken.TokenType, salesForceToken.AccessToken);
            var requestUrl = "https://bambora.my.salesforce.com/services/data/v61.0/sobjects/Case";

            var response = await client.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();

            var caseList = JsonSerializer.Deserialize<Cases>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            OrderData orderData = new OrderData();
            if (caseList != null)
            {
                var locatedUserCase = caseList.RecentItems.FirstOrDefault(x => x.CaseNumber == caseNumber);

                if (locatedUserCase != null)
                {
                    client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(salesForceToken.TokenType, salesForceToken.AccessToken);

                    requestUrl = $"https://bambora.my.salesforce.com/services/data/v61.0/sobjects/Case/{locatedUserCase.Id}";

                    response = await client.GetAsync(requestUrl);
                    content = await response.Content.ReadAsStringAsync();

                    var userCaseData = JsonSerializer.Deserialize<Case>(content, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    var caseDescriptionAsList = userCaseData?.Description.Split('\n');
                    var (zipCode1, city1) = ExtractZipCodeAndCity(caseDescriptionAsList[16]);
                    var licenseList = caseDescriptionAsList[30].Split(": ");
                    licenseList = licenseList[1].Split(", ");
                    List<int> licenses = new List<int>();
                    foreach (var license in licenseList)
                    {
                        licenses.Add(int.Parse(license.Trim()));
                    }
                    orderData = new OrderData
                    {
                        Package = caseDescriptionAsList[1],
                        Country = caseDescriptionAsList[3],
                        CustomerNumber = caseDescriptionAsList[8],
                        CustomerName = caseDescriptionAsList[14],
                        Address = caseDescriptionAsList[15],
                        City = city1,
                        PostalCode = zipCode1.Replace(" ", ""),
                        PhoneNumber = caseDescriptionAsList[21],
                        Email = caseDescriptionAsList[23],
                        TerminalModel = caseDescriptionAsList[28],
                        LicenseNumber = licenses
                    };
                    
                }
            }
            return orderData;
        }
        public async Task<SalesforceToken> GetSalesforceTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "M01WRzlLSTJISEFxMzNSeldBR0VHWVdDQ1ZpZmU4Y0g4RTJZYWs5U0pBd3I1aWhMaU1SSWtFMnpNOGl0QVo5Qk5US0hfQ0ZzTDF2LlRtejZvTUg3WDpDM0ZDOUM4QTk3REU5NTY4MzY5OTI1NEM2Qjk5RkRGNjcxQTRBQUQ0QjQ3RjAyOUY2RDk5OUNCRDg4RTE4MThD");

            var formData = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials" }
            };

            var content = new FormUrlEncodedContent(formData);

            var response = await client.PostAsync("https://bambora.my.salesforce.com/services/oauth2/token", content);

            response.EnsureSuccessStatusCode();

            var tokenContent = await response.Content.ReadAsStringAsync();
            var tokenData = JsonSerializer.Deserialize<SalesforceToken>(tokenContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return tokenData;
        }

        public static (string ZipCode, string City) ExtractZipCodeAndCity(string input)
        {
            // Numbers followed by text
            var regex = new Regex(@"^(\d{4}|\d{5}|\d{3} \d{2})\s+(.+)$");


            var match = regex.Match(input);

            if (match.Success)
            {
                string zipCode = match.Groups[1].Value;
                string city = match.Groups[2].Value;

                return (zipCode, city);
            }


            return (null, null);
        }
    }
}
