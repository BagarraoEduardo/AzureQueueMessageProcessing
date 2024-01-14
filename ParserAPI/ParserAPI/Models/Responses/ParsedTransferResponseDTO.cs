using ParserAPI.Models.Responses.Base;

namespace ParserAPI.Models.Responses;

public class ParsedTransferResponseDTO : BaseResponseDTO
{
    public ParsedTransferDTO Data { get; set; }
}