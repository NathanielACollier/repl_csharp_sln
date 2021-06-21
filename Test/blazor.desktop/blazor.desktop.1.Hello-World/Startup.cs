using Microsoft.Extensions.DependencyInjection;
using WebWindows.Blazor;

namespace blazor.desktop.Hello_World
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}