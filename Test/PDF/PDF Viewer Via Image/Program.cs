

using Docnet.Core.Models;
using nac.Forms;
using nac.Forms.model;

var form = Avalonia.AppBuilder.Configure<nac.Forms.App>()
    .NewForm();

form.Text("PDF Viewer")
    .HorizontalGroup(hg =>
    {
        hg.Text("PDF File", style: new Style{width = 50})
            .FilePathFor("pdfPath");
    })
    .HorizontalGroup(hg =>
    {
        hg.Text("Page")
            .TextBoxFor("page", convertFromUIToModel: (text) =>
            {
                if (int.TryParse(text, out int myNumber))
                {
                    return myNumber;
                }

                return 0;
            })
            .Button("Render", (_args) =>
            {
                int page = (int)form.Model["page"];
                string pdfPath = form.Model["pdfPath"] as string;

                form.Model["imgData"] = convertPDFPageToImage(pdfPath: pdfPath, pageNumber: page);
            });
    }).Image("imgData");
    
form.Display();



byte[] convertPDFPageToImage(string pdfPath, int pageNumber){
    // see example here: https://stackoverflow.com/questions/12831742/convert-pdf-to-image-without-using-ghostscript-dll
    // library is at: https://github.com/GowenGit/docnet
    using (var docReader = Docnet.Core.DocLib.Instance.GetDocReader(pdfPath, new PageDimensions()))
    {
        using (var pageReader = docReader.GetPageReader(pageNumber))
        {
            var bytes = pageReader.GetImage();
            return bytes;
        }
    }
}