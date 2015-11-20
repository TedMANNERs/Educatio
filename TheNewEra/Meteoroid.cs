using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheNewEra.Properties;

namespace TheNewEra
{
    public class Meteoroid : INotifyPropertyChanged, IMoveableObject
    {
        private double _flightDirectionAngle;
        private Vector _velocity;
        private string _sprite;
        private double _thrust;
        private Vector _thrustMovement;
        private double _viewDirectionAngle;
        private Point _position;

        public Meteoroid(Point position, int rotationSpeed, Vector movement, int height, int width)
        {
            Position = position;
            RotationSpeed = rotationSpeed;
            Velocity = movement;
            Height = height;
            Width = width;
            RelativeCenter = new Point(Width / 2.0, Height / 2.0);
            Sprite = "Resources/Images/asteroid.png";
            CollisionRadius = Height > Width ? Height / 3 : Width / 3;
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

        public int Height { get; set; }
        public int Width { get; set; }
        public Point RelativeCenter { get; private set; }

        public Vector Center
        {
            get { return new Vector(Position.X + RelativeCenter.X, Position.Y + RelativeCenter.Y); }
        }

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
        public double Mass { get; set; }

        public void Update()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}