using System;
using System.Windows;
using FluentAssertions;
using NUnit.Framework;
using TheNewEra.Objects;
using TheNewEra.Objects.Rocket;
using TheNewEra.Util;

namespace TheNewEraTest.Util
{
    [TestFixture]
    public class VectorUtilsTest
    {
        [Test]
        public void GetVector_WhenGetVector_ReturnsVector()
        {
            // arrange
            const double Length = 5.5;
            const double Angle = Math.PI / 4;
            const double ExpectedX = 3.8891;
            const double ExpectedY = 3.8891;

            // act
            Vector result = VectorUtils.GetVector(Length, Angle);

            // assert
            result.X.Should().BeApproximately(ExpectedX, 0.0001);
            result.Y.Should().BeApproximately(ExpectedY, 0.0001);
        }

        [Test]
        public void GetDistance_WhenGetDistance_ReturnsDistanceBetweenObjects()
        {
            // arrange
            IMoveableObject objectA = new Rocket(new Vector(200, 0), 50, 50);
            IMoveableObject objectB = new Meteoroid(new Vector(500, 0), 50, 50);
            const double ExpectedDistance = 300.0;

            // act
            double result = VectorUtils.GetDistance(objectA, objectB);

            // assert
            result.Should().Be(ExpectedDistance);
        }
    }
}