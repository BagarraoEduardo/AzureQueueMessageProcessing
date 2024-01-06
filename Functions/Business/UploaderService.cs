
using Functions.Business.Interfaces;
using Functions.Integrations.Interfaces.ReaderAPI;
using Microsoft.Extensions.Logging;

namespace Functions.Business;

public class UploaderService : IUploaderService
{
    private readonly ILogger<UploaderService> _logger;
    private readonly IReaderAPIClient _readerApiClient;

    public UploaderService(
        ILogger<UploaderService> logger, 
        IReaderAPIClient client)
    {
        _logger = logger;
        _readerApiClient = client;
    }

    public async Task<(bool Success, string ErrorMessage)> UploadFiles()
    {
        (bool Success, string ErrorMessage) response = (false, string.Empty);

        try
        {
            var parsedTransfers = await _readerApiClient.ReadAvailableFiles();

            if(parsedTransfers.Success)
            {
                Console.WriteLine("Success!");
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
