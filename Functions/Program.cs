using Functions.Business;
using Functions.Business.Interfaces;
using Functions.Integrations.Interfaces.ReaderAPI;
using Functions.Integrations.Interfaces.StorageAccount.Queues;
using Functions.Integrations.ReaderAPI;
using Functions.Integrations.StorageAccount.Queues;
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

        services.AddLogging();
    
        services.AddSingleton(serviceCollection => CloudStorageAccount.Parse(configuration["Storage:ConnectionString"]));
    
        services.Configure<ReaderAPIOptions>(options => configuration.GetSection("ReaderAPI").Bind(options));
        services.Configure<UploaderOptions>(options => configuration.GetSection("Queue").Bind(options));

        services.AddHttpClient<IReaderAPIClient, ReaderAPIClient>(http => http.BaseAddress = new Uri(configuration.GetSection("ReaderAPI:Url").Value));

        services
            .AddScoped<IAzureQueue, AzureQueue>()
            .AddScoped<IUploaderService, UploaderService>();

    })
    .Build();

host.Run();
