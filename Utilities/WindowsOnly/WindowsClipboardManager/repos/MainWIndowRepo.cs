using nac.Forms;
using System;
using System.Threading.Tasks;

namespace WindowsClipboardManager.repos;

public static class MainWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.MainWindowModel model;
    private static repos.WindowsClipboardMonitor clipboardMonitor;

    public static async Task run()
    {
        myForm = Avalonia.AppBuilder.Configure<nac.Forms.App>()
        .NewForm();

        model = new models.MainWindowModel();
        myForm.DataContext = model;

        myForm.Title = "Windows Clipboard Manager";

        myForm.Tabs(t =>
        {
            t.Header = "Text";
            t.Populate = PopulateTabText;
        }, t=>
        {
            t.Header = "Image";
            t.Populate = PopulateTabImage;
        });

        setupClipboardMonitoring();

        myForm.Display();
    }

    private static void setupClipboardMonitoring()
    {
        // start the thread to monitor the clipboard
        clipboardMonitor = new repos.WindowsClipboardMonitor();

        clipboardMonitor.onClipboardTextChanged += (_s, _args) =>
        {
            model.ClipboardText = _args;
        };

        clipboardMonitor.StartMonitoring();
    }

    private static void PopulateTabImage(Form t)
    {
        t.Text("Image on clipboard will show up here");
    }

    private static void PopulateTabText(Form t)
    {
        t.Text("Clipboard Text Content")
            .TextBoxFor(nameof(model.ClipboardText), 
                multiline: true, 
                isReadOnly: true
        );
    }
}

