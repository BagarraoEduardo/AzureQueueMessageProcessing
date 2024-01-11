using Microsoft.Extensions.Logging;
using ProcessorAPI.DataAccess.Context;
using ProcessorAPI.DataAccess.Interfaces.Repositories;

namespace ProcessorAPI.DataAccess.Repositories;

public class ProcessorRepository : IProcessorRepository
{
    private readonly ILogger<ProcessorRepository> _logger;
    private readonly ProcessorContext _context;

    public ProcessorRepository(
        ProcessorContext context, 
        ILogger<ProcessorRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsertParsedTransferResponse> Insert(ParsedTransfer transfer)
    {
        var response = new InsertParsedTransferResponse();

        try
        {
            await _context.ParsedTransfers.AddAsync(transfer);

            await _context.SaveChangesAsync();

            response.Success = true;    
        }
        catch(Exception exception)
        {
            var errorMessage = $"An exception has ocurred. Exception: {exception.Message}";
            _logger.LogError(exception, errorMessage);
            response.ErrorMessage = errorMessage;
        }

        return response;
    }
}
