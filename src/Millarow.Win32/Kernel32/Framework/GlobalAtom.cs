using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Millarow.Win32.Kernel32.Framework
{
    public class GlobalAtom : IDisposable //TODO make base class for Win32 objects like safeHandle, add destructor
    {
        public GlobalAtom(string name)
        {
            var id = Kernel32Native.GlobalAddAtom(name);
            if (id == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            Name = name;
            Id = id;
        }

        public void Dispose()
        {
            if (!IsZero)
            {
                Kernel32Native.GlobalDeleteAtom(Id);
                Id = 0;
            }
        }

        public string Name { get; }

        //TODO add isDisposed checks
        public short Id { get; set; }

        public bool IsZero => Id == 0;
    }
}
