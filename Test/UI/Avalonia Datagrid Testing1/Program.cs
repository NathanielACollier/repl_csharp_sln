﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using dotnetCoreAvaloniaNCForms;

namespace Avalonia_Datagrid_Testing1
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = Avalonia.AppBuilder.Configure<dotnetCoreAvaloniaNCForms.App>();
            app.AfterSetup((_app) =>
            {
                // there is a bug in avalonia.  see: https://github.com/AvaloniaUI/Avalonia/issues/3788
                var datagridStyleUri = new Uri("avares://Avalonia.Controls.DataGrid/Themes/Default.xaml");
                var _style = new StyleInclude(datagridStyleUri) {
                    Source = datagridStyleUri
                };
                app.Instance.Styles.Add(_style);
            });
            
            var f = app
                .NewForm();

            f.Text("Hello World!");

            var dg = new Avalonia.Controls.DataGrid();
            dg.AutoGenerateColumns = true;

            var people = new ObservableCollection<models.Person>
            {
                new models.Person
                {
                    First = "Nathaniel",
                    Last = "Collier"
                },
                new models.Person
                {
                    First = "Heather",
                    Last = "Collier"
                }
            };
            f.Model["items"] = people;
            
            f._Extend_AddBinding<ObservableCollection<models.Person>>("items", dg, Avalonia.Controls.DataGrid.ItemsProperty, 
                                                            isTwoWayDataBinding: true);
            f._Extend_AddRowToHost(dg, rowAutoHeight: false);

            

            f.Text("Below Datagrid");
            
            f.Display();
        }
    }
}
