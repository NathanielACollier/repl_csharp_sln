
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");  // This is critical for it to loadup Pages/_Host.cshtml which is the starting point.  If you don't have this line you'll just see a not found error page

app.Run();
