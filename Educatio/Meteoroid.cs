using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Educatio.Annotations;

namespace Educatio
{
    public class Meteoroid : INotifyPropertyChanged
    {
        private int _acceleration;
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
            Task task = new Task(Loop);
            task.Start();
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

        public int Acceleration
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

        private void Loop()
        {
            while (MainViewModel.IsRunning)
            {
                ViewDirectionAngle += RotateAcceleration;
                AccelerationMovement = VectorUtils.GetVector(Acceleration, ViewDirectionAngle);

                Vector xAxis = VectorUtils.GetVector(1, 0);
                FlightDirectionAngle = 360 - (int)Vector.AngleBetween(SpaceMovement, xAxis);

                MoveInDirection(SpaceMovement.Length, FlightDirectionAngle);
                MoveInDirection(AccelerationMovement.Length, ViewDirectionAngle);

                SpaceMovement = Vector.Add(SpaceMovement, AccelerationMovement);

                Thread.Sleep(30);
            }
        }

        public void MoveInDirection(double length, int angle)
        {
            double cos = Math.Cos(angle * (Math.PI / 180));
            X += length * cos;
            double sin = Math.Sin(angle * (Math.PI / 180));
            Y -= length * sin;
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