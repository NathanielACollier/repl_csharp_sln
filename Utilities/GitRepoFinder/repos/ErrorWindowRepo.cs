using Avalonia.Media;
using nac.Forms;
using nac.Forms.model;

namespace GitRepoFinder.repos;

public class ErrorWindowRepo
{
    public static void run(Form parentForm, Exception exception)
    {
        parentForm.DisplayChildForm(f =>
            {
                f.Model["details"] = exception.ToString();
            
            f.Text("An exception occured", style: new Style { foregroundColor = Colors.Red })
                .TextBoxFor("details", 
                    multiline:true,
                    isReadOnly:true,
                    style: new Style{height = 400})
                .Button("ok", async () =>
                {
                    f.Close();
                });

        }, isDialog: true,
            useIsolatedModelForThisChildForm: true);
    }
}