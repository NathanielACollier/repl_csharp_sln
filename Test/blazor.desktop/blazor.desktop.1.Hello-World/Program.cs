using System;

namespace blazor.desktop.Hello_World
{
    class Program
    {
        static void Main(string[] args)
        {
            WebWindows.Blazor.ComponentsDesktop.Run<Startup>("Hello World!", "wwwroot/index.html");
        }
    }
}
