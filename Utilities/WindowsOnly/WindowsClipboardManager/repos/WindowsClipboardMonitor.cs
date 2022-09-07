using System;
using System.Threading;


using windowsClipboardAPI = System.Windows.Forms.Clipboard;

/*
 Windows clipboard API info is available here:
https://markheath.net/post/save-clipboard-image-to-file
 */


namespace WindowsClipboardManager.repos;

public enum ClipboardCommands
{
    Clear
}

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
    private System.Collections.Concurrent.ConcurrentQueue<ClipboardCommands> clipboardCommandsQueue;

    public WindowsClipboardMonitor()
    {
        this.notQuit = true;
        this.textToSetClipboardQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
        this.clipboardCommandsQueue = new System.Collections.Concurrent.ConcurrentQueue<ClipboardCommands>();
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
        // see if we have any clipboard commands to run
        if (this.clipboardCommandsQueue.TryDequeue(out ClipboardCommands cmd))
        {
            switch (cmd)
            {
                case ClipboardCommands.Clear:
                    windowsClipboardAPI.Clear();
                    break;
            }
        }
        
        
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
                raiseNewClipboardTextFound(currentText);
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
                )
                {
                    raiseNewClipboardImageFound(rawImageData);
                }
                

            }
            catch (Exception ex)
            {
                throwException(ex, "Converting clipboard image to byte array");
            }

        }
    }

    private void raiseNewClipboardImageFound(byte[] rawImageData)
    {
        previousClipboardImage = rawImageData;

        if (this.onClipboardImageChanged != null)
        {
            this.onClipboardImageChanged(this, rawImageData);
        }
    }

    private void raiseNewClipboardTextFound(string currentText)
    {
        previousClipboardText = currentText;
        // raise event
        if (this.onClipboardTextChanged != null)
        {
            this.onClipboardTextChanged(this, currentText);
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


    public void clearClipboard()
    {
        this.clipboardCommandsQueue.Enqueue(ClipboardCommands.Clear);
    }

    public void Stop()
    {
        this.notQuit = false; // should stop the thread from running
        this.clipboardMonitorThread.Join(); // join it so we can wait for it to quit
    }
}
