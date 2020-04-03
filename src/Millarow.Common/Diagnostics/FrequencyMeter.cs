using Millarow.Collections;
using System.Diagnostics;

namespace Millarow.Diagnostics
{
    public sealed class FrequencyMeter
    {
        private readonly RingBuffer<long> _window;
        private long _lastTimestamp;

        public FrequencyMeter(int windowSize)
        {
            _window = new RingBuffer<long>(windowSize);

            Reset();
        }

        public void Reset()
        {
            _window.Clear();
            _lastTimestamp = Stopwatch.GetTimestamp();
        }

        public void Update()
        {
            var timestamp = Stopwatch.GetTimestamp();
            var interval = timestamp - _lastTimestamp;

            lock (_window)
            {
                _window.Add(interval);
                _lastTimestamp = timestamp;
            }
        }

        public double EstimateFrequency()
        {
            lock (_window)
            {
                if (_window.Count == 0)
                    return 0;

                var summ = 0L;
                for (int i = 0; i < _window.Count; i++)
                    summ += _window[i];

                var avg = (double)summ / _window.Count;

                return Stopwatch.Frequency / avg;
            }
        }

        public int WindowSize
        {
            get { return _window.Capacity; }
        }
    }
}