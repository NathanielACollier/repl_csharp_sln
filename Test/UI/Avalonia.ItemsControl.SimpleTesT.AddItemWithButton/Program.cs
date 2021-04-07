using System;
using Avalonia.Controls;

namespace Avalonia.ItemsControl.SimpleTesT.AddItemWithButton
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = getApp();

            var win = new MainWindow();
            win.Show();
            app.Run(win);
        }

        static Avalonia.Application getApp()
        {
            var appBuilder = Avalonia.AppBuilder.Configure<App>();
            
            var builder = appBuilder
                //.LogToDebug(Avalonia.Logging.LogEventLevel.Verbose)
                .UsePlatformDetect()
                .SetupWithoutStarting();

            return builder.Instance;
        }
        
    }
}
