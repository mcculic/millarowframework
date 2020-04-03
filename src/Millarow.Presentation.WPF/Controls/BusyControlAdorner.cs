using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    public class BusyControlAdorner : Adorner
    {
        static BusyControlAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyControlAdorner), new FrameworkPropertyMetadata(typeof(BusyControlAdorner)));
        }

        public BusyControlAdorner(UIElement adornedElement, BusyControl owner)
            : base(adornedElement)
        {
            owner.AssertNotNull(nameof(owner));
            
            Owner = owner;
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            if (Owner.IsBusy)
                return new PointHitTestResult(null, hitTestParameters.HitPoint);

            return base.HitTestCore(hitTestParameters);
        }

        protected BusyControl Owner { get; private set; }
    }
}