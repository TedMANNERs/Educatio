using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Educatio.Annotations;

namespace Educatio
{
    public class Meteoroid : INotifyPropertyChanged, IMoveableObject
    {
        private double _acceleration;
        private Vector _accelerationMovement;
        private int _flightDirectionAngle;
        private Vector _spaceMovement;
        private string _sprite;
        private int _viewDirectionAngle;
        private double _x;
        private double _y;

        public Meteoroid(double x, double y, int rotateAcceleration, Vector movement)
        {
            _x = x;
            _y = y;
            RotateAcceleration = rotateAcceleration;
            SpaceMovement = movement;
            Sprite = "Resources/Images/asteroid.png";
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

        public int FlightDirectionAngle
        {
            get { return _flightDirectionAngle; }
            set
            {
                _flightDirectionAngle = AngleUtils.LimitAngle(value);
                OnPropertyChanged();
            }
        }

        public Vector AccelerationMovement
        {
            get { return _accelerationMovement; }
            set
            {
                _accelerationMovement = value;
                OnPropertyChanged();
            }
        }

        public double Acceleration
        {
            get { return _acceleration; }
            set
            {
                _acceleration = value;
                OnPropertyChanged();
            }
        }

        public int ViewDirectionAngle
        {
            get { return _viewDirectionAngle; }
            set
            {
                _viewDirectionAngle = AngleUtils.LimitAngle(value);
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

        public int RotateAcceleration { get; set; }
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