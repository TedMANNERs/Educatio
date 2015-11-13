namespace Educatio
{
    public class MainViewModel
    {
        public static bool IsRunning = true;

        public MainViewModel()
        {
            GameArea = new GameArea(new KeyboardListener());
        }

        public GameArea GameArea { get; set; }
    }
}