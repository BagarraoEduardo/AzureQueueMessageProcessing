using AutoMapper;
using Functions.Domain.Responses;
using Functions.Integrations.Interfaces;
using Functions.Options;
using Functions.ReaderAPI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Functions.Integrations;

public class ReaderAPIClient : Client, IReaderAPIClient
{
    private readonly ILogger<ReaderAPIClient> _logger;

    public ReaderAPIClient(
        HttpClient httpClient,
        IOptions<ReaderAPIOptions> options,
        ILogger<ReaderAPIClient> logger) : base(options.Value.Url, httpClient)
    {
        _logger = logger;
    }

    public async Task<ParsedTransferResponseDTO> ReadAvailableFiles()
    {
        var response = new ParsedTransferResponseDTO();
        
        try
        {
            response = await ReaderAsync();
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Error: {exception.Message}.";
            _logger.LogError(exception, errorMessage);
            response.Success = false;
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}