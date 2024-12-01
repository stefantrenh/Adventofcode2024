using Moq;
using Adventofcode2024.Application.Interfaces;
using Adventofcode2024.Infrastructure.Helpers;
using FluentAssertions;

namespace Adventofcode2024.Tests.UnitTests.HelperTest
{
    public class ObjectsMappingByFileTest
    {
        protected string[] line;
        public ObjectsMappingByFileTest()
        {
            line = new[] { "Stefan;Trenh", "Louis;Headlam" };
        }
    }

    public class MappingObjectByStringTest : ObjectsMappingByFileTest
    {
        [Fact]
        public async Task Should_Be_Equvalent_To_Object()
        {

            var persons = new List<Person> 
            {
                new Person("Stefan", "Trenh"),
                new Person("Louis", "Headlam")
            };

            var mockFileReader = new Mock<IFileReader>();
            var mockFilePathHelper = new Mock<IFilePathHelper>();

            var mockFilePath = @"C:\path\to\mock\file.txt";


            mockFilePathHelper.Setup(fp => fp.GetFullPathToFile(It.IsAny<string>()))
                              .Returns(mockFilePath);

            mockFileReader.Setup(fr => fr.ReadAllLinesAsync(It.Is<string>(path => path == mockFilePath)))
                          .ReturnsAsync(line);

            var fileReaderHelper = new FileReaderHelper(mockFileReader.Object, mockFilePathHelper.Object);


            Func<string, Person> personMapper = line =>
            {
                var split = line.Split(';');
                return new Person(split[0], split[1]);
            };

            var result = await fileReaderHelper.MapFileToObjectAsync("file.txt", personMapper);

            var personlist = result.ToList();

            personlist.Should().BeEquivalentTo(persons);
        }

        private class Person
        {
            public string Name { get; }
            public string LastName { get; }

            public Person(string name, string lastName)
            {
                Name = name;
                LastName = lastName;
            }
        }
    }


}
