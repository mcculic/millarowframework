using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.MVP.Presenters
{
    public abstract class AbstractPresenter<TView> : IPresenter
        where TView : class, IView
    {
        public AbstractPresenter(TView view)
        {
            view.AssertNotNull(nameof(view));

            View = view;
        }

        public TView View { get; }
    }
}
