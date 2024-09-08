
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using lib= aspnetCore_MicrosoftLoginWithoutLibraryHelp.lib;

var log = new nac.Logging.Logger();
var harLogManager = new nac.http.logging.har.lib.HARLogManager("http.har");

// show logging
nac.Logging.Appenders.ColoredConsole.Setup();
log.Info("Website starting...");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers(options => {
    options.Filters.Add(new lib.HttpRedirectFiltering.HttpRedirectOnExceptionFilter());
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapRazorPages();

app.Run();

