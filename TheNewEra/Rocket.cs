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
        private Vector _velocity;
        private string _sprite;
        private double _thrust;
        private Vector _thrustMovement;
        private double _viewDirectionAngle;
        private Point _position;

        public Rocket(Point position, int height, int width)
        {
            Height = height;
            Width = width;
            Position = position;
            RelativeCenter = new Point(Width / 2.0, Height / 2.0);
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

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value; 
                OnPropertyChanged();
            }
        }

        public Vector Center
        {
            get { return new Vector(Position.X + RelativeCenter.X, Position.Y + RelativeCenter.Y); }
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public Point RelativeCenter { get; private set; }

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

        public Vector Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
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
                Thrust += 0.01;
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
            Velocity = new Vector();
            RemainingFuel = FuelTankSize;
            ViewDirectionAngle = 0;
            RotationSpeed = 0;
            Position = new Point(200, 200);
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