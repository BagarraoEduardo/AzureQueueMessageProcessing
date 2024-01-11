using ProcessorAPI.Domain;

namespace ProcessorAPI.Business.Interfaces;

public interface IProcessorService
{
    Task<InsertParsedTransferResponseDDO> Insert(ParsedTransferDDO request);
}