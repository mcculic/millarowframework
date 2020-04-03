using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public interface IScreenPresenter : IPresenter
    {
        Task<bool> RequestClose();
    }
}
