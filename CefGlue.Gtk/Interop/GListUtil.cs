﻿using System;
using System.Runtime.InteropServices;

namespace CefGlue.Gtk.Interop
{
    public class GListUtil
    {
        private readonly IntPtr _list;
        public GListUtil(IntPtr list)
        {
            _list = list;
        }
        public int Length=> g_list_length(_list);
        public void Free()
        {
            if (_list != IntPtr.Zero)
                g_list_free(_list);
        }
        public IntPtr GetItem(int nth)
        {
            if (_list != IntPtr.Zero)
                return g_list_nth_data(_list, (uint)nth);
            return IntPtr.Zero;
        }
        [DllImport(Library.GlibLib, CallingConvention = CallingConvention.Cdecl)]
        static extern int g_list_length(IntPtr l);
        [DllImport(Library.GlibLib, CallingConvention = CallingConvention.Cdecl)]
        static extern void g_list_free(IntPtr l);
        [DllImport(Library.GlibLib, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr g_list_nth_data(IntPtr l, uint n);
    }
}