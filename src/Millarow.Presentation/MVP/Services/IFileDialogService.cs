using System.Collections.Generic;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Services
{
    public interface IFileDialogService
    {
        Task<string> ShowOpenDialogAsync(IEnumerable<FileDialogFilter> filters, string initialPath = null);

        Task<string> ShowSaveDialogAsync(IEnumerable<FileDialogFilter> filters, string initialPath = null);
    }

    public class FileDialogFilter
    {
        public string Description { get; set; }

        public IEnumerable<string> Extensions { get; set; }
    }
}