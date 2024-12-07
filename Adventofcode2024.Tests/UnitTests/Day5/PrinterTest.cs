using Adventofcode2024.Infrastructure.Helpers;
using Adventofcode2024.TestHelpers;
using FluentAssertions;

namespace Adventofcode2024.Tests.UnitTests.Day5
{
    public class PrinterTest
    {
        protected string[] lines;
        protected MockFileReaderHelper mockFileReaderHelper;
        public PrinterTest()
        {
            lines = new string[] 
            { 
                "47|53",
                "97|13",
                "97|61",
                "97|47",
                "75|29",
                "61|13",
                "75|53",
                "29|13",
                "97|29",
                "53|29",
                "61|53",
                "97|53",
                "61|29",
                "47|13",
                "75|47",
                "97|75",
                "47|61",
                "75|61",
                "47|29",
                "75|13",
                "53|13",
                
                "75,47,61,53,29",
                "97,61,53,29,13",
                "75,29,13",
                "75,97,47,61,53",
                "61,13,29",
                "97,13,75,29,47"    
            };

            mockFileReaderHelper = new MockFileReaderHelper(lines);
        }
    }

    public class GetPageOrderingNumberTest : PrinterTest
    {
        [Fact]
        public async void Result_Should_Be_143()
        {
            var fileReader = mockFileReaderHelper.CreateMockFileReaderHelper();

            var result = await SortPagesOnPrinter(fileReader);

            result.Should().Be(143);
        }

        private async Task<int> SortPagesOnPrinter(FileReaderHelper fileReader)
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

            var ienumarblePagenumber = await fileReader.MapFileToObjectAsync("tempPath" ,pageNumber);
            var ienumarbleOrderingRules = await fileReader.MapFileToObjectAsync("tempPath" , pageNumberOrderingRules);

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
