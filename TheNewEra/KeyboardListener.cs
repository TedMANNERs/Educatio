using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace TheNewEra
{
    public class KeyboardListener
    {
        private readonly Thread _keyboardThread;
        private bool _isRunning;

        public KeyboardListener()
        {
            _keyboardThread = new Thread(Listen);
            _keyboardThread.SetApartmentState(ApartmentState.STA);
            Subscribers = new Dictionary<Key, Action>();
        }

        public Dictionary<Key, Action> Subscribers { get; private set; }

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
                foreach (KeyValuePair<Key, Action> subscriber in Subscribers)
                {
                    if (Keyboard.IsKeyDown(subscriber.Key))
                    {
                        subscriber.Value.BeginInvoke(null, null);
                    }
                }

                Thread.Sleep(20);
            }
        }
    }
}