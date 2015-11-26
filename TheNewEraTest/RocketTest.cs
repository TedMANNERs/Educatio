using System.Windows;
using FluentAssertions;
using NUnit.Framework;
using TheNewEra;

namespace TheNewEraTest
{
    [TestFixture]
    public class RocketTest
    {
        [SetUp]
        public void SetUp()
        {
            _testee = new Rocket(_position, Height, Width) { FuelTank = new FuelTank(0)};
        }

        private Rocket _testee;
        private readonly Vector _position = new Vector(200, 200);
        private const int Height = 50;
        private const int Width = 89;

        [Test]
        public void DecreaseThrust_WhenThrustGreaterThanZero_ThenDecreasesThrust()
        {
            // arrange
            const double Thrust = 10;
            _testee.Thrust = Thrust;

            // act
            _testee.DecreaseThrust();

            // assert
            _testee.Thrust.Should().BeLessThan(Thrust);
        }

        [Test]
        public void DecreaseThrust_WhenThrustLessThanZero_ThenSetThrustToZero()
        {
            // arrange
            const double ExpectedThrust = 0;
            const double Thrust = -2;
            _testee.Thrust = Thrust;

            // act
            _testee.DecreaseThrust();

            // assert
            _testee.Thrust.Should().Be(ExpectedThrust);
        }

        [Test]
        public void IncreaseThrust_WhenHasFuelAndThrustNotMax_ThenIncreasesThrust()
        {
            // arrange
            const double Thrust = 0;
            _testee.FuelTank.RemainingFuel = 100;
            _testee.Thrust = Thrust;

            // act
            _testee.IncreaseThrust();

            // assert
            _testee.Thrust.Should().BeGreaterThan(Thrust);
        }

        [Test]
        public void RotateLeft_WhenHasFuel_ThenRotatesLeft()
        {
            // arrange
            const double RotationThrust = 0;
            _testee.FuelTank.RemainingFuel = 100;
            _testee.RotationSpeed = RotationThrust;

            // act
            _testee.RotateLeft();

            // assert
            _testee.RotationSpeed.Should().BeLessThan(RotationThrust);
        }

        [Test]
        public void RotateRight_WhenHasFuel_ThenRotatesRight()
        {
            // arrange
            const double RotationThrust = 0;
            _testee.FuelTank.RemainingFuel = 100;
            _testee.RotationSpeed = RotationThrust;

            // act
            _testee.RotateRight();

            // assert
            _testee.RotationSpeed.Should().BeGreaterThan(RotationThrust);
        }
    }
}