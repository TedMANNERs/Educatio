using System.Collections.ObjectModel;

namespace TheNewEra
{
    public interface IPhysicsEngine
    {
        void ApplyForces(ObservableCollection<IMoveableObject> moveableObjects);

        void HandleCollisions(ObservableCollection<IMoveableObject> moveableObjects);
    }
}