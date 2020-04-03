using Millarow.Presentation.WPF.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Millarow.Presentation.WPF.Converters
{
    public class MappedCollectionConverter<TFrom, TTo> : AbstractTypedConverter<IList<TFrom>, MappedObservableCollection<TFrom, TTo>>
    {
        public MappedCollectionConverter(Func<TFrom, TTo> mapper)
        {
            mapper.AssertNotNull(nameof(mapper));

            Mapper = mapper;
        }

        public override MappedObservableCollection<TFrom, TTo> Convert(IList<TFrom> value, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return new MappedObservableCollection<TFrom, TTo>(value, Mapper);
        }

        public override IList<TFrom> ConvertBack(MappedObservableCollection<TFrom, TTo> value, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return value.Source;
        }

        protected Func<TFrom, TTo> Mapper { get; }
    }
}
