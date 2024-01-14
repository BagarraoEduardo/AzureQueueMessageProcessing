using Functions.Domain.Responses.Base;

namespace Functions.Domain.Responses;

public class ParsedTransferResponseDDO : BaseResponseDDO
{
    public List<ParsedTransferDDO> Transfers { get; set; } = new List<ParsedTransferDDO>();
}
