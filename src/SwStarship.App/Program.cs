using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SwStarship.Core.Interfaces;
using SwStarship.Core.Services;
using SwStarship.Core.Util;
using System;
using System.IO;
using System.Linq;

namespace SwStarship.App
{
    class Program
    {
        public static ServiceProvider _serviceProvider { get; private set; }
        public static IConfiguration _configuration { get; set; }

        static void Main(string[] args)
        {
            SetupProgram();
            Run();
        }

        private static void Run()
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("Star Wars Starships Resupply Calculator");
            Console.WriteLine("***************************************");

            Console.WriteLine("");
            AskDistanceInput();

            Console.ReadKey();
        }

        private static async void AskDistanceInput()
        {
            Console.WriteLine("Please, input the total distance (in MGLT) that's is going to be taken by all starships:");
            var consoleInput = Console.ReadLine();
            int convertedValue;

            if (int.TryParse(consoleInput, out convertedValue))
            {
                var starshipService = _serviceProvider.GetService<IStarshipService>();
                var starshipsResponse = await starshipService.ProcessAllStarshipsTotalResupplyStopsAsync(convertedValue);

                starshipsResponse.ToList().ForEach(ss =>
                {
                    var stopsOutput = ss.NumberOfStops.HasValue ? ss.NumberOfStops.Value.ToString() : "Unkown";

                    Console.WriteLine($"Starship: {ss.Starship.Name}, Number of stops to resupply until reach the destination: {stopsOutput}");
                });

                AskTryAgain();
            }
            else
            {
                Console.WriteLine($"The value provided {consoleInput} is not valid. Try a numeric number.");
                
                AskTryAgain();
            }
        }

        private static void AskTryAgain()
        {
            Console.WriteLine("Try again? y/n");
            var tryAgainInput = Console.ReadLine();
            if ("y".Equals(tryAgainInput, StringComparison.InvariantCultureIgnoreCase))
            {
                AskDistanceInput();
            }
        }

        private static void SetupProgram()
        {
            ReadAppSettings();
            SetupDI();
        }

        private static void SetupDI()
        {
            _serviceProvider = new ServiceCollection()
                        .AddScoped<ISwApiClient, SwApiClient>()
                        .AddScoped<IStarshipService, StarshipService>()
                        .AddScoped<SupplyStopCalculator>()
                        .AddScoped((sp) => _configuration)
                        .AddScoped<IRestClient>((sp) => new RestClient(_configuration.GetSection("SwApiUrl").Value))
                        .BuildServiceProvider();
        }

        private static void ReadAppSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();            
        }
    }
}
