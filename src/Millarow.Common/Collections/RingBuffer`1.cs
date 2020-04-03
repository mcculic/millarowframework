using System;
using System.Collections;
using System.Collections.Generic;

namespace Millarow.Collections
{
    public sealed class RingBuffer<T> : ICollection<T>, IReadOnlyList<T>
    {
        public RingBuffer(int capacity)
        {
            capacity.AssertGreaterThanZero(nameof(capacity));

            Buffer = new T[capacity];
        }

        public void Add(T item)
        {
            Buffer[Offset % Capacity] = item;
            Offset++;

            if (Count < Capacity)
                Count++;
        }

        public void AddRange(IEnumerable<T> items)
        {
            items.AssertNotNull(nameof(items));

            foreach (var item in items)
                Add(item);
        }

        public void Clear()
        {
            Count = 0;
            Offset = 0;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                var currentItem = Buffer[GetBufferIndex(i)];
                if (Equals(currentItem, item))
                    return i;
            }

            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
                array[arrayIndex++] = Buffer[GetBufferIndex(i)];
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return Buffer[GetBufferIndex(i)];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetBufferIndex(int index)
        {
            return (index + Offset) % Count;
        }

        private T[] Buffer { get; }

        private int Offset { get; set; }

        public int Count { get; private set; }

        public int Capacity => Buffer.Length;

        public bool IsFull => Count == Capacity;

        public bool IsEmpty => Count == 0;

        public T this[int index]
        {
            get
            {
                index.AssertListIndex(nameof(index), Count);

                return Buffer[GetBufferIndex(index)];
            }
            
            set
            {
                index.AssertListIndex(nameof(index), Count);

                Buffer[GetBufferIndex(index)] = value;
            }
        }

        bool ICollection<T>.IsReadOnly => false;
    }
}