using Millarow.Presentation.MVP;
using Millarow.Presentation.WPF.Framework;
using System;

namespace Millarow.Presentation.WPF.MVP.ViewModels
{
    public class BusyStateViewModel : ViewModel, IBusyState
    {
        public BusyStateViewModel()
        {
            CancelCommand = new DelegateCommand(Cancel, () => CanCancel && CancellationCallback != null);

            RegisterPropertyDependency(nameof(IsIndeterminate), nameof(Progress));
            RegisterPropertyDependency(nameof(IsStateVisible), nameof(State));
            RegisterPropertyDependency(nameof(IsMessageVisible), nameof(Message));
            RegisterPropertyDependency(nameof(ProgressValue), nameof(Progress));
        }

        private void Cancel()
        {
            CancellationCallback?.Invoke();
        }

        public void Reset()
        {
            IsBusy = false;
            Message = null;
            State = null;
            Progress = null;
            CanCancel = false;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetPropertyValue(ref _isBusy, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetPropertyValue(ref _message, value);
        }

        private int? _progress;
        public int? Progress
        {
            get => _progress;
            set => SetPropertyValue(ref _progress, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetPropertyValue(ref _state, value);
        }

        private bool _canCancel;
        public bool CanCancel
        {
            get => _canCancel;
            set
            {
                if (SetPropertyValue(ref _canCancel, value))
                    CancelCommand.UpdateCanExecute();
            }
        }

        private string _cancelCaption;
        public string CancelCaption
        {
            get => _cancelCaption;
            set => SetPropertyValue(ref _cancelCaption, value);
        }

        private Action _cancellationCallback;
        public Action CancellationCallback
        {
            get => _cancellationCallback;
            set
            {
                if (SetPropertyValue(ref _cancellationCallback, value))
                    CancelCommand.UpdateCanExecute();
            }
        }

        public DelegateCommand CancelCommand { get; }

        public bool IsIndeterminate => Progress == null;

        public bool IsStateVisible => State != null;

        public bool IsMessageVisible => Message != null;

        public int ProgressValue => Progress.GetValueOrDefault();
    }
}