using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Educatio.Annotations;

namespace Educatio
{
    public class Rakete : INotifyPropertyChanged
    {
        private Vector _beschleunigungsBewegung;
        private string _bild;
        private int _bildNummer = 1;
        private int _blickRichtungsWinkel;
        private int _flugRichtungsWinkel;
        private int _beschleunigung;
        private int _treibstoffMenge;
        private double _x;
        private double _y;
        private Vector _raumBewegung;

        public Rakete(double x, double y)
        {
            _x = x;
            _y = y;
            Task task = new Task(Loop);
            task.Start();
            Bild = "Resources/Images/rakete1.png";
        }

        public static int TankGrösse { get; set; }

        public string Bild
        {
            get { return _bild; }
            set
            {
                _bild = value;
                OnPropertyChanged();
            }
        }

        public int FlugRichtungsWinkel
        {
            get { return _flugRichtungsWinkel; }
            set
            {
                _flugRichtungsWinkel = BegrenzeWinkel(value);
                OnPropertyChanged();
            }
        }

        public Vector BeschleunigungsBewegung
        {
            get { return _beschleunigungsBewegung; }
            set
            {
                _beschleunigungsBewegung = value;
                OnPropertyChanged();
            }
        }

        public int Beschleunigung
        {
            get { return _beschleunigung; }
            set
            {
                _beschleunigung = value;
                OnPropertyChanged();
            }
        }

        public int BlickRichtungsWinkel
        {
            get { return _blickRichtungsWinkel; }
            set
            {
                _blickRichtungsWinkel = BegrenzeWinkel(value);
                OnPropertyChanged();
            }
        }

        public Vector RaumBewegung
        {
            get { return _raumBewegung; }
            set
            {
                _raumBewegung = value; 
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

        public int TreibstoffMenge
        {
            get { return _treibstoffMenge; }
            set
            {
                _treibstoffMenge = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Loop()
        {
            while (MainViewModel.IsRunning)
            {
                if (TreibstoffMenge <= 0 || BeschleunigungsBewegung.Length <= 0)
                {
                    Bild = "Resources/Images/rakete3.png";
                }
                else
                {
                    if (_bildNummer > 2)
                    {
                        _bildNummer = 1;
                    }
                    Bild = "Resources/Images/rakete" + _bildNummer + ".png";
                    _bildNummer++;
                }

                BeschleunigungsBewegung = VektorAusLängeUndWinkel(Beschleunigung, BlickRichtungsWinkel);

                Vector xAchse = VektorAusLängeUndWinkel(1, 0);
                FlugRichtungsWinkel = (int)Vector.AngleBetween(RaumBewegung, xAchse);
                BewegeInRichtung(RaumBewegung.Length, -FlugRichtungsWinkel);
                BewegeInRichtung(BeschleunigungsBewegung.Length, BlickRichtungsWinkel);
                RaumBewegung = Vector.Add(RaumBewegung, BeschleunigungsBewegung);

                Thread.Sleep(50);
            }
        }

        private Vector VektorAusLängeUndWinkel(double länge, int winkel)
        {
            double cos = Math.Cos(winkel * (Math.PI / 180));
            double sin = Math.Sin(winkel * (Math.PI / 180));
            double x = (länge / 10.0) * cos;
            double y = (länge / 10.0) * sin;
            return new Vector(x, y);
        }

        public void Dgedrückt()
        {
            if (TreibstoffMenge > 0)
            {
                BlickRichtungsWinkel += 10;
                TreibstoffMenge--;
            }
        }

        public void Agedrückt()
        {
            if (TreibstoffMenge > 0)
            {
                BlickRichtungsWinkel -= 10;
                TreibstoffMenge--;
            }
        }

        public void Sgedrückt()
        {
            if (Beschleunigung > 0)
            {
                Beschleunigung--;
            }
        }

        public void Wgedrückt()
        {
            if (TreibstoffMenge > 0 && Beschleunigung < 20)
            {
                Beschleunigung++;
            }
        }

        public void Rgedrückt()
        {
            Beschleunigung = 0;
            RaumBewegung = new Vector();
            TreibstoffMenge = TankGrösse;
            BlickRichtungsWinkel = 0;
            X = 200;
            Y = 200;
        }

        private static int BegrenzeWinkel(int winkel)
        {
            if (winkel < 0)
                return 360 + winkel;
            if (winkel > 360)
                return winkel - 360;
            return winkel;
        }

        public void BewegeInRichtung(double strecke, int winkel)
        {
            double cos = Math.Cos(winkel * (Math.PI / 180));
            X += strecke * cos;
            double sin = Math.Sin(winkel * (Math.PI / 180));
            Y -= strecke * sin;
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