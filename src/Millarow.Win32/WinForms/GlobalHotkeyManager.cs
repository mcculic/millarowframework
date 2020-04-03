using Millarow.Win32.Kernel32.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using static Millarow.Win32.User32.User32Const;

namespace Millarow.Win32.WinForms
{
    public class GlobalHotkeyManager : IMessageFilter
    {
        public GlobalHotkeyManager(Form form)
        {
            Form = form;
            Hotkeys = new Dictionary<GlobalAtom, GlobalHotkey>();
        }

        public GlobalHotkey CreateHotkey(string hotkeyName)
        {
            var globalAtom = new GlobalAtom($"{Application.ProductName}_{hotkeyName}");

            try
            {
                var hotkey = new GlobalHotkey(Form, globalAtom.Id);

                Hotkeys.Add(globalAtom, hotkey);

                return hotkey;
            }
            catch
            {
                globalAtom.Dispose();

                throw;
            }
        }

        public void RemoveHotkey(string name)
        {
            foreach (var entry in Hotkeys)
            {
                var atom = entry.Key;
                if (atom.Name == name)
                {
                    entry.Value.Disable();
                    Hotkeys.Remove(atom);
                    break;
                }
            }
        }

        protected bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                foreach (var entry in Hotkeys)
                {
                    var atom = entry.Key;
                    if ((IntPtr)atom.Id == m.WParam)
                        return entry.Value.Invoke();
                }
            }

            return false;
        }

        bool IMessageFilter.PreFilterMessage(ref Message m) => PreFilterMessage(ref m);

        private Form Form { get; }

        private Dictionary<GlobalAtom, GlobalHotkey> Hotkeys { get; }
    }
}