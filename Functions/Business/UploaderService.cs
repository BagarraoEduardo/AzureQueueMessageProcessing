
using AutoMapper;
using Functions.Business.Interfaces;
using Functions.Domain.Responses;
using Functions.Integrations.Interfaces;
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
    private readonly IMapper _mapper;
    private readonly QueueOptions _uploaderOptions;

    public UploaderService(
        ILogger<UploaderService> logger,
        IReaderAPIClient client,
        IAzureQueue azureQueue,
        IOptions<QueueOptions> uploaderOptions,
        IMapper mapper)
    {
        _logger = logger;
        _readerApiClient = client;
        _azureQueue = azureQueue;
        _uploaderOptions = uploaderOptions.Value;
        _mapper = mapper;
    }

    public async Task<UploadParsedTransfersResponseDDO> UploadFiles()
    {
        var response = new UploadParsedTransfersResponseDDO();

        try
        {
            var parsedTransfersResponse = _mapper.Map<ParsedTransferResponseDDO>(await _readerApiClient.ReadAvailableFiles());

            if(parsedTransfersResponse.Success)
            {
                _logger.LogInformation("Success on getting all available transfers to upload.");

                foreach(var parsedTransfer in parsedTransfersResponse.Transfers)
                {
                    try
                    {
                        var parsedTransferJsonString = JsonConvert.SerializeObject(parsedTransfer);

                        await _azureQueue.Add(_uploaderOptions.QueueName, new CloudQueueMessage(parsedTransferJsonString));

                        _logger.LogInformation($"Success on uploading transfer to the Queue. Transfer datas: {parsedTransferJsonString}");
                    }
                    catch(Exception exception)
                    {
                        _logger.LogError($"An exception has ocurred. Error: {exception.Message}.");
                    }
                }
                
                response.Success = true;
            }
            else
            {
                var errorMessage =  "An error ocurred while getting the parsed transfers.";
                _logger.LogError(errorMessage);
                response.ErrorMessage = errorMessage;
            }
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Error: {exception.Message}";
            _logger.LogError(exception, errorMessage);
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}
