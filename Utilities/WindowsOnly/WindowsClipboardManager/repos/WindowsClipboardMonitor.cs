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
    private byte[] previousClipboardImage;

    public event EventHandler<string> onClipboardTextChanged;
    public event EventHandler<(string message, Exception ex)> onException;
    public event EventHandler<byte[]> onClipboardImageChanged;

    private System.Collections.Concurrent.ConcurrentQueue<string> textToSetClipboardQueue;

    public WindowsClipboardMonitor()
    {
        this.notQuit = true;
        this.textToSetClipboardQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
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
        // may have things to add to clipboard so do those first
        if( this.textToSetClipboardQueue.TryDequeue(out string newClipboardText))
        {
            windowsClipboardAPI.SetText(newClipboardText);
        }


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

            try
            {
                byte[] rawImageData = repos.WindowsFormsImageManipulationRepo.ImageToByteArray(currentImage);

                // Is this new image a match to our previous image?
                if( previousClipboardImage == null || 
                    !WindowsFormsImageManipulationRepo.ByteArrayCompare(rawImageData, previousClipboardImage)
                ){
                    previousClipboardImage = rawImageData;

                    if (this.onClipboardImageChanged != null)
                    {
                        this.onClipboardImageChanged(this, rawImageData);
                    }
                }
                

            }
            catch (Exception ex)
            {
                throwException(ex, "Converting clipboard image to byte array");
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

    internal void SetClipboardText(string imageTag)
    {
        this.textToSetClipboardQueue.Enqueue(imageTag);
    }

    public void Stop()
    {
        this.notQuit = false; // should stop the thread from running
        this.clipboardMonitorThread.Join(); // join it so we can wait for it to quit
    }
}
