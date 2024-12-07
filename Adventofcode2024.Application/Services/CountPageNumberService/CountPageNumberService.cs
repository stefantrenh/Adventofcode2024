using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Application.Services.CountPageNumberService
{
    public class CountPageNumberService : ICountPageNumberService
    {
        public readonly IFileReaderHelper fileReaderHelper;

        public CountPageNumberService(IFileReaderHelper fileReaderHelper)
        {
            this.fileReaderHelper = fileReaderHelper;
        }

        public async Task<int> CountMiddleNumberOfPagesAsync()
        {
            Func<string, (int x, int y)> pageNumber = lines =>
            {
                if (lines.Contains('|'))
                {
                    var parts = lines.Split('|');

                    if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
                    {
                        return (x, y);
                    }
                    else
                    {
                        throw new FormatException($"Invalid format {lines}");
                    }
                }
                return (0, 0);
            };

            Func<string, List<int>> pageNumberOrderingRules = lines =>
            {
                var orderingList = new List<int>();
                if (lines.Contains(','))
                {
                    var parts = lines.Split(',');

                    foreach (var number in parts)
                    {
                        if (int.TryParse(number, out int value))
                        {
                            orderingList.Add(value);
                        }
                        else
                        {
                            throw new FormatException($"Invalid format {lines}");
                        }
                    }

                    return orderingList;
                }

                return orderingList;
            };

            var ienumarblePagenumber = await fileReaderHelper.MapFileToObjectAsync("Day5.txt", pageNumber);
            var ienumarbleOrderingRules = await fileReaderHelper.MapFileToObjectAsync("Day5.txt", pageNumberOrderingRules);

            var pageNumberList = ienumarblePagenumber.ToList();
            var orderingPageNumberList = ienumarbleOrderingRules.ToList();

            var validOrderNumblerList = new List<List<int>>();

            foreach (var numberList in orderingPageNumberList) // 75,97,47,61,53
            {
                bool isValid = true;

                foreach (var rule in pageNumberList) //  97|75 - (error code)
                {
                    if (numberList.Contains(rule.x) && numberList.Contains(rule.y))
                    {
                        if (numberList.IndexOf(rule.x) > numberList.IndexOf(rule.y))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    validOrderNumblerList.Add(numberList);
                }
            }

            var sum = 0;


            foreach (var numberList in validOrderNumblerList)
            {
                if (numberList.Any())
                {
                    int middleIndex = numberList.Count / 2;
                    sum += numberList[middleIndex];
                }
            }

            return sum;
        }
    }
}
