using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClipboardManager.models;

using windowsClipboardAPI = System.Windows.Forms.Clipboard;

/*
 Windows clipboard API info is available here:
https://markheath.net/post/save-clipboard-image-to-file
 */


namespace WindowsClipboardManager.repos;

public class WindowsClipboardMonitor
{
    models.MainWindowModel model;
    bool notQuit;

    public WindowsClipboardMonitor(MainWindowModel model)
    {
        this.model = model;
        this.notQuit = true;
    }

    public Task StartMonitoring()
    {
        return Task.Run(async () =>
        {
            while(notQuit)
            {
                SetModelViaClipboard();
                await Task.Delay(millisecondsDelay: 1 * 1000);
            }
        });
    }

    private void SetModelViaClipboard()
    {
        if(windowsClipboardAPI.ContainsText())
        {
            model.ClipboardText = windowsClipboardAPI.GetText();
        }
    }
}
