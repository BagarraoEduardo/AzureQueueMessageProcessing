using Microsoft.Azure.Storage.Queue;

namespace Functions.Integrations.Interfaces.StorageAccount.Queues;

public interface IAzureQueue
{
    Task Add(string queueName, CloudQueueMessage message);

    Task<CloudQueueMessage> Get(string queueName);

    Task<List<CloudQueueMessage>> BulkGet(string queueName, int numberOfMessages);

    Task<(bool Success, string ErrorMessage)> Remove(string queueName, CloudQueueMessage message);
}
