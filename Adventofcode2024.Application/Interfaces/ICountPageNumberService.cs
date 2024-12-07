namespace Adventofcode2024.Application.Interfaces
{
    public interface ICountPageNumberService
    {
        Task<int> CountMiddleNumberOfPagesAsync();
    }
}
