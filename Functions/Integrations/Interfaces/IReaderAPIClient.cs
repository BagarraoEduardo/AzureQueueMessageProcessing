using Functions.ReaderAPI;

namespace Functions.Integrations.Interfaces;

public interface IReaderAPIClient
{
    Task<ParsedTransferResponseDTO> ReadAvailableFiles();
}