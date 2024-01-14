using ProcessorAPI.Domain;
using ProcessorAPI.Domain.Responses;

namespace ProcessorAPI.Business.Interfaces;

public interface IProcessorService
{
    Task<InsertParsedTransferResponseDDO> Insert(ParsedTransferDDO request);
}