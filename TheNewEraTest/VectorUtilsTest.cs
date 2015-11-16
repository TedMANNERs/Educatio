using System.Windows;
using FluentAssertions;
using NUnit.Framework;
using TheNewEra;

namespace TheNewEraTest
{
    [TestFixture]
    public class VectorUtilsTest
    {
        [Test]
        public void GetVector_WhenGetVector_ReturnsVector()
        {
            // arrange
            const double Length = 5.5;
            const double Angle = 45.0;
            const double ExpectedX = 3.8891 / VectorUtils.ScaleFactor;
            const double ExpectedY = 3.8891 / VectorUtils.ScaleFactor;

            // act
            Vector result = VectorUtils.GetScaledVector(Length, Angle);

            // assert
            result.X.Should().BeApproximately(ExpectedX, 0.0001);
            result.Y.Should().BeApproximately(ExpectedY, 0.0001);
        }

        [Test]
        public void GetCoordinates_WhenGetCoordinates_ThenReturnsCoordinates()
        {
            // arrange
            const double Length = 7.0;
            const double Angle = 30.0;
            Point expectedCoordinates = new Point(6.0622, 3.5);
            
            // act
            Point result = VectorUtils.GetCoordinates(Length, Angle);

            // assert
            result.X.Should().BeApproximately(expectedCoordinates.X, 0.0001);
            result.Y.Should().BeApproximately(expectedCoordinates.Y, 0.0001);
        }
    }
}