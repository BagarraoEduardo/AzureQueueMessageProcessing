using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParserAPI.Models;
using ReaderAPI.Business;

namespace ReaderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReaderController : ControllerBase
{

    private readonly ILogger<ReaderController> _logger;
    private readonly IMapper _mapper;
    private readonly IReaderService _readerService;

    public ReaderController(
        ILogger<ReaderController> logger,
        IMapper mapper,
        IReaderService readerService)
    {
        _logger = logger;
        _mapper = mapper;
        _readerService = readerService;
    }

    [HttpGet(Name = "GetParsedTransfers")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetParsedTransfers()
    {
        var response = new ParsedTransferResponseDTO();

        try
        {
            response = _mapper.Map<ParsedTransferResponseDTO>(await _readerService.ParseAvailableTransferFiles());

            response.Success = true;

            return Ok(response);
        }
        catch(Exception exception)
        {
            var errorMessage = "An exception has occurred while trying to parse the file.";
            response.ErrorMessage = errorMessage;
            _logger.LogError(exception, errorMessage);
            return BadRequest(response); 
        }
    }
}