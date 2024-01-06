using Functions.Business;
using Functions.Business.Interfaces;
using Functions.Integrations.Interfaces.ReaderAPI;
using Functions.Integrations.ReaderAPI;
using Functions.Options;
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

        // services.AddOptions<ReaderAPIOptions>().Configure<IConfiguration>((options, configuration) => configuration.GetSection("ReaderAPI").Bind(options));
        services.Configure<ReaderAPIOptions>(options => configuration.GetSection("ReaderAPI").Bind(options));

        services.AddHttpClient<IReaderAPIClient, ReaderAPIClient>(http => http.BaseAddress = new Uri(configuration.GetSection("ReaderAPI:Url").Value));

        services.AddScoped<IUploaderService, UploaderService>();

        
        // services.AddScoped<IReaderAPIClient, ReaderAPIClient>();

    })
    .Build();

host.Run();
