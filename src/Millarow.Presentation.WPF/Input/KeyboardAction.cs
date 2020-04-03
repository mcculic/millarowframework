using System;
using System.Windows;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Input
{
    public class KeyboardAction<TSender, TDataContext>
        where TSender : FrameworkElement
    {
        public KeyboardAction(Action<TSender, TDataContext> action, Key key, ModifierKeys modifierKeys)
        {
            action.AssertNotNull(nameof(action));

            Action = action;
            Key = key;
            Modifiers = modifierKeys;
        }

        public bool MatchKey(KeyEventArgs e)
        {
            return e.KeyboardDevice.Modifiers.HasFlag(Modifiers) && e.Key == Key;
        }

        public bool Handle(object sender)
        {
            if (sender is TSender typedSender && typedSender.DataContext is TDataContext dataContext)
            {
                Action(typedSender, dataContext);
                return true;
            }

            return false;
        }

        public ModifierKeys Modifiers { get; }

        public Key Key { get; }

        public Action<TSender, TDataContext> Action { get; }
    }
}