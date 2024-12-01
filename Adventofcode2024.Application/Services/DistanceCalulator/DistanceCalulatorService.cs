using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Application.Services.DistanceCalulator
{
    public class DistanceCalulatorService : IDistanceCalculatorService
    {
        private readonly IFileReaderHelper _fileReaderHelper;

        public DistanceCalulatorService(IFileReaderHelper fileReaderHelper)
        {
            _fileReaderHelper = fileReaderHelper;
        }

        public async Task<int> CalculateDistanceByRangeAsync()
        {
            Func<string, (int, int)> valueMapper = line =>
            {
                try
                {
                    var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        throw new ArgumentException("Input must contain exactly two values");
                    }

                    if (!int.TryParse(parts[0], out var value1) || !int.TryParse(parts[1], out var value2))
                    {
                        throw new FormatException($"Invalid number format in line: {line}");
                    }

                    return (value1, value2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");

                    return (0, 0);
                }
            };

            var IenumarbleValues = await _fileReaderHelper.MapFileToObjectAsync("Day1.txt", valueMapper);

            var distanceList1 = IenumarbleValues.Select(x => x.Item1)
                                             .OrderBy(x => x)
                                             .ToList();

            var distanceList2 = IenumarbleValues.Select(x => x.Item2)
                                    .OrderBy(x => x)
                                    .ToList();

            if (distanceList1.Count != distanceList2.Count)
            {
                throw new InvalidOperationException("The number of elements in both list must be the same");
            }

            var sum = 0;

            for (int i = 0; i < distanceList1.Count; i++)
            {
                var diff = Math.Abs(distanceList2[i] - distanceList1[i]);
                Console.WriteLine($"Difference at index {i}: {distanceList2[i]} - {distanceList1[i]} = {diff}");
                sum += diff;
            }

            return sum;
        }

        public async Task<int> CalculateDistanceBySameNumberAsync()
        {
            Func<string, (int, int)> valueMapper = line =>
            {
                try
                {
                    var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        throw new ArgumentException("Input must contain exactly two values");
                    }

                    if (!int.TryParse(parts[0], out var value1) || !int.TryParse(parts[1], out var value2))
                    {
                        throw new FormatException($"Invalid number format in line: {line}");
                    }

                    return (value1, value2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");

                    return (0, 0);
                }
            };

            var IenumarbleValues = await _fileReaderHelper.MapFileToObjectAsync("Day1.txt", valueMapper);

            var distanceList1 = IenumarbleValues.Select(x => x.Item1)
                                             .OrderBy(x => x)
                                             .ToList();

            var distanceList2 = IenumarbleValues.Select(x => x.Item2)
                                    .OrderBy(x => x)
                                    .ToList();

            if (distanceList1.Count != distanceList2.Count)
            {
                throw new InvalidOperationException("The number of elements in both list must be the same");
            }

            var sum = 0;

            var validNumbers = distanceList1.Where(x => distanceList2.Contains(x)).ToList();

            var uniqueNumbers = validNumbers.ToHashSet();

            var validNumbersAndCountTuple = new List<(int number, int count)>();

            foreach (var number in uniqueNumbers)
            {
                int count = distanceList2.Count(x => x == number);
                validNumbersAndCountTuple.Add((number, count));
            }

            foreach (var number in validNumbers)
            {
                foreach (var value in validNumbersAndCountTuple)
                {
                    if (value.number == number)
                    {
                        var diff = number * value.count;
                        sum += diff;
                    }
                }
            }

            return sum;
        }
    }
}
