namespace ParserAPI.Models.Responses;

public class ParsedTransferResponseDTO
{
    
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public ParsedTransferDTO Data { get; set; }
}