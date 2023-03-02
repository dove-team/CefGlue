using CefGlue.Gtk.Core;
using Xilium.CefGlue;

namespace CefGlue.Gtk.Handlers
{
    internal sealed class LifeSpanHandler : CefLifeSpanHandler
    {
        private readonly WebBrowser _core;
        public LifeSpanHandler(WebBrowser core)
        {
            _core = core;
        }
        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);
            _core.OnCreated(browser);
        }
        protected override bool DoClose(CefBrowser browser) => false;
        protected override void OnBeforeClose(CefBrowser browser) { }
    }
}