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
    private System.Drawing.Image previousClipboardImage;

    public event EventHandler<string> onClipboardTextChanged;
    public event EventHandler<(string message, Exception ex)> onException;
    public event EventHandler<byte[]> onClipboardImageChanged;

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

        if(windowsClipboardAPI.ContainsImage())
        {
            var currentImage = windowsClipboardAPI.GetImage();
            if( currentImage != previousClipboardImage)
            {
                previousClipboardImage = currentImage;
                // get bitmap bytes and raise an event
                try
                {
                    byte[] rawImageData = repos.WindowsFormsImageManipulationRepo.ImageToByteArray(currentImage);
                    if( this.onClipboardImageChanged != null)
                    {
                        this.onClipboardImageChanged(this, rawImageData);
                    }
                }
                catch(Exception ex)
                {
                    throwException(ex, "Converting clipboard image to byte array");
                }

            }
        }
    }


    private void throwException(Exception ex, string message)
    {
        if( this.onException != null)
        {
            this.onException(this, 
                (message: message,
                ex: ex)
            );
        }
    }


}
