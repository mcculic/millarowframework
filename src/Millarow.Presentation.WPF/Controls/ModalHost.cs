using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    [TemplatePart(Name = nameof(PART_Content), Type = typeof(UIElement))]
    [TemplatePart(Name = nameof(PART_Host), Type = typeof(UIElement))]
    [ContentProperty(nameof(Content))]
    public class ModalHost : ItemsControl
    {
        static ModalHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalHost), new FrameworkPropertyMetadata(typeof(ModalHost)));
        }

        public ModalHost()
        {
            Focusable = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Content = GetTemplateChild(nameof(PART_Content)) as UIElement;
            PART_Host = GetTemplateChild(nameof(PART_Host)) as UIElement;

            if (PART_Host != null)
            {
                KeyboardNavigation.SetControlTabNavigation(PART_Host, KeyboardNavigationMode.Cycle);
                KeyboardNavigation.SetDirectionalNavigation(PART_Host, KeyboardNavigationMode.Cycle);
                KeyboardNavigation.SetTabNavigation(PART_Host, KeyboardNavigationMode.Cycle);
            }

            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            if (adornerLayer != null)
                adornerLayer.Add(new ModalHostAdorner(this));

            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return;

            int count = ItemContainerGenerator.Items.Count;
            for (int i = 0; i < count; i++)
            {
                var container = ItemContainerGenerator.ContainerFromIndex(i);
                if (container is ModalHostItem item)
                    item.IsActive = i == count - 1;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ModalHostItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ModalHostItem;
        }

        protected virtual void OnIsModalModeChanged(bool value)
        {
            if (PART_Content != null)
                PART_Content.SetCurrentValue(WindowsFormsHostDecorator.CompatibilityModeProperty, value);
        }

        public ModalHostItem GetActiveItem()
        {
            if (Items.Count == 0)
                return null;

            return ItemContainerGenerator.ContainerFromItem(Items[Items.Count - 1]) as ModalHostItem;
        }

        public UIElement PART_Content { get; private set; }

        public UIElement PART_Host { get; private set; }

        #region ContentProperty
        public static DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ModalHost));
        public object Content
        {
            get => (object)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        #endregion

        #region ContentTemplateProperty
        public static DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(ModalHost));
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        #endregion

        #region ContentTemplateSelectorProperty
        public static DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register("ContentTemplateSelector", typeof(DataTemplateSelector), typeof(ModalHost));
        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }
        #endregion

        #region ContentStringFormatProperty
        public static DependencyProperty ContentStringFormatProperty = DependencyProperty.Register("ContentStringFormat", typeof(string), typeof(ModalHost));
        public string ContentStringFormat
        {
            get { return (string)GetValue(ContentStringFormatProperty); }
            set { SetValue(ContentStringFormatProperty, value); }
        }
        #endregion

        #region OverlayBrushProperty
        public static DependencyProperty OverlayBrushProperty = DependencyProperty.Register("OverlayBrush", typeof(Brush), typeof(ModalHost), new PropertyMetadata(Brushes.Transparent));
        public Brush OverlayBrush
        {
            get => (Brush)GetValue(OverlayBrushProperty);
            set => SetValue(OverlayBrushProperty, value);
        }
        #endregion

        #region OverlayOpacityProperty
        public static DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(ModalHost), new PropertyMetadata(1.0));
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }
        #endregion

        #region DialogHorizontalAlignmentProperty
        public static DependencyProperty DialogHorizontalAlignmentProperty = DependencyProperty.Register("DialogHorizontalAlignment", typeof(HorizontalAlignment), typeof(ModalHost));
        public HorizontalAlignment DialogHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(DialogHorizontalAlignmentProperty); }
            set { SetValue(DialogHorizontalAlignmentProperty, value); }
        }
        #endregion

        #region DialogVerticalAlignmentProperty
        public static DependencyProperty DialogVerticalAlignmentProperty = DependencyProperty.Register("DialogVerticalAlignment", typeof(VerticalAlignment), typeof(ModalHost));
        public VerticalAlignment DialogVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(DialogVerticalAlignmentProperty); }
            set { SetValue(DialogVerticalAlignmentProperty, value); }
        }
        #endregion
    }
}