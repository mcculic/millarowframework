namespace Millarow.Presentation.MVP.Views
{
    public interface IPageView
    {
        string Caption { get; set; }

        IView ContentView { get; set; }
    }
}
