

using nac.Forms;
using Plotly.NET.CSharp;
using Plotly.NET.ImageExport;

var form = Avalonia.AppBuilder.Configure<nac.Forms.App>()
    .NewForm();
    
form.Text("Chart Display")
    .Display(onDisplay: (__f) =>
    {
        
    });

byte[] buildChart()
{
    var values = new[] { 30, 10, 50, 10 };
    var labels = new[] { "Napster", "Jack", "Honey", "Mittens" };

    var pie = Chart.Pie<int,string, string>(values: values,
        Labels: labels,
        Text: "A simple bar chart");

    string imageBase64 = pie.ToBase64JPGString(Width: 1908, Height: 1024);
    
}