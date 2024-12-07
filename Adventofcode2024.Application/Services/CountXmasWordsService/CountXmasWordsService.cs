using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Application.Services.CountXmasWordsService
{
    public class CountXmasWordsService : ICountXmasWordsService
    {
        private readonly IFileReaderHelper _readerHelper;

        public CountXmasWordsService(IFileReaderHelper readerHelper)
        {
            _readerHelper = readerHelper;
        }
        public async Task<int> CountTotalXmasWords()
        {
            Func<string, char[]> valueMapper = line =>
            {
                var charArray = line.ToLower().ToCharArray();

                return charArray;
            };

            var ienumerableValues = await _readerHelper.MapFileToObjectAsync("day4.txt", valueMapper);

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

        public async Task<int> CountTotalMasWords() 
        {
            Func<string, char[]> valueMapper = line =>
            {
                var charArray = line.ToLower().ToCharArray();
                return charArray;
            };

            var ienumerableValues = await _readerHelper.MapFileToObjectAsync("Day4.txt", valueMapper);

            var arrayList = ienumerableValues.ToList();
            var sum = 0;

            /*
              M.S          (arrayList[i+1][j] == m || s) && (arrayList[i+1][j + 2] == m || s)
              .A.          arrayList[i][j + 1] == 'a'
              M.S          (arrayList[i-1][j] == m || s) && (arrayList[i-1][j + 2] == m || s)
            */

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
