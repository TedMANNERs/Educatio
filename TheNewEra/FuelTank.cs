using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheNewEra
{
    public class FuelTank : IFuelTank, INotifyPropertyChanged
    {
        private double _remainingFuel;

        public FuelTank(int size)
        {
            Size = size;
            RemainingFuel = size;
        }

        public FuelTank(int size, double remainingFuel)
        {
            Size = size;
            RemainingFuel = remainingFuel;
        }

        public int Size { get; private set; }

        public double RemainingFuel
        {
            get { return _remainingFuel; }
            set
            {
                _remainingFuel = value;
                OnPropertyChanged();
                OnPropertyChanged("RemainingFuelScaled");
            }
        }

        public double RemainingFuelScaled
        {
            get { return RemainingFuel / Size * ScaleFactor; }
        }

        public int ScaleFactor
        {
            get { return 100; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}