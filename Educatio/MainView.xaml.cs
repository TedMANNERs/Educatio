using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Educatio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MainViewModel.IsRunning = false;
            MainViewModel mainViewModel = (MainViewModel)DataContext;
            mainViewModel.Universe.KeyboardListener.Stop();
        }
    }
}