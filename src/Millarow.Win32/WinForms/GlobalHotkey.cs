using Millarow.Win32.User32;
using System;
using System.Windows.Forms;

using static Millarow.Win32.User32.User32Const;

namespace Millarow.Win32.WinForms
{
    public class GlobalHotkey
    {
        public GlobalHotkey(Form form, short globalAtomId)
        {
            form.AssertNotNull(nameof(form));

            Form = form;
            GlobalAtomId = globalAtomId;
        }

        public bool Invoke()
        {
            if (Callback == null)
                return false;

            return Callback();
        }

        public void Enable(Keys hotkey, Func<bool> callback)
        {
            callback.AssertNotNull(nameof(callback));

            if (Enabled)
                throw new InvalidOperationException();

            var modifiers = GetFsModifiers(hotkey, out var keysModifiers);
            var keys = (int)hotkey & ~(int)keysModifiers;

            if (!User32Native.RegisterHotKey(Form.Handle, GlobalAtomId, (uint)modifiers, (uint)keys))
                throw Win32Exc.FromLastError();

            Callback = callback;
            Enabled = true;
        }

        public void Disable()
        {
            if (!Enabled)
                throw new InvalidOperationException();

            if (!User32Native.UnregisterHotKey(Form.Handle, GlobalAtomId))
                throw Win32Exc.FromLastError();
        }

        private static int GetFsModifiers(Keys keys, out Keys keysModifiers)
        {
            var modifiers = 0;
            var detectedMods = Keys.None;

            if (Modifier(Keys.Alt))
                modifiers |= MOD_ALT;

            if (Modifier(Keys.Control))
                modifiers |= MOD_CONTROL;

            if (Modifier(Keys.Shift))
                modifiers |= MOD_SHIFT;

            if (Modifier(Keys.LWin) || keys.HasFlag(Keys.RWin))
                modifiers |= MOD_WIN;

            keysModifiers = detectedMods;

            return modifiers;

            bool Modifier(Keys m)
            {
                if (keys.HasFlag(m))
                {
                    detectedMods |= m;
                    return true;
                }

                return false;
            }
        }

        private Form Form { get; }

        private Func<bool> Callback { get; set; }

        public short GlobalAtomId { get; }

        public bool Enabled { get; private set; }
    }
}
