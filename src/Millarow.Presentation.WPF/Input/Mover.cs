using System;
using System.Windows;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Input
{
    public class Mover
    {
        public static Mover CreateAttached(UIElement inputElement)
        {
            inputElement.AssertNotNull(nameof(inputElement));

            var mover = new Mover();
            mover.Attach(inputElement);

            return mover;
        }

        public void Attach(UIElement inputElement)
        {
            inputElement.AssertNotNull(nameof(inputElement));

            if (IsAttached)
                throw new InvalidOperationException("Already attached");

            InputElement = inputElement;
            InputElement.PreviewMouseMove += InputElement_PreviewMouseMove;
            InputElement.PreviewMouseLeftButtonDown += InputElement_PreviewMouseLeftButtonDown;
            InputElement.PreviewMouseLeftButtonUp += InputElement_PreviewMouseLeftButtonUp;

            IsAttached = true;
        }

        public void Detach()
        {
            if (IsAttached)
            {
                InputElement.PreviewMouseMove -= InputElement_PreviewMouseMove;
                InputElement.PreviewMouseLeftButtonDown -= InputElement_PreviewMouseLeftButtonDown;
                InputElement.PreviewMouseLeftButtonUp -= InputElement_PreviewMouseLeftButtonUp;
                InputElement = null;

                IsAttached = false;
            }
        }

        protected virtual MoverElementTestEventArgs HandleElementTest(UIElement sourceElement)
        {
            var args = new MoverElementTestEventArgs(sourceElement);
            ResolveManipulation?.Invoke(this, args);

            return args;
        }

        protected virtual bool HandleBeginManipulation(UIElement sourceElement, RectManipulations manipulations)
        {
            var args = new MoverManipulationEventArgs(sourceElement, manipulations);
            BeginManipulation?.Invoke(this, args);

            return !args.Cancel;
        }

        protected virtual bool HandlePerformManipulation(UIElement sourceElement, RectManipulations manipulations, Vector sizeVector, Vector positionVector)
        {
            var args = new MoverPerformManipulationEventArgs(sourceElement, manipulations, sizeVector, positionVector);
            PerformManipulation?.Invoke(this, args);

            return !args.Cancel;
        }

        protected virtual void HandleReleaseManipulation(UIElement sourceElement, RectManipulations manipulations)
        {
            var args = new MoverManipulationEventArgs(sourceElement, manipulations);
            ReleaseManipulation?.Invoke(this, args);
        }

        private void InputElement_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Handled)
                return;

            if (!IsManipulating && ManageCursor)
            {
				if (e.OriginalSource is UIElement sourceElement)
                {
                    var testArgs = HandleElementTest(sourceElement);
					if (!testArgs.CanMove && !testArgs.CanResize)
					{
						SetCursorIfNeed(sourceElement, MoveUtility.GetCursor(RectManipulations.None));
						return;
					}

                    var inputRect = new Rect(sourceElement.RenderSize);
                    var inputPoint = e.GetPosition(sourceElement);
                    var manipulations = MoveUtility.GetRectManipulations(inputRect, inputPoint, testArgs.ResizeBorderPadding, testArgs.CanMove, testArgs.CanResize);

                    SetCursorIfNeed(sourceElement, MoveUtility.GetCursor(manipulations));
                }
            }
            else if (IsManipulating && e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePos = MouseInfo.GetCursorPosition();
                MoveUtility.GetManipulationsVectors(CurrentInfo.Manipulations, mousePos, CurrentInfo.PrevMousePos, out var positionVector, out var sizeVector);
                CurrentInfo.PrevMousePos = mousePos;

                if (positionVector.Length != 0 || sizeVector.Length != 0)
                {
                    if (!HandlePerformManipulation(CurrentInfo.SourceElement, CurrentInfo.Manipulations, sizeVector, positionVector))
                        ReleaseCurrentManipulation();
                }

                e.Handled = true;
            }
        }

        private void InputElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;

            if (!IsManipulating && e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.OriginalSource is UIElement sourceElement)
                {
                    var testArgs = HandleElementTest(sourceElement);
                    if (!testArgs.CanMove && !testArgs.CanResize)
                        return;

                    var inputRect = new Rect(sourceElement.RenderSize);
                    var inputPoint = e.GetPosition(sourceElement);
                    var manipulations = MoveUtility.GetRectManipulations(inputRect, inputPoint, testArgs.ResizeBorderPadding, testArgs.CanMove, testArgs.CanResize);

                    if (manipulations != RectManipulations.None)
                    {
                        if (!HandleBeginManipulation(sourceElement, manipulations))
                            return;

                        if (!sourceElement.CaptureMouse())
                        {
                            HandleReleaseManipulation(sourceElement, manipulations);
                            return;
                        }

                        CurrentInfo = new MoverManipulationInfo(sourceElement, manipulations, MouseInfo.GetCursorPosition());
                        SetCursorIfNeed(sourceElement, MoveUtility.GetCursor(manipulations));

                        e.Handled = true;
                    }
                }
            }
        }

        private void InputElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;

            if (IsManipulating && e.LeftButton == MouseButtonState.Released)
            {
                ReleaseCurrentManipulation();
                e.Handled = true;
            }
        }

        private void ReleaseCurrentManipulation()
        {
            HandleReleaseManipulation(CurrentInfo.SourceElement, CurrentInfo.Manipulations);
            CurrentInfo.SourceElement.ReleaseMouseCapture();
            CurrentInfo = null;
        }
        
        private void SetCursorIfNeed(UIElement target, Cursor cursor)
        {
            if (ManageCursor && target is FrameworkElement fel)
            {
                if (fel.Cursor != cursor)
                    fel.SetCurrentValue(FrameworkElement.CursorProperty, cursor);
            }
        }

        private MoverManipulationInfo CurrentInfo { get; set; }

        public UIElement InputElement { get; private set; }

        public bool IsAttached { get; private set; }

        public bool ManageCursor { get; set; } = true;

        public bool IsManipulating => CurrentInfo != null;

        public event EventHandler<MoverElementTestEventArgs> ResolveManipulation;

        public event EventHandler<MoverManipulationEventArgs> BeginManipulation;

        public event EventHandler<MoverPerformManipulationEventArgs> PerformManipulation;

        public event EventHandler<MoverManipulationEventArgs> ReleaseManipulation;
    }
}