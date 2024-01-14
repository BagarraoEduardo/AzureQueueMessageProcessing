using ReaderAPI;
using ReaderAPI.Integration.ParserAPI;
using ReaderAPI.Models.Responses.Base;

namespace ReaderAPI.Models.Responses;

public class ParsedTransferResponseDTO : BaseResponseDTO
{
    public List<ParsedTransferDTO> Transfers { get; set; }
}