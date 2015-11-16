using System.ComponentModel;

namespace Educatio
{
    public class MainViewModel
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