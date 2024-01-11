using AutoMapper;
using Microsoft.Extensions.Logging;
using ProcessorAPI.Business.Interfaces;
using ProcessorAPI.DataAccess;
using ProcessorAPI.DataAccess.Interfaces.Repositories;
using ProcessorAPI.Domain;

namespace ProcessorAPI.Business;

public class ProcessorService : IProcessorService
{
    private readonly ILogger<ProcessorService> _logger;
    private readonly IProcessorRepository _processorRepository;
    private readonly IMapper _mapper;


    public ProcessorService(
        ILogger<ProcessorService> logger,
        IProcessorRepository processorRepository,
        IMapper mapper)
    {
        _logger = logger;
        _processorRepository = processorRepository;
        _mapper = mapper;
    }

    public async Task<InsertParsedTransferResponseDDO> Insert(ParsedTransferDDO request)
    {
        var response = new InsertParsedTransferResponseDDO();

        try
        {
            response = _mapper.Map<InsertParsedTransferResponseDDO>(await _processorRepository.Insert(_mapper.Map<ParsedTransfer>(request)));
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Exception: {exception.Message}";
            _logger.LogError(exception, errorMessage);
            response.Success = false;
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
} 
