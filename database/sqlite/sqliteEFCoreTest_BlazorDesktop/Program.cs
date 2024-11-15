

using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;
using sqliteEFCoreTest_BlazorDesktop;

var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);
appBuilder.Services
    .AddLogging();

// register root component
appBuilder.RootComponents.Add<App>("app");

var app = appBuilder.Build();

// customize window
app.MainWindow
    .SetTitle("SQLite EF Core Testing GUI");

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
    app.MainWindow.ShowMessage("Fatal exception", error.ExceptionObject.ToString());
};

app.Run();