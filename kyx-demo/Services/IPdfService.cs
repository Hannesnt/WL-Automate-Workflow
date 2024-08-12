namespace kyx_demo.Services;

public interface IPdfService
{
    Task ConvertAndPrintPdfAsync(string base64, string printerName);
}
