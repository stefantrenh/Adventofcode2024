using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Application.Services.DistanceCalulator;
using Adventofcode2024.Application.Services.EncryptCorruptedFileCalculator;
using Adventofcode2024.Application.Services.SafetyReportService;
using Adventofcode2024.Infrastructure.FileReaders;
using Adventofcode2024.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;

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
                .AddSingleton<IEncryptCorruptedFileService, EncryptCorruptedFileService>()
                .AddSingleton<IDistanceCalculatorService, DistanceCalulatorService>()
                .AddSingleton<ISafetyCalculatorService, SafetyCalculatorService>()
                .BuildServiceProvider();


            /*Day 1*/
            //var distanceCalculatorService = serviceProvider.GetService<IDistanceCalculatorService>();

            //var result = await distanceCalculatorService.CalculateDistanceByRangeAsync();
            //Console.WriteLine($"Calculated Distance: {result}");

            //var result2 = await distanceCalculatorService.CalculateDistanceBySameNumberAsync();
            //Console.WriteLine($"Calculated Distance: {result2}");

            /*Day 2*/
            //var safetyCalculatorService = serviceProvider.GetService<ISafetyCalculatorService>();
            //var result = await safetyCalculatorService.CalculateSafetyByRangeAsync();
            //var result2 = await safetyCalculatorService.CalculateSafetyWithDampenerAsync();
            //Console.WriteLine(result);
            //Console.WriteLine(result2);

            /*Day 3*/
            var enCryptCorruptedFileService = serviceProvider.GetService<IEncryptCorruptedFileService>();
            var result = await enCryptCorruptedFileService.CalculateEncryptedFile();
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}