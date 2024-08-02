using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using kyx_demo.Models;
using System.Text.RegularExpressions;

namespace kyx_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesforceController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SalesforceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "GetSalesforceCase")]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "00D610000007qyq!AQEAQKv6ceGlyYqXQ9jVPp2wb9sOgjkS1Au_Eb7HgUCweeowjWngbul71ywnxIIY0n7jiIMn0npPAHITBgOufLGEN4aBYMdB");
            var requestUrl = "https://bambora.my.salesforce.com/services/data/v61.0/sobjects/Case";

            var response = await client.GetAsync(requestUrl);

            //response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();

            var caseList = JsonSerializer.Deserialize<Cases>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (caseList != null)
            {
                var locatedUserCase = caseList.RecentItems.FirstOrDefault(x => x.CaseNumber == "02655042");

                if (locatedUserCase != null)
                {
                    client = _httpClientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "00D610000007qyq!AQEAQKv6ceGlyYqXQ9jVPp2wb9sOgjkS1Au_Eb7HgUCweeowjWngbul71ywnxIIY0n7jiIMn0npPAHITBgOufLGEN4aBYMdB");

                    requestUrl = $"https://bambora.my.salesforce.com/services/data/v61.0/sobjects/Case/{locatedUserCase.Id}";

                    response = await client.GetAsync(requestUrl);
                    content = await response.Content.ReadAsStringAsync();

                    var userCaseData = JsonSerializer.Deserialize<Case>(content, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    var caseDescriptionAsList = userCaseData?.Description.Split('\n');
                    var (zipCode1, city1) = ExtractZipCodeAndCity(caseDescriptionAsList[16]);
                    OrderData orderData = new OrderData
                    {
                        Package = caseDescriptionAsList[1],
                        Country = caseDescriptionAsList[3],
                        CustomerNumber = caseDescriptionAsList[8],
                        CustomerName = caseDescriptionAsList[14],
                        Address = caseDescriptionAsList[15],
                        City = city1,
                        PostalCode = zipCode1,
                        PhoneNumber = caseDescriptionAsList[21],
                        Email = caseDescriptionAsList[23],
                        //LicenseNumber = 


                    };
                    var test = "string";
                }
                
            }

            



            return Ok(content);
        }
        public static (string ZipCode, string City) ExtractZipCodeAndCity(string input)
        {
            // Regular expression to match zip code (numbers) followed by city (text)
            var regex = new Regex(@"^(\d{3} ?\d{2})\s+(.+)$");

            var match = regex.Match(input);

            if (match.Success)
            {
                string zipCode = match.Groups[1].Value;
                string city = match.Groups[2].Value;

                return (zipCode, city);
            }

            // If the input string does not match the expected format, return null values
            return (null, null);
        }
    }
}
