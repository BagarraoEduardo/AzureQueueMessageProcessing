using Functions;
using Functions.Business;
using Functions.Business.Interfaces;
using Functions.Integrations;
using Functions.Integrations.Interfaces;
using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Functions.Integrations.StorageAccount.Queues;
using Functions.Mapper;
using Functions.Options;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config => 
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", false, true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>        
    {
        var configuration = context.Configuration;

        services
            .AddLogging();

        services
            .AddAutoMapper(typeof(BusinessMapper));
    
        services.AddSingleton(serviceCollection => CloudStorageAccount.Parse(configuration["Storage:ConnectionString"]));
    
        services
            .Configure<QueueOptions>(options => configuration.GetSection("Queue").Bind(options));
        services
            .Configure<ReaderAPIOptions>(options => configuration.GetSection("ReaderAPI").Bind(options));
        services
            .Configure<ProcessorAPIOptions>(options => configuration.GetSection("ProcessorAPI").Bind(options)) ;

        services
            .AddHttpClient<IReaderAPIClient, ReaderAPIClient>(http => http.BaseAddress = new Uri(configuration.GetSection("ReaderAPI:Url").Value));
        services
            .AddHttpClient<IProcessorAPIClient, ProcessorAPIClient>(http => http.BaseAddress = new Uri(configuration.GetSection("ProcessorAPI:Url").Value));

        services
            .AddScoped<IAzureQueue, AzureQueue>();
            
        services
            .AddScoped<IUploaderService, UploaderService>();
        services
            .AddScoped<IProcessorService, ProcessorService>();
    })
    .Build();

host.Run();
