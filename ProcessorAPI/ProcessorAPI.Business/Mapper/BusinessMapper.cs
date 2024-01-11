using AutoMapper;
using ProcessorAPI.DataAccess;
using ProcessorAPI.Domain;

namespace ProcessorAPI.Business;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {
        CreateMap<BaseResponseDDO, BaseResponse>().ReverseMap();
        CreateMap<InsertParsedTransferResponseDDO, InsertParsedTransferResponse>().ReverseMap();
        CreateMap<ParsedTransferDDO, ParsedTransfer>().ReverseMap();	
    }
}
