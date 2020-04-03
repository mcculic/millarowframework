namespace Millarow.Presentation.MVP.Presenters
{
    public interface IPresenterFactory
    {
        TPresenter Create<TPresenter>() where TPresenter : IPresenter;
    }
}
