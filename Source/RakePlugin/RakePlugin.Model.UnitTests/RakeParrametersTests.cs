namespace RakePlugin.Model.UnitTests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using RakePlugin.Model;

    [TestFixture]
    public class RakeParametersTests
    {
        [Test(Description = "Возвращение словаря параметров.")]
        public void RakeParameters_ReturnDictionary_ReturnsRakeParametersDictionary()
        {
            // Setup
            RakeParameters parameters = new RakeParameters();
            Parameter parameter = new Parameter();
            ParameterType parameterType = new ParameterType();
            Dictionary<ParameterType, Parameter> dictionary;
            dictionary = new Dictionary<ParameterType, Parameter>
            {
                { parameterType, parameter }
            };
            parameters.Parameters = dictionary;
            var expectedResult = dictionary;

            // Act
            var actualResult = parameters.Parameters;

            // Assert
            Assert.That(expectedResult == actualResult);
        }
    }
}
