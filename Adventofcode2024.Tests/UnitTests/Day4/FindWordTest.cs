using Adventofcode2024.Infrastructure.Helpers;
using Adventofcode2024.TestHelpers;
using FluentAssertions;

namespace Adventofcode2024.Tests.UnitTests.Day4
{
    /*
 --- Day 4: Ceres Search ---

     "Looks like the Chief's not here. Next!" One of The Historians pulls out a device and pushes the only button on it. After a brief flash, you recognize the interior of the Ceres monitoring station!
     As the search for the Chief continues, a small Elf who lives on the station tugs on your shirt; she'd like to know if you could help her with her word search (your puzzle input). She only has to find one word: XMAS.
     This word search allows words to be horizontal, vertical, diagonal, written backwards, or even overlapping other words. It's a little unusual, though, as you don't merely need to find one instance of XMAS - you need to find all of them. Here are a few ways XMAS might appear, where irrelevant characters have been replaced with .:
     
     ..X...
     .SAMX.
     .A..A.
     XMAS.S
     .X....
     
     The actual word search will be full of letters instead. For example:
     
     MMMSXXMASM
     MSAMXMSMSA
     AMXSXMAAMM
     MSAMASMSMX
     XMASAMXAMM
     XXAMMXXAMA
     SMSMSASXSS
     SAXAMASAAA
     MAMMMXMMMM
     MXMXAXMASX
     
     In this word search, XMAS occurs a total of 18 times; here's the same word search again, but where letters not involved in any XMAS have been replaced with .:
     
     ....XXMAS.
     .SAMXMS...
     ...S..A...
     ..A.A.MS.X
     XMASAMX.MM
     X.....XA.A
     S.S.S.S.SS
     .A.A.A.A.A
     ..M.M.M.MM
     .X.X.XMASX
     
     Take a look at the little Elf's word search. How many times does XMAS appear?
     
      */

    public class FindWordTest
    {
        protected string[] line;
        protected string[] line2;
        protected MockFileReaderHelper mockFileReader;
        protected MockFileReaderHelper mockFileReader2;

        public FindWordTest()
        {
            line = new string[]
            {
                "MMMSXXMASM",
                "MSAMXMSMSA",
                "AMXSXMAAMM",
                "MSAMASMSMX",
                "XMASAMXAMM",
                "XXAMMXXAMA",
                "SMSMSASXSS",
                "SAXAMASAAA",
                "MAMMMXMMMM",
                "MXMXAXMASX"
            };

            line2 = new string[]
            {
                ".M.S......",
                "..A..MSMS.",
                ".M.S.MAA..",
                "..A.ASMSM.",
                ".M.S.M....",
                "..........",
                "S.S.S.S.S.",
                ".A.A.A.A..",
                "M.M.M.M.M.",
                "..........",
            };

            mockFileReader = new MockFileReaderHelper(line);
            mockFileReader2 = new MockFileReaderHelper(line2);
        }
    }

    public class FindXmasWordTest : FindWordTest
    {
        [Fact]
        public async void Should_Return_18()
        {
            var filereader = mockFileReader.CreateMockFileReaderHelper();

            var result = await FindXmasData(filereader);

            result.Should().Be(18);
        }

        private async Task<int> FindXmasData(FileReaderHelper fileReaderHelper)
        {
            Func<string, char[]> valueMapper = line =>
            {
                var charArray = line.ToLower().ToCharArray();

                return charArray;
            };

            var ienumerableValues = await fileReaderHelper.MapFileToObjectAsync("temp", valueMapper);

            var arrayList = ienumerableValues.ToList();
            var sum = 0;

            for (int i = 0; i < arrayList.Count; i++)
            {
                var rowArray = arrayList[i];
                for (int j = 0; j < rowArray.Length; j++)
                {
                    // Horizontal -
                    if (j <= rowArray.Length - 4 &&
                        rowArray[j] == 'x' &&
                        rowArray[j + 1] == 'm' &&
                        rowArray[j + 2] == 'a' &&
                        rowArray[j + 3] == 's')
                    {
                        sum++;
                    }


                    if (j <= rowArray.Length - 4 &&
                        rowArray[j] == 's' &&
                        rowArray[j + 1] == 'a' &&
                        rowArray[j + 2] == 'm' &&
                        rowArray[j + 3] == 'x')
                    {
                        sum++;
                    }

                    // Vertical |
                    if (i <= arrayList.Count - 4 &&
                        arrayList[i][j] == 'x' &&
                        arrayList[i + 1][j] == 'm' &&
                        arrayList[i + 2][j] == 'a' &&
                        arrayList[i + 3][j] == 's')
                    {
                        sum++;
                    }

                    if (i <= arrayList.Count - 4 &&
                        arrayList[i][j] == 's' &&
                        arrayList[i + 1][j] == 'a' &&
                        arrayList[i + 2][j] == 'm' &&
                        arrayList[i + 3][j] == 'x')
                    {
                        sum++;
                    }

                    //Diagonal /
                    if (i <= arrayList.Count - 4 && j <= rowArray.Length - 4 &&
                        arrayList[i][j] == 'x' &&
                        arrayList[i + 1][j + 1] == 'm' &&
                        arrayList[i + 2][j + 2] == 'a' &&
                        arrayList[i + 3][j + 3] == 's')
                    {
                        sum++;
                    }

                    if (i <= arrayList.Count - 4 && j <= rowArray.Length - 4 &&
                        arrayList[i][j] == 's' &&
                        arrayList[i + 1][j + 1] == 'a' &&
                        arrayList[i + 2][j + 2] == 'm' &&
                        arrayList[i + 3][j + 3] == 'x')
                    {
                        sum++;
                    }

                    // Diagonal \
                    if (i <= arrayList.Count - 4 && j >= 3 &&
                        arrayList[i][j] == 'x' &&
                        arrayList[i + 1][j - 1] == 'm' &&
                        arrayList[i + 2][j - 2] == 'a' &&
                        arrayList[i + 3][j - 3] == 's')
                    {
                        sum++;
                    }

                    if (i <= arrayList.Count - 4 && j >= 3 &&
                        arrayList[i][j] == 's' &&
                        arrayList[i + 1][j - 1] == 'a' &&
                        arrayList[i + 2][j - 2] == 'm' &&
                        arrayList[i + 3][j - 3] == 'x')
                    {
                        sum++;
                    }
                }

            }

            return sum;
        }
    }

    public class FindMasWordTest : FindWordTest
    {
        [Fact]
        public async void Should_Return_9()
        {
            var filereader = mockFileReader2.CreateMockFileReaderHelper();

            var result = await FindMasData(filereader);

            result.Should().Be(9);
        }

        private async Task<int> FindMasData(FileReaderHelper filereader) 
        {
            Func<string, char[]> valueMapper = line =>
            {
                var charArray = line.ToLower().ToCharArray();
                return charArray;
            };

            var ienumerableValues = await filereader.MapFileToObjectAsync("temp", valueMapper);

            var arrayList = ienumerableValues.ToList();
            var sum = 0;

            for (int i = 0; i < arrayList.Count; i++)
            {
                var rowArray = arrayList[i];
                for (int j = 0; j < rowArray.Length; j++)
                {
                    if (i > 0 && i < arrayList.Count - 1 && j < rowArray.Length - 2)
                    {
                        bool top = (arrayList[i + 1][j] == 'm' || arrayList[i + 1][j] == 's') &&
                                   (arrayList[i + 1][j + 2] == 'm' || arrayList[i + 1][j + 2] == 's');
                        bool center = arrayList[i][j + 1] == 'a';
                        bool bottom = (arrayList[i - 1][j] == 'm' || arrayList[i - 1][j] == 's') &&
                                      (arrayList[i - 1][j + 2] == 'm' || arrayList[i - 1][j + 2] == 's');

                        if (top && center && bottom)
                        {
                            /*
                               M.S          
                               .A.          
                               M.S          
                            */
                            if (arrayList[i + 1][j] == 'm' && arrayList[i + 1][j + 2] == 's' && arrayList[i - 1][j] == 'm' && arrayList[i - 1][j + 2] == 's')
                            {
                                sum++;
                            }

                            /*
                               M.M          
                               .A.          
                               S.S          
                            */
                            if (arrayList[i + 1][j] == 'm' && arrayList[i + 1][j + 2] == 'm' && arrayList[i - 1][j] == 's' && arrayList[i - 1][j + 2] == 's')
                            {
                                sum++;
                            }
                            /*
                                S.M          
                                .A.          
                                S.M          
                            */
                            if (arrayList[i + 1][j] == 's' && arrayList[i + 1][j + 2] == 'm' && arrayList[i - 1][j] == 's' && arrayList[i - 1][j + 2] == 'm')
                            {
                                sum++;
                            }
                            /*
                                S.S          
                                .A.          
                                M.M         
                            */
                            if (arrayList[i + 1][j] == 's' && arrayList[i + 1][j + 2] == 's' && arrayList[i - 1][j] == 'm' && arrayList[i - 1][j + 2] == 'm')
                            {
                                sum++;
                            }
                        }
                    }
                }
            }

            return sum;
        }
    }
}
