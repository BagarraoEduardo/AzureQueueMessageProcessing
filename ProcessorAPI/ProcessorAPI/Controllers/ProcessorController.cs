using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProcessorAPI.Business.Interfaces;
using ProcessorAPI.Domain;
using ProcessorAPI.Models;
using ProcessorAPI.Models.Response;

namespace ProcessorAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ProcessorController : ControllerBase
{

    private readonly ILogger<ProcessorController> _logger;
    private readonly IMapper _mapper;
    private readonly IProcessorService _processorService;

    public ProcessorController(
        ILogger<ProcessorController> logger,
        IProcessorService processorService,
        IMapper mapper)
    {
        _logger = logger;
        _processorService = processorService;
        _mapper = mapper;
    }

    [HttpPost(Name = "Insert")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InsertParsedTransferResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(InsertParsedTransferResponseDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromBody] ParsedTransferDTO request)
    {
        var response = new InsertParsedTransferResponseDTO();

        try
        {
            response = _mapper.Map<InsertParsedTransferResponseDTO>(await _processorService.Insert(_mapper.Map<ParsedTransferDDO>(request)));
            return Ok(response);
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has occurred. Error: {exception.Message}";
            response.ErrorMessage = errorMessage;
            response.Success = false;
            _logger.LogError(exception, errorMessage);
            return BadRequest(response); 
        }
    }
}