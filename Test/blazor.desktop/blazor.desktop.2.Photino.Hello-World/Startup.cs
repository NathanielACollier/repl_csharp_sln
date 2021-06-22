using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace blazor.desktop._2.Photino.Hello_World
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        { }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}