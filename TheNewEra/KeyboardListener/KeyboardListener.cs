using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace TheNewEra.KeyboardListener
{
    public class KeyboardListener : IKeyboardListener
    {
        private readonly Thread _keyboardThread;
        private bool _isRunning;

        public KeyboardListener()
        {
            _keyboardThread = new Thread(Listen);
            _keyboardThread.SetApartmentState(ApartmentState.STA);
            Subscribers = new List<Input>();
        }

        public IList<Input> Subscribers { get; }

        public void Start()
        {
            _isRunning = true;
            _keyboardThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void Listen()
        {
            while (_isRunning)
            {
                foreach (Input subscriber in Subscribers)
                {
                    if (Keyboard.IsKeyDown(subscriber.Key))
                    {
                        subscriber.IsDown = true;
                        subscriber.Action.BeginInvoke(null, null);
                    }
                    if (Keyboard.IsKeyUp(subscriber.Key) && subscriber.IsDown)
                    {
                        subscriber.IsDown = false;
                    }
                }

                Thread.Sleep(20);
            }
        }
    }
}