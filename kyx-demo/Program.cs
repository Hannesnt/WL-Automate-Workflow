using kyx_demo.Models;
using kyx_demo.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ISalesforceService, SalesforceService>();
builder.Services.AddTransient<ICCService, CCService>();
builder.Services.AddTransient<IPdfService, PdfService>();
builder.Services.AddTransient<IPdfBodyService, PdfBodyService>();
builder.Services.AddTransient<ILabelService, LabelService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
