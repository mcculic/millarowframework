using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Millarow.Presentation.WPF.Controls
{
    public class LineBusyIndicator : UserControl
    {
        static LineBusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineBusyIndicator), new FrameworkPropertyMetadata(typeof(LineBusyIndicator)));
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(LineBusyIndicator), new FrameworkPropertyMetadata { DefaultValue = 10 }); 
        }

        #region Methods

        protected virtual void ChangeVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsBusy ? "Busy" : "Idle", useTransitions);
        }

        protected virtual void OnIsBusyChanged()
        {
            ChangeVisualState(true);            
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LineBusyIndicator)d).OnIsBusyChanged();
        }
        
        #endregion

        #region Properties

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(LineBusyIndicator), new PropertyMetadata(OnIsBusyChanged));
        public bool IsBusy
        {
            get { return (bool)GetValue(LineBusyIndicator.IsBusyProperty); }
            set { SetValue(LineBusyIndicator.IsBusyProperty, value); }
        }

        #endregion
    }
}
