
using Functions.Integrations.Interfaces;
using Functions.Options;
using Functions.ProcessorAPI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Functions.Integrations;

public class ProcessorAPIClient : Client, IProcessorAPIClient
{    
    private readonly ILogger<ProcessorAPIClient> _logger;

    public ProcessorAPIClient(
        HttpClient httpClient,
        IOptions<ProcessorAPIOptions> options,
        ILogger<ProcessorAPIClient> logger) : base(options.Value.Url, httpClient)
    {
        _logger = logger;
    }

    public async Task<InsertParsedTransferResponseDTO> Insert(ParsedTransferDTO request)
    {
        var response = new InsertParsedTransferResponseDTO();
        
        try
        {
            response = await ProcessorAsync(request);
        }
        catch(Exception exception)
        {
            var errorMessage = $"Ocurred an unexpected error. Error: {exception.Message}";
            _logger.LogError(exception, errorMessage);
            response.Success = false;
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}
