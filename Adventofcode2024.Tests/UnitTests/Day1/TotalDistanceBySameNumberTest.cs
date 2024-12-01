using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Infrastructure.Helpers;
using FluentAssertions;
using Moq;

namespace Adventofcode2024.Tests.UnitTests.Day1
{

    /*
     Your analysis only confirmed what everyone feared: the two lists of location IDs are indeed very different.

        Or are they?
        
        The Historians can't agree on which group made the mistakes or how to read most of the Chief's handwriting, but in the commotion you notice an interesting detail: a lot of location IDs appear in both lists! Maybe the other numbers aren't location IDs at all but rather misinterpreted handwriting.
        
        This time, you'll need to figure out exactly how often each number from the left list appears in the right list. Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.
        
        Here are the same example lists again:
        
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        
        For these example lists, here is the process of finding the similarity score:
        
            The first number in the left list is 3. It appears in the right list three times, so the similarity score increases by 3 * 3 = 9.
            The second number in the left list is 4. It appears in the right list once, so the similarity score increases by 4 * 1 = 4.
            The third number in the left list is 2. It does not appear in the right list, so the similarity score does not increase (2 * 0 = 0).
            The fourth number, 1, also does not appear in the right list.
            The fifth number, 3, appears in the right list three times; the similarity score increases by 9.
            The last number, 3, appears in the right list three times; the similarity score again increases by 9.
        
        So, for these example lists, the similarity score at the end of this process is 31 (9 + 4 + 0 + 0 + 9 + 9).
        
        Once again consider your left and right lists. What is their similarity score?
     */
    public class TotalDistanceBySameNumberTest
    {
        protected string[] line;

        public TotalDistanceBySameNumberTest()
        {
            line = new[] { "3   4", "4  3", "2  5", "1   3", "3   9", "3   3" };
        }
    }

    public class Counting_Distance_By_Same_Number_Test : TotalDistanceBySameNumberTest
    {
        [Fact]
        public async Task Should_Be_Equivalent_To_31()
        {

            var mockFileReader = new Mock<IFileReader>();
            var mockFilePathHelper = new Mock<IFilePathHelper>();

            var mockFilePath = @"C:\path\to\mock\file.txt";
            mockFilePathHelper.Setup(fp => fp.GetFullPathToFile(It.IsAny<string>()))
                              .Returns(mockFilePath);


            mockFileReader.Setup(fr => fr.ReadAllLinesAsync(mockFilePath))
                          .ReturnsAsync(line);


            var fileReaderHelper = new FileReaderHelper(mockFileReader.Object, mockFilePathHelper.Object);

            var result = await ReturnCalculatedDistance(fileReaderHelper);

            result.Should().Be(31);
        }

        private async Task<int> ReturnCalculatedDistance(FileReaderHelper fileReaderHelper)
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

                    return (int.Parse(parts[0]), int.Parse(parts[1]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");

                    return (0, 0);
                }
            };

            var IenumarbleValues = await fileReaderHelper.MapFileToObjectAsync("tempPath", valueMapper);

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

            var validNumbersAndCountTuple = new List<(int number,int count)>();

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
