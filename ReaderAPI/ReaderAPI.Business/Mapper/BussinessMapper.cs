using AutoMapper;
using ReaderAPI.Domain;
using ReaderAPI.Domain.Responses;
using ReaderAPI.Integration.ParserAPI;

namespace ReaderAPI.Business;

public class BussinessMapper : Profile
{
    public BussinessMapper()
    {
			CreateMap<ParsedTransferResponseDTO, ParsedTransferResponseDDO>().ReverseMap();
			CreateMap<ParsedTransferDTO, ParsedTransferDDO>()
                .ForMember(dest => dest.Amount, options => options.MapFrom(src => Convert.ToDecimal(src.Amount)))
                .ReverseMap();
    }
}
