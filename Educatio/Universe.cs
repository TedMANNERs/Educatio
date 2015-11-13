using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Educatio
{
    public class Universe
    {
        public Universe(KeyboardListener keyboardListener)
        {
            KeyboardListener = keyboardListener;

            Meteoroids = new ObservableCollection<Meteoroid>();
            Meteoroids.Add(new Meteoroid(1000, 200, 1, VectorUtils.GetVector(8, 180)));
            Meteoroids.Add(new Meteoroid(800, 150, -2, VectorUtils.GetVector(7, 178)));

            Rocket = new Rocket(200, 200);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;

            KeyboardListener.Subscribers.Add(Key.W, Rocket.PressedW);
            KeyboardListener.Subscribers.Add(Key.A, Rocket.PressedA);
            KeyboardListener.Subscribers.Add(Key.S, Rocket.PressedS);
            KeyboardListener.Subscribers.Add(Key.D, Rocket.PressedD);
            KeyboardListener.Subscribers.Add(Key.R, Rocket.PressedR);
            KeyboardListener.Subscribers.Add(Key.T, Rocket.PressedT);
            KeyboardListener.Start();
        }

        public Universe()
        {
            Rocket = new Rocket(200, 200);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;
        }

        public ObservableCollection<Meteoroid> Meteoroids { get; set; }

        public Rocket Rocket { get; set; }
        public KeyboardListener KeyboardListener { get; private set; }
    }
}