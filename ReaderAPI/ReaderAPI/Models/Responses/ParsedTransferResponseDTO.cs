using ReaderAPI;

namespace ParserAPI.Models;

public class ParsedTransferResponseDTO : BaseResponseDTO
{
    public List<ParsedTransferDTO> Transfers { get; set; }
}