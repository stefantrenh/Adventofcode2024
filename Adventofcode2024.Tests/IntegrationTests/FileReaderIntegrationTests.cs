using Adventofcode2024.Infrastructure.FileReaders;
using Adventofcode2024.Infrastructure.Helpers;
using FluentAssertions;

namespace Adventofcode2024.Tests.IntegrationTests
{
    public class FileReaderIntegrationTests : IDisposable
    {
        private readonly string _testFilePath;
        private readonly FileReaderHelper _helper;


        public FileReaderIntegrationTests()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), "testFile.txt");
            File.WriteAllLines(_testFilePath, new[] { "Stefan;Trenh", "Louis;Headlam" });
            var fileReader = new FileReader();
            var filePath = new FilePathHelper();
            _helper = new FileReaderHelper(fileReader, filePath);
        }

        [Fact]
        public async Task MapFileToObjectAsync_Should_Return_Mapped_Objects_When_File_Exists()
        {
            Func<string, Person> personMapper = line =>
            {
                var parts = line.Split(';');
                return new Person(parts[0], parts[1]);
            };

            var result = await _helper.MapFileToObjectAsync(_testFilePath, personMapper);

            var resultlist = result.ToList();

            resultlist.Should().HaveCount(2);
            resultlist[0].Should().BeEquivalentTo(new Person("Stefan", "Trenh"));
            resultlist[1].Should().BeEquivalentTo(new Person("Louis", "Headlam"));
           
        }

        [Fact]
        public async void MapFileToObjectAsync_Should_Throw_ArgumentNullException_WhenFilePathIsNull()
        {
            string nullFilePath = null;

            Func<string, Person> personMapper = lines =>
            {
                var parts = lines.Split(";");
                return new Person(parts[0], parts[1]);
            };

            Func<Task> act = async () => await _helper.MapFileToObjectAsync(nullFilePath, personMapper);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        private class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }

            public Person(string name, string lastName)
            {
                Name = name;
                LastName = lastName;
            }
        }
    }
}
