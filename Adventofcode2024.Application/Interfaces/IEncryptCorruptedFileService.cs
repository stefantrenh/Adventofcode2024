namespace Adventofcode2024.Application.Interfaces
{
    public interface IEncryptCorruptedFileService
    {
        Task<int> CalculateEncryptedFile();

        Task<int> CalculateEncryptedFileByValidNumbers();
    }
}
