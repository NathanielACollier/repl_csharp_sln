using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }


    /*
     This byte array compare stuff is from: https://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net/1445405#1445405
      It's used to see if a clipboard image is new or not
     */
    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern int memcmp(byte[] b1, byte[] b2, long count);

    public static bool ByteArrayCompare(byte[] b1, byte[] b2)
    {
        // Validate buffers are the same length.
        // This also ensures that the count does not exceed the length of either buffer.  
        return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
    }





}
