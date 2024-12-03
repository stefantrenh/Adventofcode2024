using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Infrastructure.Helpers;
using Moq;

namespace Adventofcode2024.TestHelpers
{
    public class MockFileReaderHelper
    {
        private string[] line;

        public MockFileReaderHelper(string[] line)
        {
            this.line = line;
        }

        public FileReaderHelper CreateMockFileReaderHelper()
        {
            var mockFileReader = new Mock<IFileReader>();
            var mockFilePathHelper = new Mock<IFilePathHelper>();

            var mockFilePath = @"C:\path\to\mock\file.txt";
            mockFilePathHelper.Setup(fp => fp.GetFullPathToFile(It.IsAny<string>()))
                              .Returns(mockFilePath);


            mockFileReader.Setup(fr => fr.ReadAllLinesAsync(mockFilePath))
                          .ReturnsAsync(line);


            var fileReaderHelper = new FileReaderHelper(mockFileReader.Object, mockFilePathHelper.Object);

            return fileReaderHelper;
        }
    }
}
