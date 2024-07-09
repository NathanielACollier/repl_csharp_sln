
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using lib= aspnetCore_MicrosoftLoginWithoutLibraryHelp.lib;

var log = new nac.Logging.Logger();
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

// har logging stuff
var harEntries = new List<nac.http.logging.har.model.Entry>();
nac.http.logging.har.LoggingHandler.isEnabled = true;
nac.http.logging.har.LoggingHandler.onMessage += (_s, _args) =>
{
    harEntries.Add(_args);
};

app.Run();

// when website is done save the har entries
string harFileJSON = nac.http.logging.har.lib.utility.BuildHARFileJSON(harEntries);
System.IO.File.WriteAllText("http.har", harFileJSON);
