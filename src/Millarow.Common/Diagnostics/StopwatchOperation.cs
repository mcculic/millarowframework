using System;
using System.Diagnostics;

namespace Millarow.Diagnostics
{
    public sealed class StopwatchOperation : IDisposable
    {
        private readonly Action<TimeSpan> _onComplete;
        private readonly Stopwatch _stopwatch;

        public StopwatchOperation(Action<TimeSpan> onComplete)
        {
            onComplete.AssertNotNull(nameof(onComplete));

            _onComplete = onComplete;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _onComplete(_stopwatch.Elapsed);

            GC.SuppressFinalize(this);
        }

        public static StopwatchOperation ToConsole(string outputFormat = "{0}")
        {
            outputFormat.AssertNotNull(nameof(outputFormat));

            return new StopwatchOperation(x => Console.WriteLine(string.Format(outputFormat, x)));
        }

        public static StopwatchOperation ToTrace(string outputFormat = "{0}")
        {
            return new StopwatchOperation(x => Trace.WriteLine(string.Format(outputFormat, x)));
        }
    }
}