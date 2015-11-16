using System.Windows;

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
            MainViewModel mainViewModel = (MainViewModel)DataContext;
            Closing += mainViewModel.Close;
        }
    }
}