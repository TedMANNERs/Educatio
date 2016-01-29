using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TheNewEra.KeyboardListener
{
    public class Input : INotifyPropertyChanged
    {
        private bool _isDown;

        public Input(Key key, Action action)
        {
            Key = key;
            Action = action;
        }

        public bool IsDown  
        {
            get { return _isDown; }
            set
            {
                _isDown = value; 
                OnPropertyChanged();
            }
        }

        public Key Key { get; set; }
        public Action Action { get; set; }  
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}