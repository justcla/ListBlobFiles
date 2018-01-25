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
            // Fetch the storage configuration (stored in appsettings.json locally, or App Settings on Cloud)
            string storageConnectionString = _storageConfig.StorageConnectionString;
            string containerName = _storageConfig.ContainerName;

            FilesInContainer = StorageHelper.GetBlobFileList(storageConnectionString, containerName).GetAwaiter().GetResult();
        }

    }
}