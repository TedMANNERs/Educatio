namespace TheNewEra
{
    public class MainViewModelDesignData
    {
        public static bool IsRunning = true;

        public MainViewModelDesignData()
        {
            Universe = new Universe();
        }

        public Universe Universe { get; set; }
    }
}