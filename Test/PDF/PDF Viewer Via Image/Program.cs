

using Docnet.Core.Models;
using nac.Forms;
using nac.Forms.model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

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
    // Examples are here: https://github.com/GowenGit/docnet/blob/master/examples/nuget-usage/NugetUsageAnyCpu/PdfToImageExamples.cs
    using (var docReader = Docnet.Core.DocLib.Instance.GetDocReader(pdfPath, new PageDimensions(1080, 1920)))
    {
        using (var pageReader = docReader.GetPageReader(pageNumber))
        {
            var rawBytes = pageReader.GetImage(); // formats it as Bgra32
            
            var width = pageReader.GetPageWidth();
            var height = pageReader.GetPageHeight();
            
            // THis is the best documentation I found on using ImageSharp and pdfium 
            //   see: https://stackoverflow.com/questions/23905169/how-to-convert-pdf-files-to-images

            // use ImageSharp to interpret those raw bytes in B-G-R-A format into a bitmap that avalonia can understand
            var img = SixLabors.ImageSharp.Image.LoadPixelData<Bgra32>(rawBytes, width, height);
            // Set the background to white, otherwise it's black. https://github.com/SixLabors/ImageSharp/issues/355#issuecomment-333133991
            img.Mutate(x => x.BackgroundColor(Color.White));
            
            using (var ms = new System.IO.MemoryStream())
            {
                img.SaveAsBmp(ms);
                var imgData = ms.ToArray();
                // write out a test bitmap
                System.IO.File.WriteAllBytes(path: System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "out.bmp"),
                    bytes: imgData);
                return imgData;
            }
        }
    }
}