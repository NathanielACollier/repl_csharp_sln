

using System.Drawing;
using PhotinoNET;

var window = new PhotinoWindow()
    .SetTitle("Browser Test Window")
    // set the window to be a specific size
    .SetUseOsDefaultSize(false)
    .SetSize(new Size(600, 400))
    // Center window in the middle of the screen
    .Center()
    .Load(new Uri("https://www.google.com"));
    
window.WaitForClose();