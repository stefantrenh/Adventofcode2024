using Adventofcode2024.Application.Interfaces;

namespace Adventofcode2024.Infrastructure.Helpers
{
    public class FilePathHelper : IFilePathHelper
    {
        private readonly string _baseDirectory;

        public FilePathHelper()
        {
            _baseDirectory = AppContext.BaseDirectory;
        }

        private string GetProjectRoot()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(_baseDirectory, "..", "..", "..", ".."));
            return projectRoot;
        }

        public string GetFullPathToFile(string filename)
        {
            string projectRoot = GetProjectRoot();
            return Path.Combine(projectRoot, "Adventofcode2024.Infrastructure", "Files", "Data", filename);
        }
    }
}
