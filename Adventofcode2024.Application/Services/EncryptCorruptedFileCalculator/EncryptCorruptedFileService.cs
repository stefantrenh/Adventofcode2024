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

        private async Task<string> CombineFileLinesIntoSingleStringAsync(string fileName)
        {
            var lines = await fileReaderHelper.MapFileToObjectAsync(fileName, line => line);
            return string.Concat(lines);
        }

        public async Task<int> CalculateEncryptedFileByValidNumbers()
        {
            var combinedContent = await CombineFileLinesIntoSingleStringAsync("Day3.txt");

            var regExPatternTarget = @"mul\(\d+,\d+\)|do\(\)|don't\(\)";

            bool validMuls = true;
            var sum = 0;

            Regex regex = new Regex(regExPatternTarget);
            MatchCollection matches = regex.Matches(combinedContent);

            foreach (Match match in matches)
            {
                string value = match.Value;

                if (value == "don't()")
                {
                    validMuls = false;
                }
                else if (value == "do()")
                {
                    validMuls = true;
                }
                else if (value.StartsWith("mul(") && validMuls)
                {
                    var numbers = Regex.Match(value, @"mul\((\d+),(\d+)\)");
                    if (numbers.Success)
                    {
                        int firstNumb = int.Parse(numbers.Groups[1].Value);
                        int secondNumb = int.Parse(numbers.Groups[2].Value);
                        sum += firstNumb * secondNumb;
                    }
                }
            }

            return sum;
        }
    }
}
