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

        myForm.Display();
    }
}

