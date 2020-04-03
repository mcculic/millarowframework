using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Millarow.Win32.User32
{
    internal static class User32Native
    {
        private const string LibraryFileName = "user32.dll";

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport(LibraryFileName, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(LibraryFileName)]
        public static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.BStr)] StringBuilder text, int count);

        [DllImport(LibraryFileName, ExactSpelling = true, SetLastError = true)]
        public static extern int ScrollWindowEx(IntPtr hWnd, int nXAmount, int nYAmount, IntPtr prcScroll, IntPtr prcRectClip, IntPtr hrgnUpdate, IntPtr prcUpdate, int flags);

        //add all ExactSpelling = true,
        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern int PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }
}