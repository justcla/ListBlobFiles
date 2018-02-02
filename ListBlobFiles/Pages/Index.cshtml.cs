using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ListBlobFiles.Pages
{
    public class IndexModel : PageModel
    {
        public List<string> FilesInContainer { get; private set; }

        private string _storageConnectionString;
        private string _containerName;
        private readonly StorageHelper storageHelper;

        public IndexModel(IConfiguration config, StorageHelper storageHelper)
        {
            // Fetch the storage configuration (stored in appsettings.json locally, or App Settings on Cloud)
            _storageConnectionString = config["StorageConnectionString"];
            _containerName = config["ContainerName"];
            this.storageHelper = storageHelper;
        }

        public void OnGet()
        {
            // Populate the files list each time a page is called.
            FilesInContainer = storageHelper.GetBlobFileListAsync(_storageConnectionString, _containerName).GetAwaiter().GetResult();
        }

    }
}