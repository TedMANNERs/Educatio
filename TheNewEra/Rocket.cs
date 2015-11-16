using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TheNewEra.Annotations;

namespace TheNewEra
{
    public class Rocket : INotifyPropertyChanged, IMoveableObject
    {
        private double _acceleration;
        private Vector _accelerationMovement;
        private double _flightDirectionAngle;
        private int _imageId = 1;
        private double _positionAngle;
        private int _remainingFuel;
        private Vector _spaceMovement;
        private string _sprite;
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

        public double RotateAcceleration { get; set; }

        public double Acceleration
        {
            get { return _acceleration; }
            set
            {
                _acceleration = value;
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

        public void Update()
        {
            if (RemainingFuel <= 0 || AccelerationMovement.Length <= 0)
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

        public void PressedW()
        {
            if (RemainingFuel > 0 && Acceleration < 20)
            {
                Acceleration += 0.1;
            }
        }

        public void PressedA()
        {
            if (RemainingFuel > 0)
            {
                RotateAcceleration -= 0.2;
                RemainingFuel--;
            }
        }

        public void PressedS()
        {
            if (Acceleration > 0)
            {
                Acceleration -= 0.5;
            }
            else
            {
                Acceleration = 0;
            }
        }

        public void PressedD()
        {
            if (RemainingFuel > 0)
            {
                RotateAcceleration += 0.2;
                RemainingFuel--;
            }
        }

        public void PressedR()
        {
            Acceleration = 0;
            SpaceMovement = new Vector();
            RemainingFuel = FuelTankSize;
            ViewDirectionAngle = 0;
            RotateAcceleration = 0;
            X = 200;
            Y = 200;
        }

        public void PressedT()
        {
            RotateAcceleration = 0;
            Acceleration = 0;
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