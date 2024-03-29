﻿

using nac.Forms;
using Plotly.NET.CSharp;
using Plotly.NET.ImageExport;

/*
 Use this search on github to find all places where people use the export functionality of plotyly with csharp
 https://github.com/search?q=Plotly.NET.ImageExport+in%3Afile+filename%3A.csproj&type=Code
 
 
 */

var form = nac.Forms.Form.NewForm();
var model = new Model
{
    chartIsLoading = false
};
form.DataContext = model;
    
form.Text("Chart Display")
    .HorizontalGroup(hg=> {
        hg.Text("Loading chart")
        .LoadingTextAnimation();
    }, style: new nac.Forms.model.Style { isVisibleModelName= nameof(model.chartIsLoading) })
    .Image(modelFieldName: nameof(model.chartData))
    .Display(onDisplay: async (__f) =>
    {
        model.chartIsLoading = true;
        model.chartData = await Task.Run( () =>
        {
            var chartData =  buildChart();
            return chartData;
        });

        model.chartIsLoading = false;
    });

byte[] buildChart()
{
    var values = new[] { 30, 10, 50, 10 };
    var labels = new[] { "Napster", "Jack", "Honey", "Mittens" };

    var pie = Chart.Pie<int,string, string>(values: values,
        Labels: labels,
        Text: "A simple bar chart");

    string imageBase64 = pie.ToBase64PNGString(Width: 1908, Height: 1024);

    return commonUtilitiesLib.TextParse.parseBase64ImageTag(imageBase64);
}





class Model : nac.ViewModelBase.ViewModelBase
{
    public bool chartIsLoading
    {
        get { return GetValue(() => chartIsLoading); }
        set { SetValue(() => chartIsLoading, value); }
    }
    public byte[] chartData
    {
        get { return GetValue(() => chartData); }
        set { SetValue(() => chartData, value); }
    }
}