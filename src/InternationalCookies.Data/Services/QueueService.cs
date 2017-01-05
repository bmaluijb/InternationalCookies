using InternationalCookies.Data.Interfaces;
using InternationalCookies.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace InternationalCookies.Data.Services
{
    public class QueueService : IQueueService
    {
        private ConnectionStrings _connectionStrings;
        private CloudStorageAccount _storageAccount;


        public QueueService(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;

            if (connectionStrings.Value != null && !string.IsNullOrEmpty(connectionStrings.Value.AzureStorageConnection))
            {
                _storageAccount = CloudStorageAccount.Parse(connectionStrings.Value.AzureStorageConnection);
            }
        }

        public async void QueueNewStoreCreation(string userAndStoreData)
        {
            // Create the CloudQueueClient object for the storage account.
            CloudQueueClient queueClient = _storageAccount.CreateCloudQueueClient();

            // Get a reference to the CloudQueue named "NewStoreQueue"
            CloudQueue newStoreQueue = queueClient.GetQueueReference("newstorequeue");

            // Create the CloudQueue if it does not exist.
            await newStoreQueue.CreateIfNotExistsAsync();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(userAndStoreData);
            await newStoreQueue.AddMessageAsync(message);
        }
    }
}
