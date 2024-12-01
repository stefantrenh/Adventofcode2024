namespace Adventofcode2024.Application.Interfaces
{
    public interface IDistanceCalculatorService
    {
        Task<int> CalculateDistanceByRangeAsync();
        Task<int> CalculateDistanceBySameNumberAsync();

    }
}
