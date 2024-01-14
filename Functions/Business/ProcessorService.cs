
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Functions.Business.Interfaces;
using Functions.Domain;
using Functions.Domain.Responses;
using Functions.Integrations.Interfaces;
using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Functions.Options;
using Functions.ProcessorAPI;
using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Functions;

public class ProcessorService : IProcessorService
{
    
    private readonly ILogger<ProcessorService> _logger;
    private readonly IProcessorAPIClient _processorApiClient;
    private readonly IAzureQueue _azureQueue;
    private readonly IMapper _mapper;
    private readonly QueueOptions _uploaderOptions;

    public ProcessorService(
        ILogger<ProcessorService> logger,
        IProcessorAPIClient processorApiClient,
        IAzureQueue azureQueue,
        IOptions<QueueOptions> uploaderOptions,
        IMapper mapper)
    {
        _logger = logger;
        _processorApiClient = processorApiClient;
        _azureQueue = azureQueue;
        _uploaderOptions = uploaderOptions.Value;
        _mapper = mapper;
    }

    public async Task<InsertTransferResponseDDO> InsertFiles()
    {
        var response = new InsertTransferResponseDDO();

        try
        {
            var availableTransfers = (await _azureQueue.BulkGet(_uploaderOptions.QueueName, 5))
                .ToList();

            foreach(var cloudParsedTransfer in availableTransfers)
            {
                try
                {
                    var parsedTransfer = JsonConvert.DeserializeObject<ParsedTransferDDO>(cloudParsedTransfer.AsString);

                    if(parsedTransfer != null)
                    {
                        ParsedTransferDDO transfer = parsedTransfer;
                        
                        var insertResponse = await _processorApiClient.Insert(_mapper.Map<ParsedTransferDTO>(transfer));

                        if(insertResponse.Success)
                        {
                            _logger.LogInformation($"Success on inserting the transfer on the database. Transfer data: {cloudParsedTransfer.AsString}");

                           await _azureQueue.Remove(_uploaderOptions.QueueName, cloudParsedTransfer);

                            _logger.LogInformation($"Removed transfer from the Queue. Transfer data: {cloudParsedTransfer.AsString}");
                        }
                    }
                }
                catch(Exception exception)
                {
                    _logger.LogError(exception, $"An exception has ocurred. Error: {exception.Message}");
                }
            }

            response.Success = true;
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
