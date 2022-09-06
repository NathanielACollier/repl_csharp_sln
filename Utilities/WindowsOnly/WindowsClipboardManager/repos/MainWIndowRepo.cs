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

        clipboardMonitor.onClipboardTextChanged += (_s, text) =>
        {
            model.ClipboardText = text;
        };

        clipboardMonitor.onClipboardImageChanged += (_s, imageData) =>
        {
            model.ClipboardImage = imageData;
        };

        clipboardMonitor.onException += (_s, _args) =>
        {
            showException(_args.message, _args.ex);
        };

        clipboardMonitor.StartMonitoring();
    }

    private static void PopulateTabImage(Form t)
    {
        t.Text("Clipboard Image Content")
            .HorizontalGroup(hg =>
            {
                hg.Button("img tag base64", async () =>
                {
                    try
                    {
                        string imageBase64 = Convert.ToBase64String(model.ClipboardImage);
                        string imageTag = $"data:image/jpeg;base64, {imageBase64}";
                        clipboardMonitor.SetClipboardText(imageTag);
                    }
                    catch (Exception ex)
                    {
                        showException("Converting image on clipboard to base64 image tag", ex);
                    }
                }, style: new nac.Forms.model.Style
                {
                    width = 30
                });
            })
            .Image(nameof(model.ClipboardImage));
    }

    private static void PopulateTabText(Form t)
    {
        t.Text("Clipboard Text Content")
            .TextBoxFor(nameof(model.ClipboardText), 
                multiline: true, 
                isReadOnly: true
        );
    }


    private static void showException(string message, Exception ex)
    {

    }
}

