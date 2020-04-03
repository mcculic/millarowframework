using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Millarow.Win32
{
    internal static class Win32Exc
    {
        public static Win32Exception FromLastError() => new Win32Exception(Marshal.GetLastWin32Error());
    }
}
