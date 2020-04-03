using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Input
{
    public class KeyboardActionHandler<TSender, TDataContext>
        where TSender : FrameworkElement
    {
        public KeyboardActionHandler()
        {
            Actions = new List<KeyboardAction<TSender, TDataContext>>();
        }

        public void AddAction(Action<TSender, TDataContext> action, Key key, ModifierKeys modifierKeys = ModifierKeys.None)
        {
            Actions.Add(new KeyboardAction<TSender, TDataContext>(action, key, modifierKeys));
        }

        public void HandleKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            foreach (var action in Actions)
            {
                if (action.MatchKey(e) && action.Handle(sender))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        protected List<KeyboardAction<TSender, TDataContext>> Actions { get; }
    }
}
