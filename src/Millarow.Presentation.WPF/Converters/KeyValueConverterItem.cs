using System.ComponentModel;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Converters
{
    [DefaultProperty("Value")]
    [ContentProperty("Value")]
	public class KeyValueConverterItem
	{
		public object Key { get; set; }
		public object Value { get; set; }
	}
}
