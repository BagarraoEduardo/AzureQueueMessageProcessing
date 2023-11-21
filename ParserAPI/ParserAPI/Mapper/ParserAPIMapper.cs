using AutoMapper;
using ParserAPI.Domain;
using ParserAPI.Domain.Responses;
using ParserAPI.Models;
using ParserAPI.Models.Responses;

namespace ParserAPI.Mapper;

public class ParserAPIMapper : Profile
{
    public ParserAPIMapper()
    {
        CreateMap<ParsedTransferDDO, ParsedTransferDTO>()
            .ForMember(dest => dest.Reference, origin => origin.MapFrom(src => new Guid(src.Reference)));
        CreateMap<ParsedTransferResponseDTO, ParsedTransferResponseDDO>().ReverseMap();
    }
}