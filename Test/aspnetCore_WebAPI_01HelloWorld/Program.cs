
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapFallbackToPage("/_Host");  // This is critical for it to loadup Pages/_Host.cshtml which is the starting point.  If you don't have this line you'll just see a not found error page

app.Run();
