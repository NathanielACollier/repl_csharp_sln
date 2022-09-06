using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClipboardManager.repos;

public static class WindowsFormsImageManipulationRepo
{

    /*
        From here: https://stackoverflow.com/questions/3801275/how-to-convert-image-to-byte-array
    */
    public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
    }





}
