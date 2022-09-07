using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClipboardManager.models;

public class MainWindowModel : nac.Forms.model.ViewModelBase
{

    public string ClipboardText
    {
        get {  return GetValue(() => ClipboardText); }
        set { SetValue(() => ClipboardText, value); }
    }


    public byte[] ClipboardImage
    {
        get { return GetValue(() => ClipboardImage); }
        set { SetValue(() => ClipboardImage, value); }
    }

    public string Version
    {
        get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString(); }
    }
}
