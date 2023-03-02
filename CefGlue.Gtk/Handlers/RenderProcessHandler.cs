using System;
using Xilium.CefGlue;

namespace CefGlue.Gtk.Handlers
{
    public class RenderProcessHandler : CefRenderProcessHandler
    {
        public static bool DumpProcessMessages { get; set; }
        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context) => context.Dispose();
        protected override void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context) => context.Dispose();
        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (DumpProcessMessages)
            {
                Console.WriteLine("Render::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
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
            return base.OnProcessMessageReceived(browser, frame, sourceProcess, message);
        }
    }
}