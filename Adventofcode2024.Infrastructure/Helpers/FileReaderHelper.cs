using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Infrastructure.Helpers
{
    public class FileReaderHelper : IFileReaderHelper
    {
        private readonly IFileReader _fileReader;
        private readonly IFilePathHelper _filePathHelper;

        public FileReaderHelper(IFileReader fileReader, IFilePathHelper filePathHelper)
        {
            _fileReader = fileReader;
            _filePathHelper = filePathHelper;
        }

        public async Task<IEnumerable<T>> MapFileToObjectAsync<T>(string fileName, Func<string, T> mapFunction)
        { 
            string filepath = _filePathHelper.GetFullPathToFile(fileName);
            var lines = await _fileReader.ReadAllLinesAsync(filepath);

            return lines.Select(mapFunction);
        }
    }
}
