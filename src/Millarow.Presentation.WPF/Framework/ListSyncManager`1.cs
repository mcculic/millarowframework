using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Millarow.Presentation.WPF.Framework
{
    public class ListSyncManager<TFrom, TTo> : IDisposable
    {
        public ListSyncManager(IList<TFrom> source, IList<TTo> target, Func<TFrom, TTo> mapper, Action<TTo> removeHandler = null)
        {
            source.AssertNotNull(nameof(source));
            target.AssertNotNull(nameof(target));
            mapper.AssertNotNull(nameof(mapper));

            Source = source;
            Target = target;
            Mapper = mapper;
            RemoveHandler = removeHandler;

            foreach (var item in source)
                target.Add(Mapper(item));

            if (source is INotifyCollectionChanged incc)
                incc.CollectionChanged += Source_CollectionChanged;
        }

        private void Source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                var index = e.NewStartingIndex;

                foreach (var item in e.NewItems.Cast<TFrom>())
                    Target.Insert(index++, Mapper(item));
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                throw new NotImplementedException();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                for (int i = 0; i < e.OldItems.Count; i++)
                {
                    var index = e.OldStartingIndex + i;

                    var item = Target[index];
                    Target.RemoveAt(index);

                    RemoveHandler?.Invoke(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                throw new NotImplementedException();
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var item in Target)
                    RemoveHandler?.Invoke(item);

                Target.Clear();

                foreach (var item in Source)
                    Target.Add(Mapper(item));
            }
        }
        
        public void Dispose()
        {
            if (Source is INotifyCollectionChanged incc)
                incc.CollectionChanged -= Source_CollectionChanged;
        }

        public IList<TFrom> Source { get; }

        public IList<TTo> Target { get; }

        public Func<TFrom, TTo> Mapper { get; }

        public Action<TTo> RemoveHandler { get; }
    }
}