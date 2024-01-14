using AutoMapper;
using Functions.Domain;
using Functions.Domain.Responses;
using Functions.Domain.Responses.Base;
using Functions.ReaderAPI;
using ProcessorAPIParsedTransferDTO = Functions.ProcessorAPI.ParsedTransferDTO;

namespace Functions.Mapper;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {
        CreateMap<ParsedTransferDTO, ParsedTransferDDO>()
            .ForMember(dest => dest.Amount, origin => origin.MapFrom(src => Convert.ToDecimal(src.Amount)))
            .ReverseMap();
        CreateMap<ParsedTransferResponseDTO, ParsedTransferResponseDDO>().ReverseMap();
        
        CreateMap<ParsedTransferDDO, ProcessorAPIParsedTransferDTO>()
            .ForMember(dest => dest.Amount, origin => origin.MapFrom(src => Convert.ToDouble(src.Amount)))
            .ReverseMap();
    }
}
