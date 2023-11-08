using API.Dal.Extensions;
using API.Services.ConcreteClass;
using API.Services.Interfaces;
using Microsoft.Extensions.Logging.AzureAppServices;

var builder = WebApplication.CreateBuilder(args);
// Add log provider
builder.Logging.ClearProviders();
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});

builder.Services.Configure<AzureBlobLoggerOptions>(options =>
{
    options.BlobName = "log.txt";
});

// Add services to the container.
builder.Services.AddDALServices(rOpts =>
{
    var configValue = builder.Configuration.GetValue<string>("Authentication:CookieAuthentication:LoginPath");

    rOpts.ConnexionString = builder.Configuration.GetValue<string>("connectionStrings:articleRead") ?? "";
    rOpts.ProviderName = builder.Configuration.GetValue<string>("connectionStrings:providerName") ?? "";

},
wOpts =>
{
    wOpts.ConnexionString = builder.Configuration.GetValue<string>("connectionStrings:articleReadWrite") ?? "";
    wOpts.ProviderName = builder.Configuration.GetValue<string>("connectionStrings:providerName") ?? "";

});

builder.Services.AddTransient<ITopicService, TopicService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
