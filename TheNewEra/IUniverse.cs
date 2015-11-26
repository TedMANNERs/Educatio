using System.Collections.ObjectModel;
using TheNewEra.Objects;
using TheNewEra.Objects.Rocket;
using TheNewEra.Physics;

namespace TheNewEra
{
    public interface IUniverse
    {
        ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        Rocket Rocket { get; set; }
        KeyboardListener.KeyboardListener KeyboardListener { get; }
        IPhysicsEngine PhysicsEngine { get; set; }

        void Stop();
    }
}