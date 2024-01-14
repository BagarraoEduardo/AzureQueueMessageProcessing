using ProcessorAPI.DataAccess.Entities;
using ProcessorAPI.DataAccess.Entities.Responses;

namespace ProcessorAPI.DataAccess.Interfaces.Repositories;

public interface IProcessorRepository
{
    Task<InsertParsedTransferResponse> Insert(ParsedTransfer transfer);
}
