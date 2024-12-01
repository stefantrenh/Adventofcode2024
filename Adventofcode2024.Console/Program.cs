using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Application.Services.DistanceCalulator;
using Adventofcode2024.Infrastructure.FileReaders;
using Adventofcode2024.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileReader, FileReader>()      
                .AddSingleton<IFilePathHelper, FilePathHelper>()      
                .AddSingleton<IFileReaderHelper, FileReaderHelper>()   
                .AddSingleton<IDistanceCalculatorService, DistanceCalulatorService>()
                .BuildServiceProvider();


            /*Day 1*/
            var distanceCalculatorService = serviceProvider.GetService<IDistanceCalculatorService>();

            var result = await distanceCalculatorService.CalculateDistanceByRangeAsync();
            Console.WriteLine($"Calculated Distance: {result}");

            var result2 = await distanceCalculatorService.CalculateDistanceBySameNumberAsync();
            Console.WriteLine($"Calculated Distance: {result2}");

            Console.ReadLine();
        }
    }
}