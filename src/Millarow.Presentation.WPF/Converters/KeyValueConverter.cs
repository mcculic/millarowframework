using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Converters
{
    [ContentProperty("Items")]
	public class KeyValueConverter : AbstractTypedConverter<object, object>
	{
        private Dictionary<object, object> _dictionary;

		public KeyValueConverter()
		{
			Items = new List<KeyValueConverterItem>();
		}

        public void Add(object key, object value)
        {
            Items.Add(new KeyValueConverterItem { Key = key, Value = value });
        }

        public override object Convert(object value, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DefaultValue;

            if (_dictionary == null)
                _dictionary = Items.ToDictionary(x => x.Key, x => x.Value);

            object ret;
            if (_dictionary.TryGetValue(value, out ret))
                return ret;
            else
                return DefaultValue;
        }

        public List<KeyValueConverterItem> Items { get; private set; }
		public object DefaultValue { get; set; }

        public object[] Keys
        {
            get { return Items.Select(x => x.Key).ToArray(); }
        }
	}
}
