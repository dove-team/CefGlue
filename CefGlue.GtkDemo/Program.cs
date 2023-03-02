using CefGlue.Gtk.Core;
using CefGlue.Gtk.Interop;
using CefGlue.Gtk;
using Gtk;

namespace CefGlue.GtkDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var runtime = new Runtime();
            runtime.Initialize();
            Application.Init();
            using var window = new Window("ChromiumGTK Demo")
            {
                WidthRequest = 1200,
                HeightRequest = 800
            };
            window.Destroyed += (sender, args) => runtime.QuitMessageLoop();
            InteropLinux.SetDefaultWindowVisual(window.Handle);
            using var webView = new WebView();
            webView.LoadUrl("https://dotnet.microsoft.com/");
            window.Add(webView);
            window.ShowAll();
            runtime.RunMessageLoop();
            runtime.Shutdown();
        }
    }
}