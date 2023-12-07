namespace RakePlugin.Model.UnitTests
{
    using NUnit.Framework;
    using RakePlugin.Model;

    [TestFixture]
    public class ValidatorTests
    {
        [Test(Description = "Ввод некорректного значения.")]
        public void Validate_InputIncorrectValue_ReturnsFalse()
        {
            // Setup
            var minValue = 10;
            var maxValue = 15;
            var inputValue = 20;
            Parameter parameter = new Parameter();
            parameter.Value = inputValue;
            parameter.MinValue = minValue;
            parameter.MaxValue = maxValue;
            var expectedResult = false;

            // Act
            var actualResult = Validator.Validate(parameter);

            // Assert
            Assert.That(expectedResult == actualResult);
        }

        [Test(Description = "Ввод корректного значения.")]
        public void Validate_InputCorrectValue_ReturnTrue()
        {
            // Setup
            var minValue = 10;
            var maxValue = 15;
            var inputValue = 13;
            Parameter parameter = new Parameter();
            parameter.Value = inputValue;
            parameter.MinValue = minValue;
            parameter.MaxValue = maxValue;
            var expectedResult = true;

            // Act
            var actualResult = Validator.Validate(parameter);

            // Assert
            Assert.That(actualResult == expectedResult);
        }
    }
}