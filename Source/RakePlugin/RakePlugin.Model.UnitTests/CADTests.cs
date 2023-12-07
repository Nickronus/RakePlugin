namespace RakePlugin.Model.UnitTests
{
    using NUnit.Framework;
    using RakePlugin.Model;

    [TestFixture]
    public class CADTests
    {
        [Test(Description = "Возвращение значения параметра.")]
        public void ParameterValue_ReturnValue_ReturnsParameterValue()
        {
            // Setup
            var parameter =new Parameter();
            parameter.Value = 0;
            var expectedResult = 0;

            // Act
            var actualResult = parameter.Value;

            // Assert
            Assert.That(expectedResult == actualResult);
        }

        [Test(Description = "Возвращение минимального значения параметра.")]
        public void ParameterMinValue_ReturnMinValue_ReturnsParameterMinValue()
        {
            // Setup
            var parameter = new Parameter();
            parameter.MinValue = 0;
            var expectedResult = 0;

            // Act
            var actualResult = parameter.MinValue;

            // Assert
            Assert.That(expectedResult == actualResult);
        }

        [Test(Description = "Возвращение максимального значения параметра.")]
        public void ParameterMaxValue_ReturnMaxValue_ReturnsParameterMaxValue()
        {
            // Setup
            var parameter = new Parameter();
            parameter.MaxValue = 0;
            var expectedResult = 0;

            // Act
            var actualResult = parameter.MaxValue;

            // Assert
            Assert.That(expectedResult == actualResult);
        }
    }
}
