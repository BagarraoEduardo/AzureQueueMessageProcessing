using System.Net.Mime;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReaderAPI.Domain;
using ReaderAPI.Domain.Responses;
using ReaderAPI.Integration.Interfaces.ParserAPI;
using ReaderAPI.Integration.ParserAPI;
using FileOptions = ReaderAPI.Utility.FileOptions;

namespace ReaderAPI.Business;

public class ReaderService : IReaderService
{
    private readonly IParserAPIClient _parserAPIClient;
    private readonly IFileHandler _fileHandler;
    private readonly IMapper _mapper;
    private readonly ILogger<ReaderService> _logger;

    public ReaderService(
        IParserAPIClient parserAPIClient,
        ILogger<ReaderService> logger,
        IFileHandler fileHandler,
        IMapper mapper)
    {
        _parserAPIClient = parserAPIClient;
        _logger = logger;
        _fileHandler = fileHandler;
        _mapper = mapper;
    }

    public async Task<ParsedTransferResponseDDO> ParseAvailableTransferFiles()
    {
        var response = new ParsedTransferResponseDDO();

        try
        {
            var fileList = await _fileHandler.GetAllFiles();

            for(int i = fileList.Count - 1; i >= 0; i--)
            {
                try
                {
                    var memo = new MemoryStream(File.ReadAllBytes(fileList[i].FullName))
                    {
                        Position = 0
                    };

                    var fileParameter = new FileParameter(memo, fileList[i].Name, MediaTypeNames.Application.Xml);
                    var result = await _parserAPIClient.ParseFile(fileParameter);

                    if(result.Success)
                    {
                        response.Transfers.Add(_mapper.Map<ParsedTransferDDO>(result.Data));
                        fileList[i].Delete();                    
                    }
                    else
                    {
                        _logger.LogError("An error ocurred while trying to parse the file. In order to be parsed on the next API request, the file will not be deleted.");
                    }
                }
                catch(Exception exception)
                {
                    _logger.LogError(exception, $"An exception has ocurred while parsing a file. Error: {exception.Message}. File: {fileList[i].Name}");
                }
            }
            
            response.Success = true;
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Error: {exception.Message}.";
            _logger.LogError(errorMessage);
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}
