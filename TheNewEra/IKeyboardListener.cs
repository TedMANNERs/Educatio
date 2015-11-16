using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace TheNewEra
{
    public interface IKeyboardListener
    {
        Dictionary<Key, Action> Subscribers { get; }

        void Start();

        void Stop();
    }
}