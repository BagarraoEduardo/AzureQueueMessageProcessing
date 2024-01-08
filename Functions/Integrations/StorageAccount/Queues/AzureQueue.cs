using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Logging;

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
        var messages = await _cloudQueueClient.GetQueueReference(queueName).GetMessagesAsync(numberOfMessages);

        return messages?.ToList() ?? new List<CloudQueueMessage>();
    }
}
