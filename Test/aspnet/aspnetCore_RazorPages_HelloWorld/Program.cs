

/*
  Followed this article to document how to do these razor pages
    https://codemag.com/Article/2303051/Clean-Shave-Razor-Pages-for-Web-Forms-Developers?utm_source=FOCUS10.11.2023&utm_medium=newsletter&utm_campaign=sm-articles
*/
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();