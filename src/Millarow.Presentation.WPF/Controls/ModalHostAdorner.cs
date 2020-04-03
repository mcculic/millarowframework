using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    public class ModalHostAdorner : Adorner
    {
        static ModalHostAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalHostAdorner), new FrameworkPropertyMetadata(typeof(ModalHostAdorner)));
        }

        public ModalHostAdorner(ModalHost owner)
            : base(owner)
        {
            Owner = owner;
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            if (Owner.HasItems)
            {
                var item = Owner.GetActiveItem();
                var itemPoint = Owner.TranslatePoint(hitTestParameters.HitPoint, item);
                if (item.InputHitTest(itemPoint) is Visual visual)
                    return new PointHitTestResult(visual, hitTestParameters.HitPoint);

                return new PointHitTestResult(null, hitTestParameters.HitPoint);
            }

            return base.HitTestCore(hitTestParameters);
        }

        protected ModalHost Owner { get; private set; }
    }
}