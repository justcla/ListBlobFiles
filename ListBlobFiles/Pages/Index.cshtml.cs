using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace ListBlobFiles.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> FilesInContainer { get; private set; }

        private readonly AzureStorageConfig _storageConfig;

        public IndexModel(IOptions<AzureStorageConfig> optionsAccessor)
        {
            _storageConfig = optionsAccessor.Value;
        }

        public void OnGet()
        {
            FilesInContainer = StorageHelper.GetBlobFileList(_storageConfig).GetAwaiter().GetResult();
        }

    }
}