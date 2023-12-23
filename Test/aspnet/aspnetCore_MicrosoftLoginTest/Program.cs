var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

/*
Trying to follow this documentation:
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-8.0
*/

services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = commonUtilitiesLib.settings.Get("TestAuthAzureAppID");
        microsoftOptions.ClientSecret = commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret");
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
