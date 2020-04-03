using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Millarow.Win32.Kernel32
{
    internal static class Kernel32Native
    {
        private const string LibraryFileName = "kernel32.dll";

        [DllImport(LibraryFileName, BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport(LibraryFileName, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern uint GetCurrentThreadId();

        [DllImport(LibraryFileName, SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern short GlobalAddAtom([MarshalAs(UnmanagedType.BStr)] string name);

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern short GlobalDeleteAtom(Int16 nAtom);
    }
}
