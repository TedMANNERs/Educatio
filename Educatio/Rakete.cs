using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Educatio.Annotations;

namespace Educatio
{
    public class Rakete : INotifyPropertyChanged
    {
        private int _beschleunigung;
        private int _flugRichtungsWinkel;
        private int _geschwindigkeit;
        private int _blickRichtungsWinkel;
        private int _treibstoffMenge;
        private int _x;
        private int _y;
        private int _bildNummer = 1;
        private string _bild;

        public Rakete(int x, int y)
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

        public int Beschleunigung
        {
            get { return _beschleunigung; }
            set
            {
                _beschleunigung = value < 0 ? 0 : value;
                OnPropertyChanged();
            }
        }

        public int Geschwindigkeit
        {
            get { return _geschwindigkeit; }
            set
            {
                _geschwindigkeit = value;
                OnPropertyChanged();
            }
        }

        public int BlickRichtungsWinkel
        {
            get { return _blickRichtungsWinkel; }
            set
            {
                if (TreibstoffMenge <= 0)
                    return;
                _blickRichtungsWinkel = BegrenzeWinkel(value);
                OnPropertyChanged();
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
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
                if (TreibstoffMenge < 0)
                {
                    Bild = "Resources/Images/rakete3.png";
                    GeheVorwärts(10);
                }
                else
                {
                    if (_bildNummer > 2)
                    {
                        _bildNummer = 1;
                    }
                    Bild = "Resources/Images/rakete" + _bildNummer +".png";
                    _bildNummer++;
                }

                Thread.Sleep(20);
            }
        }

        private static int BegrenzeWinkel(int winkel)
        {
            if (winkel < 0)
                return 360 + winkel;
            if (winkel > 360)
                return winkel - 360;
            return winkel;
        }

        public void GeheVorwärts(int strecke)
        {
            double cos = Math.Cos(BlickRichtungsWinkel * (Math.PI / 180));
            X += (int)(strecke * cos);
            double sin = Math.Sin(BlickRichtungsWinkel * (Math.PI / 180));
            Y -= (int)(strecke * sin);
        }

        public void GeheVorwärtsRealistisch()
        {
            double cos = Math.Cos(FlugRichtungsWinkel * (Math.PI / 180));
            X += (int)(Geschwindigkeit * cos);
            double sin = Math.Sin(FlugRichtungsWinkel * (Math.PI / 180));
            Y -= (int)(Geschwindigkeit * sin);
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