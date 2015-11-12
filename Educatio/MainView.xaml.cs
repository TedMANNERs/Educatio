﻿using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Educatio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainView()
        {
            InitializeComponent();
            _mainViewModel = (MainViewModel)DataContext;
            Thread keyboardThread = new Thread(KeyboardListener);
            keyboardThread.SetApartmentState(ApartmentState.STA);
            keyboardThread.Start();
        }

        private void KeyboardListener()
        {
            
            while (MainViewModel.IsRunning)
            {
                if (Keyboard.IsKeyDown(Key.W))
                    Dispatcher.Invoke(_mainViewModel.Spielfeld.Wgedrückt);

                if (Keyboard.IsKeyDown(Key.S))
                    Dispatcher.Invoke(_mainViewModel.Spielfeld.Sgedrückt);

                if (Keyboard.IsKeyDown(Key.A))
                    Dispatcher.Invoke(_mainViewModel.Spielfeld.Agedrückt);

                if (Keyboard.IsKeyDown(Key.D))
                    Dispatcher.Invoke(_mainViewModel.Spielfeld.Dgedrückt);

                if (Keyboard.IsKeyDown(Key.Space))
                    Dispatcher.Invoke(_mainViewModel.Spielfeld.Leertastegedrückt);

                Thread.Sleep(20);
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MainViewModel.IsRunning = false;
        }
    }
}