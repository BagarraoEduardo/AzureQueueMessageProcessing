using System;
using Functions.Business.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Functions.Functions
{
    public class UploaderFunction
    {
        private readonly ILogger _logger;
        private readonly IUploaderService _uploaderService;

        public UploaderFunction(ILoggerFactory loggerFactory, IUploaderService uploaderService)
        {
            _logger = loggerFactory.CreateLogger<UploaderFunction>();
            _uploaderService = uploaderService;
        }

        [Function("UploaderFunction")]
        public async Task Run([TimerTrigger("0 */1 * * *", RunOnStartup = true)] TimerInfo myTimer)
        {
            var x = await _uploaderService.UploadFiles();


            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
