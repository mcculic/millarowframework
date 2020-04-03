using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Controls
{
    [ContentProperty("ContentTemplate")]
    public class CachedContentPresenter : Decorator
    {
        private readonly ConditionalWeakTable<object, ContentPresenter> _presenterCache;

        static CachedContentPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CachedContentPresenter), new FrameworkPropertyMetadata(typeof(CachedContentPresenter)));
        }

        public CachedContentPresenter()
        {
            _presenterCache = new ConditionalWeakTable<object, ContentPresenter>();

            DataContextChanged += (s, e) => UpdateChild(e.NewValue);
        }

        private void UpdateChild(object item)
        {
            Child = GetPresenter(item);
        }

        private ContentPresenter GetPresenter(object item)
        {
            if (item == null)
                return null;

            if (!_presenterCache.TryGetValue(item, out var presenter))
            {
                presenter = new ContentPresenter();
                presenter.Content = item;
                presenter.SetBinding(ContentPresenter.ContentTemplateProperty, new Binding(nameof(ContentTemplate)) { Source = this });
                presenter.SetBinding(ContentPresenter.ContentTemplateSelectorProperty, new Binding(nameof(ContentTemplateSelector)) { Source = this });

                _presenterCache.Add(item, presenter);
            }

            return presenter;
        }

        #region ContentTemplateProperty
        public static DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(CachedContentPresenter));
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        #endregion

        #region ContentTemplateSelectorProperty
        public static DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register("ContentTemplateSelector", typeof(DataTemplateSelector), typeof(CachedContentPresenter));
        public DataTemplateSelector ContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty); }
            set { SetValue(ContentTemplateSelectorProperty, value); }
        }
        #endregion
    }
}
