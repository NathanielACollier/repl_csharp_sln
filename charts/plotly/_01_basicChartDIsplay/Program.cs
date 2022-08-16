

using nac.Forms;

var form = Avalonia.AppBuilder.Configure<nac.Forms.App>()
    .NewForm();
    
form.Text("Chart Display")
    .Display();