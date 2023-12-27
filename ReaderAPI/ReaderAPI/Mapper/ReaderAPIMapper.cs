using AutoMapper;
using ParserAPI.Models;
using ReaderAPI.Domain;
using ReaderAPI.Domain.Responses;

namespace ReaderAPI.Mapper;

    public class ReaderAPIMapper : Profile
    {
        public ReaderAPIMapper()
		{
			CreateMap<BaseResponseDTO, BaseResponseDDO>().ReverseMap();
			CreateMap<ParsedTransferDTO, ParsedTransferDDO>().ReverseMap();
			CreateMap<ParsedTransferResponseDTO, ParsedTransferResponseDDO>().ReverseMap();
		}
    }