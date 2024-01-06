using Functions.Integrations.Interfaces.ReaderAPI;
using Functions.Options;
using Functions.ReaderAPI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Functions.Integrations.ReaderAPI;

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

    public async Task<ParsedTransferResponseDDO> ReadAvailableFiles()
    {
        var response = new ParsedTransferResponseDDO();
        
        try
        {
            var responseDto = await ReaderAsync();

            if(responseDto != null)
            {
                response.Success = responseDto.Success;
                response.ErrorMessage = responseDto.ErrorMessage;
                response.Transfers = responseDto
                    .Transfers
                    .Select(transferDto => new ParsedTransferDDO()
                    {
                        From = transferDto.From,
                        Amount = Convert.ToDecimal(transferDto.Amount),
                        Currency = transferDto.Currency,
                        Reference = transferDto.Reference,
                        To = transferDto.To
                    })
                    .ToList();
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