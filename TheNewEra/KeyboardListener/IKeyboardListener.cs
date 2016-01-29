using System.Collections.Generic;

namespace TheNewEra.KeyboardListener
{
    public interface IKeyboardListener
    {
        IList<Input> Subscribers { get; }

        void Start();

        void Stop();
    }
}