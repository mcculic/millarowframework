using System;

namespace Millarow.Presentation.MVP
{
    public interface IBusyState
    {
        void Reset();

        bool IsBusy { get; set; }

        string Message { get; set; }

        int? Progress { get; set; }

        string State { get; set; }

        bool CanCancel { get; set; }

        string CancelCaption { get; set; }

        Action CancellationCallback { get; set; }
    }
}