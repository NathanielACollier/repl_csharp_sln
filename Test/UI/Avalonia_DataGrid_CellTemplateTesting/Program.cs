using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.ReactiveUI;

namespace Avalonia_DataGrid_CellTemplateTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = getApp();

            var win = new Windows.MainWindow();
            win.Show();
            app.Run(win);
        }

        static Avalonia.Application getApp()
        {
            var appBuilder = Avalonia.AppBuilder.Configure<App>();

            var builder = appBuilder
                .UsePlatformDetect()
                .LogToTrace(LogEventLevel.Debug)
                .UseReactiveUI() // UseReactiveUI is required for being able to add Items to the list, and it update the ItemsControl; see: https://github.com/AvaloniaUI/Avalonia/issues/5144
                .SetupWithoutStarting();
            
       
            return builder.Instance;
        }
    }
}
