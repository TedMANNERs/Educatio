using System.Windows;

namespace TheNewEra
{
    public class Rocket : MoveableObjectBase
    {
        private int _imageId = 1;
        private double _positionAngle;

        public Rocket(Vector position, int height, int width)
        {
            Height = height;
            Width = width;
            Position = position;
            Sprite = "Resources/Images/rocket1.png";
            Init();
        }

        public IFuelTank FuelTank { get; set; }

        public double PositionAngle
        {
            get { return _positionAngle; }
            set
            {
                _positionAngle = value;
                OnPropertyChanged();
            }
        }

        public override void Update()
        {
            base.Update();
            if (FuelTank.RemainingFuel <= 0 || ThrustMovement.Length <= 0)
            {
                Sprite = "Resources/Images/rocket3.png";
            }
            else
            {
                if (_imageId > 2)
                {
                    _imageId = 1;
                }
                Sprite = "Resources/Images/rocket" + _imageId + ".png";
                _imageId++;
                FuelTank.RemainingFuel--;
            }

            Vector navigatorCenter = new Vector(50, 50);
            Vector rocketPosition = Vector.Subtract(new Vector(Position.X, Position.Y), navigatorCenter);
            Vector navigatorXAxis = Vector.Subtract(new Vector(100, 50), navigatorCenter);
            PositionAngle = AngleUtils.ConvertToRadians(Vector.AngleBetween(rocketPosition, navigatorXAxis));
        }

        public void IncreaseThrust()
        {
            if (FuelTank.RemainingFuel > 0 && Thrust < 0.5)
            {
                Thrust += 0.01;
            }
        }

        public void RotateLeft()
        {
            if (FuelTank.RemainingFuel > 0)
            {
                RotationSpeed -= AngleUtils.ConvertToRadians(0.2);
                FuelTank.RemainingFuel--;
            }
        }

        public void DecreaseThrust()
        {
            if (Thrust > 0.5)
            {
                Thrust -= 0.5;
            }
            else
            {
                Thrust = 0;
            }
        }

        public void RotateRight()
        {
            if (FuelTank.RemainingFuel > 0)
            {
                RotationSpeed += AngleUtils.ConvertToRadians(0.2);
                FuelTank.RemainingFuel--;
            }
        }

        public void PressedR()
        {
            Thrust = 0;
            Velocity = new Vector();
            FuelTank.RemainingFuel = FuelTank.Size;
            ViewDirectionAngle = 0;
            RotationSpeed = 0;
            Position = new Vector(200, 200);
        }

        public void PressedT()
        {
            RotationSpeed = 0;
            Thrust = 0;
        }
    }
}