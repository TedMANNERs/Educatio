using System.Collections.ObjectModel;

namespace TheNewEra
{
    public interface IUniverse
    {
        ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        Rocket Rocket { get; set; }
        KeyboardListener KeyboardListener { get; }
        IPhysicsEngine PhysicsEngine { get; set; }

        void Stop();
    }
}