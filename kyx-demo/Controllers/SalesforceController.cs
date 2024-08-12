using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using kyx_demo.Models;
using System.Text.RegularExpressions;
using kyx_demo.Services;
using System.Reflection.Metadata.Ecma335;

namespace kyx_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesforceController : ControllerBase
    {
        private readonly ISalesforceService _salesforceService;
        private readonly ICCService _ccService;
        private readonly IPdfService _pdfService;
        private readonly IPdfBodyService _pdfBodyService;
        private readonly ILabelService _labelService;
        public SalesforceController(ISalesforceService salesforceService, ICCService ccService, IPdfService pdfService, IPdfBodyService pdfBodyService, ILabelService labelService)
        {
            _salesforceService = salesforceService;
            _ccService = ccService;
            _pdfService = pdfService;
            _pdfBodyService = pdfBodyService;
            _labelService = labelService;
        }

        [HttpGet(Name = "GetSalesforceData")]
        public async Task<IActionResult> GetSalesforceData(string caseNumber)
        {
            var data = await _salesforceService.GetDataAsync(caseNumber);
            return Ok(data);
        }

        [HttpPost("CCDeliver")]
        public async Task<IActionResult> CCDeliver(string caseNumber, string terminalId)
        {

            var salesforceData = await _salesforceService.GetDataAsync(caseNumber);

            CCDeliver ccDeliverObject = new CCDeliver
            {
                AcquisitionType = 1, //Gör denna dynamisk
                licenseId = salesforceData.LicenseNumber.FirstOrDefault()
            };


            var shipmentBody = await _pdfBodyService.CreatePdfBody(salesforceData);

            var dhlResponse = await _labelService.CreateLabel(shipmentBody);

            var printRequest = new ConvertAndPrintRequest
            {
                Base64String = dhlResponse.Documents[0].Content,
                PrinterName = "ZDesigner GK420d (1)"
            };

            var printResponse = ConvertAndPrint(printRequest);

            //var response = await _ccService.SendDataAsync(ccDeliverObject, terminalId);
            return Ok("Data sent successfully");

            //if (response.IsSuccessStatusCode)
            //{
            //    return Ok("Data sent successfully");
            //}

            //return StatusCode((int)response.StatusCode, "Failed to send data");
        }


        [HttpPost("convert-and-print")]
        public async Task<IActionResult> ConvertAndPrint(ConvertAndPrintRequest request)
        {
            try
            {
                await _pdfService.ConvertAndPrintPdfAsync(request.Base64String, request.PrinterName);
                return Ok("PDF converted and sent to printer successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public class ConvertAndPrintRequest
        {
            public string Base64String { get; set; }
            public string PrinterName { get; set; }
        }
    }
}
