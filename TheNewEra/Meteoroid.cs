using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheNewEra.Properties;

namespace TheNewEra
{
    public class Meteoroid : INotifyPropertyChanged, IMoveableObject
    {
        private double _flightDirectionAngle;
        private Vector _spaceMovement;
        private string _sprite;
        private double _thrust;
        private Vector _thrustMovement;
        private double _viewDirectionAngle;
        private double _x;
        private double _y;

        public Meteoroid(double x, double y, int rotationSpeed, Vector movement, int height, int width)
        {
            _x = x;
            _y = y;
            RotationSpeed = rotationSpeed;
            SpaceMovement = movement;
            Height = height;
            Width = width;
            CenterX = Width / 2.0;
            CenterY = Height / 2.0;
            Sprite = "Resources/Images/asteroid.png";
            CollisionRadius = Height > Width ? Height / 3 : Width / 3;
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
        public double CenterX { get; private set; }
        public double CenterY { get; private set; }

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