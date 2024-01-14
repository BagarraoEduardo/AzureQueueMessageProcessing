using Functions.Business.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions.Functions;

 public class ProcessorFunction
    {
        private readonly ILogger _logger;

        private readonly IProcessorService _processorService;

    public ProcessorFunction(
        ILoggerFactory loggerFactory, 
        IProcessorService processorService)
    {
        _logger = loggerFactory.CreateLogger<ProcessorFunction>();
        _processorService = processorService;
    }

    [Function("ProcessorFunction")]
    public async Task Run([TimerTrigger("*/2 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        await _processorService.InsertFiles();

        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }
}
