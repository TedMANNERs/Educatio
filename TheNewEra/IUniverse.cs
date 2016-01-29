using System.Collections.ObjectModel;
using TheNewEra.KeyboardListener;
using TheNewEra.Objects;
using TheNewEra.Objects.Rocket;
using TheNewEra.Physics;

namespace TheNewEra
{
    public interface IUniverse
    {
        ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        Rocket Rocket { get; set; }
        IKeyboardListener KeyboardListener { get; }
        IPhysicsEngine PhysicsEngine { get; set; }

        void Stop();
    }
}