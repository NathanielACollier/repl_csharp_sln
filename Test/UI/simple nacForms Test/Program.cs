using System;
using dotnetCoreAvaloniaNCForms;

namespace simple_nacForms_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = Avalonia.AppBuilder.Configure<dotnetCoreAvaloniaNCForms.App>()
                                    .NewForm();

            f.Text("Hello World!");
            f.Display();
        }
    }
}
