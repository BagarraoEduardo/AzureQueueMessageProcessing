namespace Functions;

public class ParsedTransferResponseDDO : BaseResponseDDO
{
    public List<ParsedTransferDDO> Transfers { get; set; } = new List<ParsedTransferDDO>();
}
