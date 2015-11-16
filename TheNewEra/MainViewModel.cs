using System.ComponentModel;

namespace TheNewEra
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {
            Universe = new Universe(new KeyboardListener());
        }

        public Universe Universe { get; set; }

        public void Close(object sender, CancelEventArgs e)
        {
            Universe.Stop();
        }
    }
}