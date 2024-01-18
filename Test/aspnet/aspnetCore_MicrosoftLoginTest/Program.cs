using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using appDB=aspnetCore_MicrosoftLoginTest.appDB;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

/*
Need to run these commands to setup the database
```
dotnet tool install --global dotnet-ef
dotnet ef database update
```
*/
services.AddDbContext<appDB.ApplicationDbContext>(options =>
        options.UseSqlite(connectionString: "Filename=./appDb.sqlite")
        );

/*
Might be a way to store the user identity in memory
-- https://stackoverflow.com/questions/60316005/how-to-use-identity-in-asp-net-core-3-1-without-ef-core
-- look at this too
- https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-custom-storage-providers?view=aspnetcore-3.1
*/

services.AddIdentityCore<IdentityUser>(options => 
    { options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedEmail = true;

    })
    .AddRoles<IdentityRole>()
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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

app.Run();

