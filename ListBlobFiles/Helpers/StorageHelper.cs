using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListBlobFiles
{
    public class StorageHelper
    {
        private readonly ILogger<StorageHelper> logger;

        public StorageHelper(ILogger<StorageHelper> logger)
        {
            this.logger = logger;
        }

        public async Task<List<string>> GetBlobFileListAsync(string storageConnectionString, string containerName)
        {
            try
            {
                // Get Reference to Blob Container
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Fetch info about files in the container
                // Note: Loop with BlobContinuationToken to fetch results in pages. Pass null as currentToken to fetch all results.
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(currentToken: null);
                IEnumerable<IListBlobItem> blobItems = resultSegment.Results;

                // Extract the URI of the files into a new list
                List<string> fileUris = new List<string>();
                foreach (var blobItem in blobItems)
                {
                    fileUris.Add(blobItem.StorageUri.PrimaryUri.ToString());
                }
                return fileUris;
            }
            catch (System.Exception e)
            {
                // Note: When using ASP.NET Core Web Apps, to output to streaming logs, use ILogger rather than System.Diagnostics
                logger.LogError($"Exception occurred while attempting to list files on server: {e.Message}");
                return null;         // or throw e; if you want to bubble the exception up to the caller
            }
        }

    }
}
