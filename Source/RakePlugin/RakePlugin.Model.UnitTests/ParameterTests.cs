namespace RakePlugin.Model.UnitTests
{
    using NUnit.Framework;
    using RakePlugin.Model;

    [TestFixture]
    public class ParameterTests
    {
        [Test(Description = "Возвращение корректного значения процесса Компас.")]
        public void Kompas_ReturnCorrectValue_ReturnsKompasProcess()
        {
            // Setup
            var expectedResult = "kStudy";

            // Act
            var actualResult = CAD.Kompas;

            // Assert
            Assert.That(expectedResult == actualResult);
        }

        [Test(Description = "Возвращение корректного значения процесса Солид Воркс.")]
        public void SolidWorks_ReturnCorrectValue_ReturnsSolidWorksProcess()
        {
            // Setup
            var expectedResult = "SLDWORKS";

            // Act
            var actualResult = CAD.SolidWorks;

            // Assert
            Assert.That(actualResult == expectedResult);
        }
    }
}
