namespace Educatio
{
    public class MainViewModel
    {
        public static bool IsRunning = true;

        public MainViewModel()
        {
            Universe = new Universe(new KeyboardListener());
        }

        public Universe Universe { get; set; }
    }
}