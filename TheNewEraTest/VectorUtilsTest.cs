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
            const double ExpectedX = 3.8891;
            const double ExpectedY = 3.8891;

            // act
            Vector result = VectorUtils.GetVector(Length, Angle);

            // assert
            result.X.Should().BeApproximately(ExpectedX, 0.0001);
            result.Y.Should().BeApproximately(ExpectedY, 0.0001);
        }
    }
}