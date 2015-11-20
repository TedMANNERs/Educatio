using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheNewEra.Properties;

namespace TheNewEra
{
    public class Rocket : INotifyPropertyChanged, IMoveableObject
    {
        private double _flightDirectionAngle;
        private int _imageId = 1;
        private double _positionAngle;
        private int _remainingFuel;
        private Vector _spaceMovement;
        private string _sprite;
        private double _thrust;
        private Vector _thrustMovement;
        private double _viewDirectionAngle;
        private double _x;
        private double _y;

        public Rocket(double x, double y, int height, int width)
        {
            Height = height;
            Width = width;
            _x = x;
            _y = y;
            CenterX = Width / 2.0;
            CenterY = Height / 2.0;
            Sprite = "Resources/Images/rocket1.png";
            CollisionRadius = Height > Width ? Height / 3 : Width / 3;
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

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public double CenterY { get; private set; }
        public double CenterX { get; private set; }

        public string Sprite
        {
            get { return _sprite; }
            set
            {
                _sprite = value;
                OnPropertyChanged();
            }
        }

        public double RotationSpeed { get; set; }

        public double Thrust
        {
            get { return _thrust; }
            set
            {
                _thrust = value;
                OnPropertyChanged();
            }
        }

        public Vector ThrustMovement
        {
            get { return _thrustMovement; }
            set
            {
                _thrustMovement = value;
                OnPropertyChanged();
            }
        }

        public Vector SpaceMovement
        {
            get { return _spaceMovement; }
            set
            {
                _spaceMovement = value;
                OnPropertyChanged();
            }
        }

        public double FlightDirectionAngle
        {
            get { return _flightDirectionAngle; }
            set
            {
                _flightDirectionAngle = AngleUtils.LimitAngle(value);
                OnPropertyChanged();
            }
        }

        public double ViewDirectionAngle
        {
            get { return _viewDirectionAngle; }
            set
            {
                _viewDirectionAngle = AngleUtils.LimitAngle(value);
                OnPropertyChanged();
            }
        }

        public double CollisionRadius { get; set; }

        public void Update()
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void IncreaseThrust()
        {
            if (RemainingFuel > 0 && Thrust < 20)
            {
                Thrust += 0.1;
            }
        }

        public void RotateLeft()
        {
            if (RemainingFuel > 0)
            {
                RotationSpeed -= 0.2;
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
                RotationSpeed += 0.2;
                RemainingFuel--;
            }
        }

        public void PressedR()
        {
            Thrust = 0;
            SpaceMovement = new Vector();
            RemainingFuel = FuelTankSize;
            ViewDirectionAngle = 0;
            RotationSpeed = 0;
            X = 200;
            Y = 200;
        }

        public void PressedT()
        {
            RotationSpeed = 0;
            Thrust = 0;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}