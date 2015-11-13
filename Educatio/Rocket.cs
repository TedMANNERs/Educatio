using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Educatio.Annotations;

namespace Educatio
{
    public class Rocket : INotifyPropertyChanged
    {
        private double _acceleration;
        private Vector _accelerationMovement;
        private int _flightDirectionAngle;
        private int _imageId = 1;
        private int _positionAngle;
        private int _remainingFuel;
        private Vector _spaceMovement;
        private string _sprite;
        private int _viewDirectionAngle;
        private double _x;
        private double _y;

        public Rocket(double x, double y)
        {
            _x = x;
            _y = y;
            Task task = new Task(Loop);
            task.Start();
            Sprite = "Resources/Images/rocket1.png";
        }

        public static int FuelTankSize { get; set; }

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

        public int RemainingFuel
        {
            get { return _remainingFuel; }
            set
            {
                _remainingFuel = value;
                OnPropertyChanged();
            }
        }

        public int RotateAcceleration { get; set; }

        public int PositionAngle
        {
            get { return _positionAngle; }
            set
            {
                _positionAngle = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Loop()
        {
            while (MainViewModel.IsRunning)
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
                }

                ViewDirectionAngle += RotateAcceleration;
                AccelerationMovement = VectorUtils.GetVector(Acceleration, ViewDirectionAngle);

                Vector xAxis = VectorUtils.GetVector(1, 0);
                PositionAngle = (int)Vector.AngleBetween(new Vector(X, Y), xAxis);
                FlightDirectionAngle = 360 - (int)Vector.AngleBetween(SpaceMovement, xAxis);

                MoveInDirection(SpaceMovement.Length, FlightDirectionAngle);
                MoveInDirection(AccelerationMovement.Length, ViewDirectionAngle);

                SpaceMovement = Vector.Add(SpaceMovement, AccelerationMovement);

                Thread.Sleep(30);
            }
        }

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
                RotateAcceleration--;
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
                RotateAcceleration++;
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

        public void MoveInDirection(double length, int angle)
        {
            double cos = Math.Cos(angle * (Math.PI / 180));
            X += length * cos;
            double sin = Math.Sin(angle * (Math.PI / 180));
            Y -= length * sin;
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