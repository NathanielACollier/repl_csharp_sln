

using nac.Forms;
using Plotly.NET.CSharp;
using Plotly.NET.ImageExport;

/*
 Use this search on github to find all places where people use the export functionality of plotyly with csharp
 https://github.com/search?q=Plotly.NET.ImageExport+in%3Afile+filename%3A.csproj&type=Code
 
 
 */

var form = Avalonia.AppBuilder.Configure<nac.Forms.App>()
    .NewForm();
    
form.Text("Chart Display")
    .Image(modelFieldName: "chart")
    .Display(onDisplay: async (__f) =>
    {
        var chartData = await Task.Run(async () =>
        {
            return await buildChart();
        });

        form.Model["chart"] = chartData;
    });

async Task<byte[]> buildChart()
{
    var values = new[] { 30, 10, 50, 10 };
    var labels = new[] { "Napster", "Jack", "Honey", "Mittens" };

    var pie = Chart.Pie<int,string, string>(values: values,
        Labels: labels,
        Text: "A simple bar chart");

    string imageBase64 = pie.ToBase64PNGString(Width: 1908, Height: 1024);

    return Convert.FromBase64String(imageBase64);
}