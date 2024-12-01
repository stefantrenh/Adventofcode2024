using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Infrastructure.Helpers;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventofcode2024.Tests.UnitTests.Day1
{
    public class TotalDistanceByExistingNumbersTest
    {
        protected string[] line;

        public TotalDistanceByExistingNumbersTest()
        {
            line = new[] { "3   4", "4  3", "2  5", "1   3", "3   9", "3   3" };
        }
    }

    public class Counting_Distance_With_Existing_Numbers_Test : TotalDistanceByExistingNumbersTest
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

            var numberdicitonary = new Dictionary<int, int>();

            for (int i = 0; i < distanceList1.Count; i++)
            {
                if (distanceList1[i] == distanceList2[i])
                {

                }
            }



            return sum;
        }
    }
}
