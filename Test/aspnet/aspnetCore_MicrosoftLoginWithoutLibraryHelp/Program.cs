﻿
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using lib= aspnetCore_MicrosoftLoginWithoutLibraryHelp.lib;

// show logging
nac.Logging.Logger.OnNewMessage += (_s, _args)=>{
    Console.WriteLine(_args.ToString());
};

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
