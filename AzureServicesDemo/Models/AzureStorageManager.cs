using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureServicesDemo.Models
{
    public class AzureStorageManager
    {
        private readonly CloudBlobContainer _container;

        public AzureStorageManager(CloudStorageAccount storageAccount)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            _container = blobClient.GetContainerReference("filescontainer");
        }

        public IEnumerable<IListBlobItem> GetBlobList()
        {

            if (!_container.Exists())
            {
                return new List<IListBlobItem>();
            }

            var listBlobItems = _container.ListBlobs();
            return listBlobItems;
        }

        public void UploadBlob(string fileName, HttpPostedFileBase file)
        {
            if (!_container.Exists())
            {
                _container.Create();
                _container.SetPermissions(new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob});
            }

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(file.InputStream);
        }
    }
}