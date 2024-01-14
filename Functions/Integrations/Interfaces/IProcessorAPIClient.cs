using Functions.Domain;
using Functions.ProcessorAPI;

namespace Functions.Integrations.Interfaces;

public interface IProcessorAPIClient
{
    Task<InsertParsedTransferResponseDTO> Insert(ParsedTransferDTO request);
}
