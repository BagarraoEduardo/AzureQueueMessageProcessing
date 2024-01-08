
using Functions.Business.Interfaces;
using Functions.Integrations.Interfaces.ReaderAPI;
using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Functions.Options;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.Storage.Queue.Protocol;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Functions.Business;

public class UploaderService : IUploaderService
{
    private readonly ILogger<UploaderService> _logger;
    private readonly IReaderAPIClient _readerApiClient;
    private readonly IAzureQueue _azureQueue;
    private readonly UploaderOptions _uploaderOptions;

    public UploaderService(
        ILogger<UploaderService> logger,
        IReaderAPIClient client,
        IAzureQueue azureQueue,
        IOptions<UploaderOptions> uploaderOptions)
    {
        _logger = logger;
        _readerApiClient = client;
        _azureQueue = azureQueue;
        _uploaderOptions = uploaderOptions.Value;
    }

    public async Task<(bool Success, string ErrorMessage)> UploadFiles()
    {
        (bool Success, string ErrorMessage) response = (false, string.Empty);

        try
        {
            var parsedTransfersResponse = await _readerApiClient.ReadAvailableFiles();

            if(parsedTransfersResponse.Success)
            {
                Console.WriteLine("Success!");

                foreach(var parsedTransfer in parsedTransfersResponse.Transfers)
                {
                    await _azureQueue.Add(_uploaderOptions.QueueName, new CloudQueueMessage(JsonConvert.SerializeObject(parsedTransfer)));
                }
            }
            else
            {
                Console.WriteLine("Error!");
            }
        }
        catch(Exception exception)
        {
            var errorMessage = "Ocurred an unexpected error while trying to retrieve all available transfers from ReaderAPI.";
            _logger.LogError(exception, errorMessage);
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}
