using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    public class EmbeddedKeyboardSymbol
    {
        public EmbeddedKeyboardSymbol(Key key, string value, string shiftValue)
        {
            value.AssertNotNull(nameof(value));

            Key = key;
            Value = value;
            ShiftValue = shiftValue;
        }

        public string GetValue(bool shifted)
        {
            return shifted ? ShiftValue : Value;
        }

        public Key Key { get; }

        public string Value { get; }

        public string ShiftValue { get; }
    }
}