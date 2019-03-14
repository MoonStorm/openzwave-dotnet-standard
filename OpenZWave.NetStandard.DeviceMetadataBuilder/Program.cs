using System;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    class Program
    {
        static int Main(string[] args)
        {
            var success = false;
            Parser.Default.ParseArguments<DeviceMetadataBuilderConfiguration>(args)
                .WithParsed<DeviceMetadataBuilderConfiguration>(o =>
                {
                    // set up the IOC
                    var serviceCollection = new ServiceCollection();
                    ConfigureServices(serviceCollection, o);
                    var serviceProvider = serviceCollection.BuildServiceProvider();

                    var logger = serviceProvider.GetService<ILogger<Program>>();
                    try
                    {
                        // activate the processor
                        var processor = serviceProvider.GetService<DeviceMetadataBuilderProcessor>();
                        processor.Process();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("An error has occurred", ex);
                        throw;
                    }
                    finally
                    {
#if DEBUG
                        Console.ReadLine();
#endif
                    }
                });
            return success ? 0 : -1;
        }

        private static void ConfigureServices(IServiceCollection services, DeviceMetadataBuilderConfiguration configuration)
        {
            services.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddSingleton<DeviceMetadataBuilderConfiguration>((serviceProvider)=>configuration)
                .AddSingleton<DeviceConfigFileEnumerator>()
                .AddSingleton<DeviceMetadataBuilderProcessor>()
                .AddSingleton<DeviceConfigurationFileParser>();
        }
    }
}
