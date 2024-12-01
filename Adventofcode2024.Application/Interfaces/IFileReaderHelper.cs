namespace Adventofcode2024.Application.Interfaces
{
    public interface IFileReaderHelper
    {
        Task<IEnumerable<T>> MapFileToObjectAsync<T>(string fileName, Func<string, T> mapFunction);
    }
}
