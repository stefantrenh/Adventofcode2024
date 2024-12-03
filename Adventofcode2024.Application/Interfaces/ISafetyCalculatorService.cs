namespace Adventofcode2024.Application.Interfaces
{
    public interface ISafetyCalculatorService
    {
        Task<int> CalculateSafetyByRangeAsync();
        Task<int> CalculateSafetyWithDampenerAsync();
    }
}
