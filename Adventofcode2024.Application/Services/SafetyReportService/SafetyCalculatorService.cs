using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Application.Services.SafetyReportService
{
    public class SafetyCalculatorService : ISafetyCalculatorService
    {
        private readonly IFileReaderHelper _fileReaderHelper;

        public SafetyCalculatorService(IFileReaderHelper fileReaderHelper)
        {
            _fileReaderHelper = fileReaderHelper;
        }

        public async Task<int> CalculateSafetyWithDampenerAsync()
        {
            Func<string, List<int>> valueMapper = line =>
            {
                var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var partsValue = new List<int>();

                foreach (var part in parts)
                {
                    partsValue.Add(int.Parse(part));
                }

                return partsValue;
            };

            var ienumarbleDataList = await _fileReaderHelper.MapFileToObjectAsync("Day2.txt", valueMapper);
            var datalist = ienumarbleDataList.ToList();

            var safetyLevelCount = 0;

            foreach (var data in datalist)
            {
                bool isSafe = IsSafe(data);

                if (!isSafe)
                {
                    isSafe = CanBeSafeByRemovingOneNumber(data);
                }

                if (isSafe)
                {
                    safetyLevelCount++;
                }
            }

            return safetyLevelCount;
        }

        public async Task<int> CalculateSafetyByRangeAsync()
        {
            Func<string, List<int>> valueMapper = line =>
            {
                var parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var partsValue = new List<int>();

                try
                {
                    foreach (var part in parts)
                    {
                        partsValue.Add(int.Parse(part));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

                return partsValue;
            };

            var ienumarbleDataList = await _fileReaderHelper.MapFileToObjectAsync("Day2.txt", valueMapper);

            var datalist = ienumarbleDataList.ToList();

            var safetyLevelCount = 0;

            for (int i = 0; i < datalist.Count; i++)
            {
                var isIncreasing = datalist[i].Zip(datalist[i].Skip(1), (current, next) =>
                {
                    return current < next && (Math.Abs(next - current) <= 3);
                }).All(x => x);


                var isDecreasing = datalist[i].Zip(datalist[i].Skip(1), (current, next) =>
                {
                    return current > next && (Math.Abs(next - current) <= 3);
                }).All(x => x);

                if (isIncreasing || isDecreasing)
                {
                    safetyLevelCount++;
                }
            }

            return safetyLevelCount;
        }

        private bool IsSafe(List<int> data)
        {
            var isIncreasing = data.Zip(data.Skip(1), (current, next) =>
            {
                return current < next && (Math.Abs(next - current) <= 3);
            }).All(x => x);

            var isDecreasing = data.Zip(data.Skip(1), (current, next) =>
            {
                return current > next && (Math.Abs(next - current) <= 3);
            }).All(x => x);

            return isIncreasing || isDecreasing;
        }

        private bool CanBeSafeByRemovingOneNumber(List<int> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var newData = new List<int>(data);
                newData.RemoveAt(i);

                if (IsSafe(newData))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
