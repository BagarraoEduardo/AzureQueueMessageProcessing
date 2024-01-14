using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReaderAPI.Integration.Interfaces.ParserAPI;
using ReaderAPI.Integration.ParserAPI;
using ReaderAPI.Utility;

namespace ReaderAPI.Integration;

public class ParserAPIClient : Client, IParserAPIClient
{
    private readonly ILogger<ParserAPIClient> _logger;

    public ParserAPIClient(
        HttpClient httpClient, 
        IOptions<ParserAPIOptions> options, 
        ILogger<ParserAPIClient> logger) : base(options.Value.Url, httpClient)
    {
        _logger = logger;
    }

    public async Task<ParsedTransferResponseDTO> ParseFile(FileParameter file)
    {
        var response = new ParsedTransferResponseDTO();
        
        try
        {
            response = await ParserAsync(file);
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Error: {exception.Message}";
            _logger.LogError(exception, errorMessage);
            response.ErrorMessage = errorMessage;
            response.Success = false;
        }

        return response;
    }
}
