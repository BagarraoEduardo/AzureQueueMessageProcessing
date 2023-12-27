using System.Net.Mime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReaderAPI.Domain;
using ReaderAPI.Domain.Responses;
using ReaderAPI.Integration;
using ReaderAPI.Integration.ParserAPI;
using FileOptions = ReaderAPI.Utility.FileOptions;

namespace ReaderAPI.Business;

public class ReaderService : IReaderService
{
    private readonly IParserAPIClient _parserAPIClient;
    private readonly IFileHandler _fileHandler;
    private readonly ILogger<ReaderService> _logger;
    private FileOptions _options;

    public ReaderService(
        IParserAPIClient parserAPIClient,
        ILogger<ReaderService> logger,
        IOptions<FileOptions> options,
        IFileHandler fileHandler)
    {
        _parserAPIClient = parserAPIClient;
        _logger = logger;
        _options = options.Value;
        _fileHandler = fileHandler;
    }

    public async Task<ParsedTransferResponseDDO> ParseAvailableTransferFiles()
    {
        var response = new ParsedTransferResponseDDO();

        try
        {
            var fileList = await _fileHandler.GetAllFiles();

            for(int i = fileList.Count - 1; i >= 0; i--)
            {
                var memo = new MemoryStream(File.ReadAllBytes(fileList[i].FullName))
                {
                    Position = 0
                };

                var fileParameter = new FileParameter(memo, fileList[i].Name, MediaTypeNames.Application.Xml);

                var result = await _parserAPIClient.ParseFile(fileParameter);

                if(result.Success)
                {
                    var parsedTransfer = new ParsedTransferDDO()
                    {
                        Amount = Convert.ToDecimal(result.Data.Amount),
                        Currency = result.Data.Currency,
                        From = result.Data.From,
                        Reference = result.Data.Reference,
                        To = result.Data.From
                    };
                    response.Transfers.Add(parsedTransfer);
                    fileList[i].Delete();                    
                }
                else
                {
                    _logger.LogError("A problem ocurred while trying to parse the file. In order to be parsed on the next API request, the file will not be deleted.");
                }
            }
            response.Success = true;
        }
        catch(Exception exception)
        {
            response.ErrorMessage = $"An exception ocurred: {exception.Message}.";
        }

        return response;
    }
}
