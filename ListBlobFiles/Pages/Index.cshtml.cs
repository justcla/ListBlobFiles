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

        public IndexModel(IConfiguration config)
        {
            // Fetch the storage configuration (stored in appsettings.json locally, or App Settings on Cloud)
            _storageConnectionString = config["StorageConnectionString"];
            _containerName = config["ContainerName"];
        }

        public void OnGet()
        {
            FilesInContainer = StorageHelper.GetBlobFileList(_storageConnectionString, _containerName).GetAwaiter().GetResult();
        }

    }
}