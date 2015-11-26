using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace TheNewEra
{
    public abstract class MoveableObjectBase : IMoveableObject, INotifyPropertyChanged
    {
        private double _flightDirectionAngle;
        private Vector _position;
        private string _sprite;
        private double _thrust;
        private Vector _thrustMovement;
        private Vector _velocity;
        private double _viewDirectionAngle;
        private readonly Matrix _yInversionMatrix;

        protected MoveableObjectBase()
        {
            _yInversionMatrix = new Matrix(1, 0, 0, -1, 0, 0);
        }

        public Vector TranslatedPosition
        {
            get { return new Vector(Position.X - RelativeCenter.X, Position.Y - RelativeCenter.Y); }
        }

        public Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
                OnPropertyChanged("TranslatedPosition");
            }
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
        public double Mass { get; set; }

        public virtual void Update()
        {
            Position += Vector.Multiply(Velocity, _yInversionMatrix);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Init()
        {
            RelativeCenter = new Point(Width / 2.0, Height / 2.0);
            CollisionRadius = Height > Width ? Height / 3 : Width / 3;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}