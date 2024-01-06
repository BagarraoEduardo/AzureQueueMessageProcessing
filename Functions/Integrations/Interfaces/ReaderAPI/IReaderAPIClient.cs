namespace Functions.Integrations.Interfaces.ReaderAPI;

public interface IReaderAPIClient
{
    Task<ParsedTransferResponseDDO> ReadAvailableFiles();
}