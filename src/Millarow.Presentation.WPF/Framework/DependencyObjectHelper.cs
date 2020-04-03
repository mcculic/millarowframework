using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Framework
{
    public static class DependencyObjectHelper
    {
        public static IEnumerable<DependencyObject> TraverseUpVisual(this DependencyObject self)
        {
            self.AssertNotNull(nameof(self));

            while (self != null)
            {
                yield return self;

                self = VisualTreeHelper.GetParent(self);
            }
        }

        public static IEnumerable<DependencyObject> TraverseUpLogical(this DependencyObject self)
        {
            self.AssertNotNull(nameof(self));

            while (self != null)
            {
                yield return self;

                self = LogicalTreeHelper.GetParent(self);
            }
        }

        public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject self)
        {
            self.AssertNotNull(nameof(self));

            var childCount = VisualTreeHelper.GetChildrenCount(self);
            for (var i = 0; i < childCount; i++)
                yield return VisualTreeHelper.GetChild(self, i);
        }

        public static IEnumerable<DependencyObject> GetVisualChildrenBreadthFirst(this DependencyObject self)
        {
            self.AssertNotNull(nameof(self));

            return GetVisualChildren(self).FlattenTreeBreadthFirst(GetVisualChildren);
        }

        public static IEnumerable<DependencyObject> GetVisualChildrenDepthFirst(this DependencyObject self)
        {
            self.AssertNotNull(nameof(self));

            return GetVisualChildren(self).FlattenTreeDepthFirst(GetVisualChildren);
        }

        public static bool SetIsSealed(this DependencyObject self, bool isSealed)
        {
            self.AssertNotNull(nameof(self));

            if (self.IsSealed == isSealed)
                return true;

            var sealProperty = typeof(DependencyObject).GetProperty("DO_Sealed", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (sealProperty == null)
                return false;
            else
            {
                sealProperty.SetValue(self, isSealed, null);
                return true;
            }
        }
    }
}
