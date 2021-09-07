using CSValidator;
using System.IO;
using System.Reflection;
using Xunit;

namespace ValidatorTest
{
    public class ValidatorTest
    {
        private readonly string _directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        [Fact]
        public void TestValidator()
        {
            string pathss = Path.Combine(_directory, "PrivacyTest.csv");
            Validator validated = new(pathss);
            Assert.Equal("3300", validated.Budget("Employee1"));
        }

        [Fact]
        public void TestValidatorInvalidFile()
        {
            string pathss = Path.Combine(_directory, "PrivacyTestInvalid.csv");
            Validator validated = new(pathss);
            Assert.Equal("Invalid CSV file!", validated.Budget("Employee1"));
        }

        [Fact]
        public void TestValidatorInvalidPath()
        {
            Validator validated = new("ps");
            Assert.Equal("Invalid CSV file!", validated.Budget("Employee1"));
        }
    }
}
