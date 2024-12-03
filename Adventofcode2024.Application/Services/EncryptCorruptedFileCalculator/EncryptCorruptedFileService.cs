using Adventofcode2024.Application.Interfaces;
using System.Text.RegularExpressions;

namespace Adventofcode2024.Application.Services.EncryptCorruptedFileCalculator
{
    public class EncryptCorruptedFileService : IEncryptCorruptedFileService
    {
        private readonly IFileReaderHelper fileReaderHelper;

        public EncryptCorruptedFileService(IFileReaderHelper fileReaderHelper)
        {
            this.fileReaderHelper = fileReaderHelper;
        }

        public async Task<int> CalculateEncryptedFile()
        {
            Func<string, List<(int firstNumber, int secondNumber)>> valueMapper = line =>
            {
                var partsValue = new List<(int, int)>();

                string pattern = @"mul\((\d+),(\d+)\)";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(line);

                foreach (Match match in matches)
                {
                    var firstNumb = int.Parse(match.Groups[1].Value);
                    var secondNumb = int.Parse(match.Groups[2].Value);
                    partsValue.Add((firstNumb, secondNumb));
                }

                return partsValue;
            };

            var ienumerableValues = await fileReaderHelper.MapFileToObjectAsync("Day3.txt", valueMapper);

            var sum = 0;

            foreach (var item in ienumerableValues)
            {
                foreach (var pairValue in item)
                {
                    var multiplyNumbers = pairValue.firstNumber * pairValue.secondNumber;
                    sum += multiplyNumbers;
                }
            }

            return sum;
        }
    }
}
