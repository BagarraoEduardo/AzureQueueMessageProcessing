using AutoMapper;
using ProcessorAPI.Domain;
using ProcessorAPI.Domain.Responses;
using ProcessorAPI.Domain.Responses.Base;
using ProcessorAPI.Models;
using ProcessorAPI.Models.Response;
using ProcessorAPI.Models.Response.Base;

namespace ProcessorAPI.Mapper;

public class PresentationMapper : Profile
{
    public PresentationMapper()
    {
        CreateMap<BaseResponseDTO, BaseResponseDDO>().ReverseMap();
        CreateMap<InsertParsedTransferResponseDTO, InsertParsedTransferResponseDDO>().ReverseMap();
        CreateMap<ParsedTransferDTO, ParsedTransferDDO>().ReverseMap();		
    }
}
