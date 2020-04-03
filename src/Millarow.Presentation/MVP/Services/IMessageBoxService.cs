using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Services
{
    public interface IMessageBoxService
    {
        Task ShowMessage(string message, string caption, MessageBoxIcon icon);

        Task<bool> ShowConfirmation(string message, string caption, MessageBoxIcon icon);

        Task<bool> ShowQuestion(string message, string caption, MessageBoxIcon icon);

        Task<bool?> ShowMaybeQuestion(string message, string caption, MessageBoxIcon icon);
    }
}