using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    bool notQuit;
    Thread clipboardMonitorThread;
    private string previousClipboardText;

    public event EventHandler<string> onClipboardTextChanged;

    public WindowsClipboardMonitor()
    {
        this.notQuit = true;
    }

    public void StartMonitoring()
    {
        /*
         The clipboard API for Windows Forms only works in a [STA] thread.  see: https://stackoverflow.com/questions/518701/clipboard-gettext-returns-null-empty-string
         */
        this.clipboardMonitorThread = new Thread(() =>
        {
            while (notQuit)
            {
                SetModelViaClipboard();
                Thread.Sleep(millisecondsTimeout: 1 * 1000);
            }
        });

        this.clipboardMonitorThread.SetApartmentState(ApartmentState.STA);
        this.clipboardMonitorThread.Start();
    }

    private void SetModelViaClipboard()
    {
        if(windowsClipboardAPI.ContainsText())
        {
            var currentText = windowsClipboardAPI.GetText();
            if( currentText != previousClipboardText)
            {
                previousClipboardText = currentText;
                // raise event
                if( this.onClipboardTextChanged != null)
                {
                    this.onClipboardTextChanged(this, currentText);
                }
            }
        }
    }


}
