namespace Adventofcode2024.Application.Interfaces
{
    public interface ICountXmasWordsService
    {
        Task<int> CountTotalXmasWords();
        Task<int> CountTotalMasWords();
    }
}
