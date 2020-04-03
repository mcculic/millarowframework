using Microsoft.Win32;
using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.WPF.MVP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP.Services
{
    public sealed class PlatformFileDialogService : IFileDialogService
    {
        private readonly IOwnerWindowProvider _ownerWindowProvider;

        public PlatformFileDialogService(IOwnerWindowProvider ownerWindowProvider)
        {
            ownerWindowProvider.AssertNotNull(nameof(ownerWindowProvider));

            _ownerWindowProvider = ownerWindowProvider;
        }

        public Task<string> ShowOpenDialogAsync(IEnumerable<FileDialogFilter> filters, string initialPath = null)
        {
            return ShowDialog<OpenFileDialog>(filters, initialPath);
        }

        public Task<string> ShowSaveDialogAsync(IEnumerable<FileDialogFilter> filters, string initialPath = null)
        {
            return ShowDialog<SaveFileDialog>(filters, initialPath);
        }

        private Task<string> ShowDialog<TDialog>(IEnumerable<FileDialogFilter> filters, string initialPath = null, Action<TDialog> configureDialog = null)
            where TDialog : FileDialog, new()
        {
            filters.AssertNotNull(nameof(filters));

            var ownerWindow = _ownerWindowProvider.GetOwnerWindow();
            var dialog = CreateDialog(filters, initialPath, configureDialog);
            if (dialog.ShowDialog(ownerWindow) == true)
                return Task.FromResult(dialog.FileName);

            throw new OperationCanceledException();
        }

        private TDialog CreateDialog<TDialog>(IEnumerable<FileDialogFilter> filters, string initialPath, Action<TDialog> configureDialog)
            where TDialog : FileDialog, new()
        {
            var dialog = new TDialog
            {
                InitialDirectory = initialPath,
                Filter = string.Join("|", filters.Select(RenderDialogFilter)),
                DereferenceLinks = true
            };

            configureDialog?.Invoke(dialog);

            return dialog;
        }

        private static string RenderDialogFilter(FileDialogFilter filter)
        {
            var exts = string.Join(";", filter.Extensions.Select(e => $"*.{e}"));

            return $"{filter.Description}|{exts}";
        }
    }
}