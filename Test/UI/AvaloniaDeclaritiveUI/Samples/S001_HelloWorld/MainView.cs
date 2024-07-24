using Avalonia.Controls;
using Avalonia.Markup.Declarative;

namespace S001_HelloWorld;

public class MainView : ViewBase
{
    protected override object Build() =>
        new StackPanel()
        .Children(
            new TextBlock()
            .Text("Hello World!")
        );

}