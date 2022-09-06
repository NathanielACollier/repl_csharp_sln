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


}
