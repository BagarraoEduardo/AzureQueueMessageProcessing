using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace Functions.Integrations.StorageAccount.Queues;

public class AzureQueue : IAzureQueue
{
    private readonly CloudQueueClient _cloudQueueClient;
    
    public AzureQueue(
        CloudStorageAccount cloudStorageAccount)
    {
        _cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
    }

    public async Task Add(string queueName, CloudQueueMessage message)
    {
        await _cloudQueueClient
            .GetQueueReference(queueName)
            .CreateIfNotExistsAsync();

        await _cloudQueueClient
            .GetQueueReference(queueName)
            .AddMessageAsync(message);
    }

    public async Task<(bool Success, string ErrorMessage)> Remove(string queueName, CloudQueueMessage message)
    {
        await _cloudQueueClient
            .GetQueueReference(queueName)
            .DeleteMessageAsync(message);

        return (true, string.Empty);
    }

    public async Task<CloudQueueMessage> Get(string queueName)
    {
        return await _cloudQueueClient.GetQueueReference(queueName).GetMessageAsync();
    }

    public async Task<List<CloudQueueMessage>> BulkGet(string queueName, int numberOfMessages)
    {
         var queueReference = _cloudQueueClient.GetQueueReference(queueName);

        if(await queueReference.ExistsAsync())
        {
            return (await queueReference.GetMessagesAsync(numberOfMessages))?.ToList() ?? new List<CloudQueueMessage>();
        }

        return new List<CloudQueueMessage>();
    }
}
