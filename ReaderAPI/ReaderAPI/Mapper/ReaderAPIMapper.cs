using AutoMapper;
using ReaderAPI.Domain;
using ReaderAPI.Domain.Responses;
using ReaderAPI.Domain.Responses.Base;
using ReaderAPI.Models;
using ReaderAPI.Models.Responses;
using ReaderAPI.Models.Responses.Base;

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