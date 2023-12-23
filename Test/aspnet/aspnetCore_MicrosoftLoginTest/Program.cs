var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = commonUtilitiesLib.settings.Get("TestAuthAzureAppID");
        microsoftOptions.ClientSecret = commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret");
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
