using Millarow.Win32.User32.Framework;
using System;
using System.Windows.Forms;

namespace Millarow.Win32.WinForms
{
    public static class User32ControlHelper
    {
        public static User32Window AsUser32Window(this Control control)
        {
            control.AssertNotNull(nameof(control));

            if (!control.IsHandleCreated)
                throw new InvalidOperationException("Handle not created");

            return new User32Window(control.Handle);
        }
    }
}
