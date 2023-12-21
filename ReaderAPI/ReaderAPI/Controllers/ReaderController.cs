using Microsoft.AspNetCore.Mvc;

namespace ReaderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReaderController : ControllerBase
{

    private readonly ILogger<ReaderController> _logger;

    public ReaderController(ILogger<ReaderController> logger)
    {
        _logger = logger;
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