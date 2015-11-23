using System;
using FluentAssertions;
using NUnit.Framework;
using TheNewEra;

namespace TheNewEraTest
{
    [TestFixture]
    public class AngleUtilsTest
    {
        [Test]
        public void LimitAngle_WhenGreaterThan360_ThenReturnsCorrectAngle()
        {
            // arrange
            const double Angle = 3 * Math.PI;
            const double ExpectedAngle = Math.PI;

            // act
            double result = AngleUtils.LimitAngle(Angle);

            // assert
            result.Should().Be(ExpectedAngle);
        }

        [Test]
        public void LimitAngle_WhenLessThanZero_ThenReturnsCorrectAngle()
        {
            // arrange
            const double Angle = -Math.PI / 2;
            const double ExpectedAngle = Math.PI * 1.5;

            // act
            double result = AngleUtils.LimitAngle(Angle);

            // assert
            result.Should().Be(ExpectedAngle);
        }

        [Test]
        public void LimitAngle_WhenLimitAngle_ThenReturnsSameAngle()
        {
            // arrange
            const double Angle = Math.PI * 1.5;
            const double ExpectedAngle = Math.PI * 1.5;

            // act
            double result = AngleUtils.LimitAngle(Angle);

            // assert
            result.Should().Be(ExpectedAngle);
        }

        [Test]
        public void ConvertToRadians_WhenConvertToRadians_ThenReturnAngleInRadians()
        {
            // arrange
            const double Angle = 180;
            const double ExpectedAngle = Math.PI;

            // act
            double result = AngleUtils.ConvertToRadians(Angle);

            // assert
            result.Should().Be(ExpectedAngle);
        }

        [Test]
        public void ConvertToDegrees_WhenConvertToDegrees_ThenReturnAngleInDegrees()
        {
            // arrange
            const double Angle = Math.PI;
            const double ExpectedAngle = 180;

            // act
            double result = AngleUtils.ConvertToDegrees(Angle);

            // assert
            result.Should().Be(ExpectedAngle);
        }
    }
}