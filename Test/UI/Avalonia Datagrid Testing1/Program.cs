using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using dotnetCoreAvaloniaNCForms;

namespace Avalonia_Datagrid_Testing1
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = Avalonia.AppBuilder.Configure<dotnetCoreAvaloniaNCForms.App>()
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
