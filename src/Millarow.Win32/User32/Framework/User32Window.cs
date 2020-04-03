using System;
using System.Text;

using static Millarow.Win32.User32.User32Const;

namespace Millarow.Win32.User32.Framework
{
    public struct User32Window
    {
        public User32Window(IntPtr handle)
        {
            Handle = handle;
        }

        public bool PostMessage(uint message, IntPtr wParam, IntPtr lParam)
        {
            return User32Native.PostMessage(Handle, message, wParam, lParam) != 0;
        }

        public string GetText()
        {
            int length = User32Native.GetWindowTextLength(Handle);
            if (length == 0)
                return string.Empty;

            var text = new StringBuilder(length * 2);
            User32Native.GetWindowText(Handle, text, text.Capacity);

            return text.ToString();
        }

        public void Scroll(int dx, int dy)
        {
            if (User32Native.ScrollWindowEx(Handle, dx, dy, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, SW_INVALIDATE) == -1)
                throw Win32Exc.FromLastError();
        }

        public IntPtr SetRedraw(bool redraw)
        {
            return User32Native.SendMessage(Handle, WM_SETREDRAW, redraw ? (IntPtr)1 : (IntPtr)0, (IntPtr)0);
        }

        public int GetProcessId(out int threadId)
        {
            threadId = User32Native.GetWindowThreadProcessId(Handle, out var processId);
            if (threadId == 0)
                throw Win32Exc.FromLastError();

            return processId;
        }

        public RECT GetRect()
        {
            var rect = default(RECT);
            if (!User32Native.GetWindowRect(Handle, ref rect))
                throw Win32Exc.FromLastError();

            return rect;
        }

        public void Move(int x, int y, int w, int h)
        {
            if (!User32Native.MoveWindow(Handle, x, y, w, h, true))
                throw Win32Exc.FromLastError();
        }

        public static User32Window ForegroundWindow
        {
            get => User32Native.GetForegroundWindow();
            set
            {
                if (!User32Native.SetForegroundWindow(value))
                    throw Win32Exc.FromLastError();
            }
        }

        public IntPtr Handle { get; }

        public static implicit operator User32Window(IntPtr handle) => new User32Window(handle);

        public static implicit operator IntPtr(User32Window window) => window.Handle;
    }
}
