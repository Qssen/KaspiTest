using System;
using System.Threading.Tasks;
using KaspiTest.Exceptions;
using KaspiTest.Infrastructure.Impl;
using KaspiTest.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace KaspiTest
{
    class Program
    {
        private const string MainPage = "https://tengrinews.kz";

        private static IServiceProvider _serviceProvider;
        
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            ConfigureServices();
            ConfigureLogger();

            Log.Information("Starting");

            IPageCrawler crawler = _serviceProvider.GetService<IPageCrawler>();
            IPublicationService service = _serviceProvider.GetService<IPublicationService>();

            Log.Information("Getting all links");
            var links = crawler.GetAllLinksFromPage(MainPage);
            
            await foreach (var link in links)
            {
                try
                {
                    Log.Information($"Parsing: {link}");
                    var publication = await crawler.GetPublication(link);
                    Log.Information($"Saving: {link}");
                    await service.AddPublication(publication);
                }
                catch (PublicationParseException e)
                {
                    Log.Warning($"Could not parse {link}", e);
                }
                catch (Exception e)
                {
                    Log.Warning($"Could not parse or save publication {link}", e);
                }
            }

            Log.Information("Application closed with status code N");
        }

        static void ConfigureServices()
        {
            _serviceProvider = new ServiceCollection()
                .AddScoped<IPublicationService, PublicationService>()
                .AddScoped<IPageCrawler, TengriPageCrawler>()
                .AddSingleton<IWordProcessor, WordProcessor>()
            .BuildServiceProvider();
        }

        static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .WriteTo.File(@"C:\Logs\PublicationParser.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
