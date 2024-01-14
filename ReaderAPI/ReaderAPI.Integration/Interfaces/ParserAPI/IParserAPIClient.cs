using ReaderAPI.Integration.ParserAPI;

namespace ReaderAPI.Integration.Interfaces.ParserAPI;

public interface IParserAPIClient
{
    Task<ParsedTransferResponseDTO> ParseFile(FileParameter file);
}
