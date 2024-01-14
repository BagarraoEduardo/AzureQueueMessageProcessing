using Functions.Domain.Responses;

namespace Functions.Business.Interfaces;

public interface IProcessorService
{    
    Task<InsertTransferResponseDDO> InsertFiles();
}
