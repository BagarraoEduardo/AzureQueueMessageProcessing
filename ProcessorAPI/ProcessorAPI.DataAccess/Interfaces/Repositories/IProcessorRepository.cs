namespace ProcessorAPI.DataAccess.Interfaces.Repositories;

public interface IProcessorRepository
{
    Task<InsertParsedTransferResponse> Insert(ParsedTransfer transfer);
}
