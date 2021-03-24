using FluentAssertions;
using Gamlo.ValidationApi.Core.Interfaces;
using Gamlo.ValidationApi.Core.Model;
using Gamlo.ValidationApi.Service;
using NUnit.Framework;

namespace Gamlo.ValidationApi.Test.Service
{
    public class RegexValidatorTests
    {
        [Test]
        public void IsValueValiTest()
        {
            // Arrange
            IValidator validator = new RegexValidator();
            var scheme = new SchemeModel("^\\d{3}$", "threeNumbers", "Regex");
            var value = new ValueModel("threeNumbers", "234");

            // Act
            var valid = validator.IsValueValid(scheme, value);

            // Assert
            valid.Should().BeTrue();
        }
    }
}
