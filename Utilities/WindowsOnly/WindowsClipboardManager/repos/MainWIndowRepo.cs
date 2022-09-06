using nac.Forms;

namespace WindowsClipboardManager.repos;

public static class MainWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.MainWindowModel model;

    public static async Task run()
    {
        myForm = Avalonia.AppBuilder.Configure<nac.Forms.App>()
        .NewForm();

        model = new models.MainWindowModel();

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

        myForm.Display();
    }

    private static void PopulateTabImage(Form t)
    {
        t.Text("Image on clipboard will show up here");
    }

    private static void PopulateTabText(Form t)
    {
        t.Text("This will be where clipboard text shows up");
    }
}

