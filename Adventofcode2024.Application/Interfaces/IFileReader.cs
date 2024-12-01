namespace Adventofcode2024.Application.Interfaces
{
    public interface IFileReader
    {
        Task<string[]> ReadAllLinesAsync(string filepath);
    }
}
