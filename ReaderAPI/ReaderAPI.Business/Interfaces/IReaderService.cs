using ReaderAPI.Domain.Responses;

namespace ReaderAPI.Business;

public interface IReaderService
{
    Task<ParsedTransferResponseDDO> ParseAvailableTransferFiles();
}
