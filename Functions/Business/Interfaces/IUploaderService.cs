using Functions.Domain.Responses;

namespace Functions.Business.Interfaces;

public interface IUploaderService
{
    Task<UploadParsedTransfersResponseDDO> UploadFiles();
}
