using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Millarow.Presentation.WPF.Converters
{
    public class NameToImageConverter : AbstractTypedConverter<string, object>
    {
        private ConcurrentDictionary<string, ImageSource> _cache;

        public NameToImageConverter(string uriStringFormat)
        {
            UriStringFormat = uriStringFormat;
        }

        public override object Convert(string value, object parameter, CultureInfo culture)
        {
            if (UriStringFormat == null)
                throw new InvalidOperationException($"Property {nameof(UriStringFormat)} must be setted before calling the Convert method.");

            if (value is string name)
            {
                if (UseCache)
                    return _cache.GetOrAdd(name, LoadBitmap);

                return LoadBitmap(name);
            }

            return DependencyProperty.UnsetValue;
        }

        private BitmapImage LoadBitmap(string name)
        {
            var ret = new BitmapImage();

            ret.BeginInit();
            ret.CacheOption = BitmapCacheOption.OnLoad;
            ret.UriSource = new Uri(string.Format(UriStringFormat, name, UriKind.RelativeOrAbsolute));
            ret.EndInit();
            ret.Freeze();

            return ret;
        }

        public string UriStringFormat { get; set; }

        public bool UseCache
        {
            get => _cache != null;
            set => _cache = value ? new ConcurrentDictionary<string, ImageSource>() : null;
        }
    }
}