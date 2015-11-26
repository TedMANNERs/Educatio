using System.Collections.ObjectModel;
using TheNewEra.Objects;

namespace TheNewEra.Physics
{
    public interface IPhysicsEngine
    {
        void ApplyForces(ObservableCollection<IMoveableObject> moveableObjects);

        void HandleCollisions(ObservableCollection<IMoveableObject> moveableObjects);
    }
}