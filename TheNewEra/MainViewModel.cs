using System.ComponentModel;
using TheNewEra.Physics;

namespace TheNewEra
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel()
        {
            Universe = new Universe(new KeyboardListener.KeyboardListener(), new PhysicsEngine());
        }

        public Universe Universe { get; set; }

        public void Close(object sender, CancelEventArgs e)
        {
            Universe.Stop();
        }
    }
}