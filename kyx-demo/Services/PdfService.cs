namespace kyx_demo.Services;
using IronPdf;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Threading.Tasks;
public class PdfService :IPdfService
{
    public async Task ConvertAndPrintPdfAsync(string base64, string printerName)
    {
        try
        {
            byte[] pdfBytes = Convert.FromBase64String(base64);
            string tempPdfPath = Path.Combine(Path.GetTempPath(), "label.pdf");

            await File.WriteAllBytesAsync(tempPdfPath, pdfBytes);
            PrinterSettings settings = new PrinterSettings()
            {
                PrinterName = printerName,
                Copies = 1
            };
            var pdfDocument = PdfDocument.FromFile(tempPdfPath);


            await PrintPdfAsync(pdfDocument, settings);

            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
    private async Task PrintPdfAsync(PdfDocument pdfDocument, PrinterSettings settings)
    {
        PrintDocument doc = pdfDocument.GetPrintDocument(settings);
        await Task.Run(doc.Print);
    }
}
