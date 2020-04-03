using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Millarow.Presentation.WPF.Framework
{
    public class MappedObservableCollection<TFrom, TTo> : ReadOnlyObservableCollection<TTo>, IDisposable
    {
        public MappedObservableCollection(IList<TFrom> source, Func<TFrom, TTo> mapper) 
            : base(new ObservableCollection<TTo>())
        {
            source.AssertNotNull(nameof(source));
            mapper.AssertNotNull(nameof(mapper));

            SyncAgent = new ListSyncManager<TFrom, TTo>(source, Items, mapper);
        }

        public void Dispose()
        {
            SyncAgent.Dispose();
        }

        protected ListSyncManager<TFrom, TTo> SyncAgent { get; }

        public IList<TFrom> Source => SyncAgent.Source;
    }
}