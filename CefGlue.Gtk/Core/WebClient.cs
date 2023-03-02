using CefGlue.Gtk.Handlers;
using System;
using Xilium.CefGlue;

namespace CefGlue.Gtk.Core
{
    public class WebClient : CefClient
    {
        private readonly LifeSpanHandler _lifeSpanHandler;
        public WebClient(WebBrowser core)
        {
            _lifeSpanHandler = new LifeSpanHandler(core);
        }
        public static bool DumpProcessMessages { get; set; }
        public CefKeyboardHandler KeyboardHandler { get; set; }
        public CefContextMenuHandler ContextMenuHandler { get; set; }
        public CefFindHandler FindHandler { get; set; }
        public CefAudioHandler AudioHandler { get; set; }
        public CefDragHandler DragHandler { get; set; }
        public CefRenderHandler RenderHandler { get; set; }
        public CefFocusHandler FocusHandler { get; set; }
        public CefDownloadHandler DownloadHandler { get; set; }
        public CefRequestHandler RequestHandler { get; set; }
        public CefJSDialogHandler JSDialogHandler { get; set; }
        public CefDialogHandler DialogHandler { get; set; }
        public CefLoadHandler LoadHandler { get; set; }
        public CefDisplayHandler DisplayHandler { get; set; }
        protected override CefLifeSpanHandler GetLifeSpanHandler() => _lifeSpanHandler;
        protected override CefDisplayHandler GetDisplayHandler()   => DisplayHandler;
        protected override CefLoadHandler GetLoadHandler()  => LoadHandler;
        protected override CefKeyboardHandler GetKeyboardHandler() => KeyboardHandler;
        protected override CefContextMenuHandler GetContextMenuHandler()  => ContextMenuHandler;
        protected override CefDialogHandler GetDialogHandler() => DialogHandler;
        protected override CefJSDialogHandler GetJSDialogHandler()  => JSDialogHandler;
        protected override CefRequestHandler GetRequestHandler()=> RequestHandler;
        protected override CefDownloadHandler GetDownloadHandler()  => DownloadHandler;
        protected override CefFocusHandler GetFocusHandler() => FocusHandler;
        protected override CefRenderHandler GetRenderHandler()   => RenderHandler;
        protected override CefDragHandler GetDragHandler()=> DragHandler;
        protected override CefAudioHandler GetAudioHandler()=> AudioHandler;
        protected override CefFindHandler GetFindHandler()=> FindHandler;
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (DumpProcessMessages)
            {
                Console.WriteLine("Client::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
                Console.WriteLine("Message Name={0} IsValid={1} IsReadOnly={2}", message.Name, message.IsValid, message.IsReadOnly);
                var arguments = message.Arguments;
                for (var i = 0; i < arguments.Count; i++)
                {
                    var type = arguments.GetValueType(i);
                    object value = type switch
                    {
                        CefValueType.Null => null,
                        CefValueType.String => arguments.GetString(i),
                        CefValueType.Int => arguments.GetInt(i),
                        CefValueType.Double => arguments.GetDouble(i),
                        CefValueType.Bool => arguments.GetBool(i),
                        _ => null,
                    };
                    Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
                }
            }
            return false;
        }
    }
}