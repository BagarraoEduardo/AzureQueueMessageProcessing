using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParserAPI.Business.Interfaces;
using ParserAPI.Models;
using ParserAPI.Models.Responses;

namespace ParserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ParserController : ControllerBase
{
    private readonly ILogger<ParserController> _logger;
    private readonly IParserService _parserService;
    private readonly IMapper _mapper;


    public ParserController(
        ILogger<ParserController> logger, 
        IParserService parserService, 
        IMapper mapper)
    {
        _logger = logger;
        _parserService = parserService;
        _mapper = mapper;
    }

    [HttpPost(Name = "Parse")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Parse(IFormFile file)
    {
        var response = new ParsedTransferResponseDTO();

        try
        {
            response = _mapper.Map<ParsedTransferResponseDTO>(await _parserService.ParseTransfer(file.OpenReadStream()));

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