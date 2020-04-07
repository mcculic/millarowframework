using System;

namespace Millarow.Presentation.WPF.Controls
{
    public class RadioButtonGroup : IFormattable
    {
        public RadioButtonGroup()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return Name;

            return string.Format(formatProvider, format, Name);
        }

        public string Name { get; }
    }
}
