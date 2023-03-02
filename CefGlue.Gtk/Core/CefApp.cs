using CefGlue.Gtk.Handlers;
using Xilium.CefGlue;

namespace CefGlue.Gtk.Core
{
    internal sealed class CefApp : Xilium.CefGlue.CefApp
    {
        public CefBrowserProcessHandler BrowserProcessHandler { get; set; }
        public CefResourceBundleHandler ResourceBundleHandler { get; set; }
        public CefRenderProcessHandler RenderProcessHandler { get; set; } = new RenderProcessHandler();
        protected override CefRenderProcessHandler GetRenderProcessHandler() => RenderProcessHandler;
        protected override CefBrowserProcessHandler GetBrowserProcessHandler() => BrowserProcessHandler;
        protected override CefResourceBundleHandler GetResourceBundleHandler() => ResourceBundleHandler;
    }
}