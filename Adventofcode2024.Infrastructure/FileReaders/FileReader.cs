

using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Infrastructure.FileReaders
{
    public class FileReader : IFileReader
    {
        public async Task<string[]> ReadAllLinesAsync(string filepath)
        {
            return await File.ReadAllLinesAsync(filepath);
        }
    }
}
