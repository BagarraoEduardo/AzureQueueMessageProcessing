using ParserAPI.Domain.Responses;

namespace ParserAPI.Business.Interfaces;

public interface IParserService
{
    Task<ParsedTransferResponseDDO> ParseTransfer(Stream stream);
}