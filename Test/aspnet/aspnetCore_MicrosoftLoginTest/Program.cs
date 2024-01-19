using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using appDB=aspnetCore_MicrosoftLoginTest.appDB;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

/*
Need to run these commands to setup the database
```
# Install the entity framework tool
dotnet tool install --global dotnet-ef
# Setup the stuff to create the initial database
dotnet ef migrations add InitialCreate
# Run this to actually create the database
dotnet ef database update
```
*/
services.AddDbContext<appDB.ApplicationDbContext>(options =>
        options.UseSqlite(connectionString: "Filename=./appDb.sqlite")
        );


services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<appDB.ApplicationDbContext>();

/*
Trying to follow this documentation:
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-8.0
*/

services.AddAuthentication()
    .AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = commonUtilitiesLib.settings.Get("TestAuthAzureAppID");
        microsoftOptions.ClientSecret = commonUtilitiesLib.settings.Get("TestAuthAzureClientSecret");
    });

services.AddAuthorization();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.MapGet("/", (HttpContext httpContext) => $"Hello {httpContext.User.Identity.Name}!")
.RequireAuthorization();

app.Run();

