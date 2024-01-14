using AutoMapper;
using ProcessorAPI.DataAccess.Entities;
using ProcessorAPI.DataAccess.Entities.Responses;
using ProcessorAPI.DataAccess.Entities.Responses.Base;
using ProcessorAPI.Domain;
using ProcessorAPI.Domain.Responses;
using ProcessorAPI.Domain.Responses.Base;

namespace ProcessorAPI.Business.Mapper;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {
        CreateMap<BaseResponseDDO, BaseResponse>().ReverseMap();
        CreateMap<InsertParsedTransferResponseDDO, InsertParsedTransferResponse>().ReverseMap();
        CreateMap<ParsedTransferDDO, ParsedTransfer>().ReverseMap();	
    }
}
