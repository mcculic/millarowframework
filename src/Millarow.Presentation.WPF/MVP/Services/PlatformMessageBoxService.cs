using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.WPF.MVP.Framework;
using System.Threading.Tasks;
using System.Windows;

namespace Millarow.Presentation.WPF.MVP.Services
{
    public class PlatformMessageBoxService : IMessageBoxService
    {
        private readonly IOwnerWindowProvider _ownerWindowProvider;

        public PlatformMessageBoxService(IOwnerWindowProvider ownerWindowProvider)
        {
            ownerWindowProvider.AssertNotNull(nameof(ownerWindowProvider));

            _ownerWindowProvider = ownerWindowProvider;
        }

        public Task ShowMessage(string message, string caption, MessageBoxIcon icon)
        {
            return ShowMessageBox(message, caption, MessageBoxButton.OK, icon.Convert());
        }

        public async Task<bool> ShowConfirmation(string message, string caption, MessageBoxIcon icon)
        {
            return await ShowMessageBox(message, caption, MessageBoxButton.OKCancel, icon.Convert()) == MessageBoxResult.OK;
        }

        public async Task<bool> ShowQuestion(string message, string caption, MessageBoxIcon icon)
        {
            return await ShowMessageBox(message, caption, MessageBoxButton.YesNo, icon.Convert()) == MessageBoxResult.Yes;
        }

        public async Task<bool?> ShowMaybeQuestion(string message, string caption, MessageBoxIcon icon)
        {
            var result = await ShowMessageBox(message, caption, MessageBoxButton.YesNo, icon.Convert());
            if (result == MessageBoxResult.Cancel)
                return default(bool?);

            return result == MessageBoxResult.Yes;
        }

        private Task<MessageBoxResult> ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            message.AssertNotNull(nameof(message));
            caption.AssertNotNull(nameof(caption));

            var ownerWindow = _ownerWindowProvider.GetOwnerWindow();
            if (ownerWindow == null)
                return Task.FromResult(MessageBox.Show(message, caption, button, icon));

            return Task.FromResult(MessageBox.Show(ownerWindow, message, caption, button, icon));
        }
    }
}