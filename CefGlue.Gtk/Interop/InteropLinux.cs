using System;
using System.Runtime.InteropServices;

namespace CefGlue.Gtk.Interop
{
    public sealed class InteropLinux
    {
        [DllImport(Library.GtkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_widget_get_window(IntPtr widget);
        [DllImport(Library.GtkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gtk_widget_set_visual(IntPtr widget, IntPtr visual);
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_window_get_xid(IntPtr raw);
        [DllImport(Library.GtkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gtk_widget_get_display(IntPtr window);
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_display_get_xdisplay(IntPtr gdkDisplay);
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_visual_get_xvisual(IntPtr handle);
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_screen_get_default();
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_x11_screen_lookup_visual(IntPtr screen, IntPtr xvisualid);
        [DllImport(Library.GdkLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr gdk_screen_list_visuals(IntPtr raw);
        public static void SetDefaultWindowVisual(IntPtr widget)
        {
            try
            {
                var xDisplay = XOpenDisplay(IntPtr.Zero);
                var screenNumber = XDefaultScreen(xDisplay);
                var xVisual = XDefaultVisual(xDisplay, screenNumber);
                var visualId = XVisualIDFromVisual(xVisual);
                var gdkScreen = gdk_screen_get_default();
                var gdkVisualList = gdk_screen_list_visuals(gdkScreen);
                if (gdkVisualList == IntPtr.Zero)
                {
                    Console.WriteLine("Warning in LinuxNativeMethods::SetDefaultWindowVisual: List of visuals is invalid.");
                    return;
                }
                var glistUtil = new GListUtil(gdkVisualList);
                int length = glistUtil.Length;
                for (int i = 0; i < length; i++)
                {
                    var currItem = glistUtil.GetItem(i);
                    if (currItem != IntPtr.Zero)
                    {
                        var currVisual = gdk_x11_visual_get_xvisual(currItem);
                        var currVisualId = XVisualIDFromVisual(currVisual);
                        if (visualId == currVisualId)
                        {
                            var gdkVisual = gdk_x11_screen_lookup_visual(gdkScreen, currVisualId);
                            gtk_widget_set_visual(widget, gdkVisual);
                            break;
                        }
                    }
                }
                glistUtil.Free();
                XCloseDisplay(xDisplay);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        [DllImport(Library.X11Lib)]
        public static extern int XMoveResizeWindow(IntPtr display, IntPtr w, int x, int y, int width, int height);
        [DllImport(Library.X11Lib)]
        public static extern IntPtr XOpenDisplay(IntPtr display);
        [DllImport(Library.X11Lib)]
        public static extern int XCloseDisplay(IntPtr display);
        [DllImport(Library.X11Lib)]
        public static extern int XDefaultScreen(IntPtr display);
        [DllImport(Library.X11Lib)]
        public static extern IntPtr XDefaultVisual(IntPtr display, int screen);
        [DllImport(Library.X11Lib)]
        public static extern IntPtr XVisualIDFromVisual(IntPtr visual);
    }
}