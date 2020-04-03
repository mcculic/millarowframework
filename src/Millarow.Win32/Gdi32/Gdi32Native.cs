using System;
using System.Runtime.InteropServices;

namespace Millarow.Win32.Gdi32
{
    public static class Gdi32Native
    {
        private const string LibraryFileName = "gdi32.dll";

        [DllImport(LibraryFileName, SetLastError = true)]
        public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, uint fdwItalic, uint fdwUnderline, uint fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);
    }
}
