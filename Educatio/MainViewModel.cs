namespace Educatio
{
    public class MainViewModel
    {
        public static bool IsRunning = true;

        public MainViewModel()
        {
            Spielfeld = new Spielfeld();
        }

        public Spielfeld Spielfeld { get; set; }
    }
}