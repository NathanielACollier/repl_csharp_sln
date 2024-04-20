
/*
 ## Linux
 + Need libwebkit2gtk-4.0-dev
 ## ARCH
 + Install webkit2 dev with
 ```
 sudo pacman -S webkit2gtk
 ```
 */

using (var webView = new SharpWebview.Webview())
{
    webView
        .SetTitle("Google")             
        .SetSize(1024, 768, SharpWebview.WebviewHint.None)
        .SetSize(800, 600, SharpWebview.WebviewHint.Min)
        .Navigate(new SharpWebview.Content.UrlContent("https://google.com"))
        .Run();
}