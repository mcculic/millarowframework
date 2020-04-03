using System.Runtime.InteropServices;

namespace Millarow.Win32.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;

        public int Top;

        public int Right;

        public int Bottom;
    }
}
