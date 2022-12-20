using nac.Forms;

namespace GitRepoFinder.repos;

public static class MainWindowRepo
{
    private static nac.Forms.Form myForm;
    private static models.MainWindowModel model;


    public static async Task run()
    {
        myForm = Avalonia.AppBuilder.Configure<nac.Forms.App>()
            .NewForm();

        model = new models.MainWindowModel();
        myForm.DataContext = model;

        myForm.Title = $"Windows Clipboard Manager v{model.Version}";


        myForm.Text("Hello World!")
            .Display();
    }
}