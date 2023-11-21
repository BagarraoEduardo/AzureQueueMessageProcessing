namespace ParserAPI.Domain.Responses;

public class ParsedTransferResponseDDO
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public ParsedTransferDDO Data { get; set; }
}