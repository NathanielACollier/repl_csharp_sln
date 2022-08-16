

using nac.Forms;
using Plotly.NET.CSharp;
using Plotly.NET.ImageExport;

var form = Avalonia.AppBuilder.Configure<nac.Forms.App>()
    .NewForm();
    
form.Text("Chart Display")
    .Image(modelFieldName: "chart")
    .Display(onDisplay: async (__f) =>
    {
        var chartData = await Task.Run(() =>
        {
            return buildChart();
        });

        form.Model["chart"] = chartData;
    });

byte[] buildChart()
{
    var values = new[] { 30, 10, 50, 10 };
    var labels = new[] { "Napster", "Jack", "Honey", "Mittens" };

    var pie = Chart.Pie<int,string, string>(values: values,
        Labels: labels,
        Text: "A simple bar chart");

    string imageBase64 = pie.ToBase64PNGString(Width: 1908, Height: 1024);

    return Convert.FromBase64String(imageBase64);
}