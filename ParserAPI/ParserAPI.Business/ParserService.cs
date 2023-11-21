using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using ParserAPI.Business.Interfaces;
using ParserAPI.Domain;
using ParserAPI.Domain.Responses;

namespace ParserAPI.Business;

public class ParserService : IParserService
{

    private readonly ILogger<ParserService> _logger;

    public ParserService(ILogger<ParserService> logger)
    {
        _logger = logger;
    }
    
    public async Task<ParsedTransferResponseDDO> ParseTransfer(Stream stream)
    {
        var response = new ParsedTransferResponseDDO();
        
        try
        {   
            var serializer = new XmlSerializer(typeof(ParsedTransferDDO));
            var streamReader = new StreamReader(stream);
            var xmlString = await streamReader.ReadToEndAsync();

            if (!string.IsNullOrWhiteSpace(xmlString))
            {
                var stringReader = new StringReader(xmlString);
                var parsedTransfer = serializer.Deserialize(stringReader) as ParsedTransferDDO;

                response.Data = parsedTransfer;
                response.Success = true;
            }
            else
            {
                response.ErrorMessage = "The file conversion retrieved a null or empty string.";
            }
        }
        catch (Exception exception)
        {
            var errorMessage = "An exception occurred on the service layer.";
            response.ErrorMessage = errorMessage;
            _logger.LogError(exception, errorMessage);
        }
        
        return response;
    }
}