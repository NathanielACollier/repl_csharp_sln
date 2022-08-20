
using Plotly.NET.CSharp;
using Plotly.NET.ImageExport;

var values = new[] { 30, 10, 50, 10 };
var labels = new[] { "Napster", "Jack", "Honey", "Mittens" };

var pie = Chart.Pie<int,string, string>(values: values,
    Labels: labels,
    Text: "A simple bar chart");

string imageBase64 = pie.ToBase64PNGString(Width: 1920, Height: 1024);

Console.WriteLine($"Chart: \n{imageBase64}");