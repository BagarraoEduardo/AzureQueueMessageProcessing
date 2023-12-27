namespace ReaderAPI;

public class ParsedTransferDTO
{
    public Guid Reference { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}
