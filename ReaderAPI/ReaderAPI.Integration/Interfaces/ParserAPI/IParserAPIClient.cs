using ReaderAPI.Integration.ParserAPI;

namespace ReaderAPI.Integration;

public interface IParserAPIClient
{
    Task<ParsedTransferResponseDTO> ParseFile(FileParameter file);
}
