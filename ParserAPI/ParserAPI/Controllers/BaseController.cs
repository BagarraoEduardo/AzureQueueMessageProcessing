using Microsoft.AspNetCore.Mvc;

namespace ParserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "BaseController")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ParsedTransferResponseDTO), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Parse(IFormFile file)
    {
        var response = new ParsedTransferResponseDTO();

        try
        {
            response.Success = true;

            return Ok(response);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, "An exception has ocurred whily trying to parse the file.");
            return BadRequest(response); 
        }
    }
}
