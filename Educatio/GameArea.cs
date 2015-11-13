using System.Windows.Input;

namespace Educatio
{
    public class GameArea
    {
        public GameArea(KeyboardListener keyboardListener)
        {
            KeyboardListener = keyboardListener;
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

        public Rocket Rocket { get; set; }
        public KeyboardListener KeyboardListener { get; private set; }
    }
}