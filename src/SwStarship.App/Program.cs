using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SwStarship.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
