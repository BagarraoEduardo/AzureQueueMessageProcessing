using ParserAPI.Domain.Responses.Base;

namespace ParserAPI.Domain.Responses;

public class ParsedTransferResponseDDO : BaseResponseDDO
{
    public ParsedTransferDDO Data { get; set; }
}