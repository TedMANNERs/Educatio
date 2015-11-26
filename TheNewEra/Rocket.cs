using System.Windows;

namespace TheNewEra
{
    public class Rocket : MoveableObjectBase
    {
        private int _imageId = 1;
        private double _positionAngle;
        private int _remainingFuel;

        public Rocket(Vector position, int height, int width)
        {
            Height = height;
            Width = width;
            Position = position;
            Sprite = "Resources/Images/rocket1.png";
            Init();
        }

        public static int FuelTankSize { get; set; }

        public int RemainingFuel
        {
            get { return _remainingFuel; }
            set
            {
                _remainingFuel = value;
                OnPropertyChanged();
            }
        }

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
            if (RemainingFuel <= 0 || ThrustMovement.Length <= 0)
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
                RemainingFuel--;
            }
        }

        public void IncreaseThrust()
        {
            if (RemainingFuel > 0 && Thrust < 20)
            {
                Thrust += 0.01;
            }
        }

        public void RotateLeft()
        {
            if (RemainingFuel > 0)
            {
                RotationSpeed -= AngleUtils.ConvertToRadians(0.2);
                RemainingFuel--;
            }
        }

        public void DecreaseThrust()
        {
            if (Thrust > 0)
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
            if (RemainingFuel > 0)
            {
                RotationSpeed += AngleUtils.ConvertToRadians(0.2);
                RemainingFuel--;
            }
        }

        public void PressedR()
        {
            Thrust = 0;
            Velocity = new Vector();
            RemainingFuel = FuelTankSize;
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