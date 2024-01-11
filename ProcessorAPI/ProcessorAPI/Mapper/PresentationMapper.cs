using AutoMapper;
using ProcessorAPI.Domain;

namespace ProcessorAPI;

public class PresentationMapper : Profile
{
    public PresentationMapper()
    {
        CreateMap<BaseResponseDTO, BaseResponseDDO>().ReverseMap();
        CreateMap<InsertParsedTransferResponseDTO, InsertParsedTransferResponseDDO>().ReverseMap();
        CreateMap<ParsedTransferDTO, ParsedTransferDDO>().ReverseMap();		
    }
}
