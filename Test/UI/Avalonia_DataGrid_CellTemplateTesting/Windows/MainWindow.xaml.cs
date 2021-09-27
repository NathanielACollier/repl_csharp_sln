using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace Avalonia_DataGrid_CellTemplateTesting.Windows
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools(); // requires Avalonia.Diagnostics nuget package (namespace Avalonia)
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            addExtraColumns();
        }

        private void addExtraColumns()
        {
            var dg = this.FindControl<DataGrid>("dg1");
            
            var col = new Avalonia.Controls.DataGridTemplateColumn();
            col.Header = "First Name (Code)";
            col.CellTemplate = new FuncDataTemplate<object>((itemModel, namescope) =>
            {
                var p = itemModel as model.Person;

                return new Avalonia.Controls.TextBlock
                {
                    Text = p.Firstname
                };
            });
            
            dg.Columns.Add(col);
        }
    }
}