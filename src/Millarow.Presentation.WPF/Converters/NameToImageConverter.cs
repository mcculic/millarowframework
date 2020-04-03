using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Millarow.Presentation.WPF.Converters
{
    public class NameToImageConverter : AbstractTypedConverter<string, ImageSource>
    {
        private readonly Dictionary<string, ImageSource> _cache;

        public NameToImageConverter()
        {
            _cache = new Dictionary<string, ImageSource>();
        }

        public NameToImageConverter(string uriStringFormat)
            : this()
        {
            UriStringFormat = uriStringFormat;
        }

        public override ImageSource Convert(string value, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(UriStringFormat))
                return null;

            var name = value as string;
            if (string.IsNullOrWhiteSpace(name))
                return null;

            if (!_cache.TryGetValue(name, out var image))
            {
                image = LoadBitmap(new Uri(string.Format(UriStringFormat, value.ToString(), UriKind.RelativeOrAbsolute)));

                if (UseCache)
                    _cache.Add(name, image);
            }

            return image;
        }

        private BitmapImage LoadBitmap(Uri sourceUri)
        {
            var ret = new BitmapImage();

            ret.BeginInit();
            ret.CacheOption = BitmapCacheOption.OnLoad;
            ret.UriSource = sourceUri;
            ret.EndInit();
            ret.Freeze();

            return ret;
        }

        public string UriStringFormat { get; set; }

        public bool UseCache { get; set; } = true;
    }
}