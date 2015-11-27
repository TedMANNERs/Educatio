using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TheNewEra.Objects;
using TheNewEra.Util;

namespace TheNewEra.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        //In reality G would be approximately 0.000000000066742;
        public const double G = 0.00000000066742;

        public void ApplyForces(ObservableCollection<IMoveableObject> moveableObjects)
        {
            foreach (IMoveableObject moveableObject in moveableObjects)
            {
                moveableObject.ViewDirectionAngle += moveableObject.RotationSpeed;
                moveableObject.ThrustMovement = VectorUtils.GetVector(moveableObject.Thrust, moveableObject.ViewDirectionAngle);

                Vector xAxis = VectorUtils.GetVector(1, 0);
                moveableObject.FlightDirectionAngle = 2 * Math.PI - AngleUtils.ConvertToRadians(Vector.AngleBetween(moveableObject.Velocity, xAxis));

                moveableObject.Velocity += moveableObject.ThrustMovement;

                if (moveableObject is Planet)
                    continue;
                Vector gravitationalPull = GetResultingVelocityFromGravity(moveableObject, moveableObjects);
                moveableObject.Velocity += gravitationalPull;
            }
        }

        public void HandleCollisions(ObservableCollection<IMoveableObject> moveableObjects)
        {
            for (int i = 0; i < moveableObjects.Count; i++)
            {
                IMoveableObject objectA = moveableObjects[i];
                IEnumerable<IMoveableObject> remainingObjects = moveableObjects.Skip(i + 1);
                foreach (IMoveableObject objectB in remainingObjects)
                {
                    double distance = VectorUtils.GetDistance(objectA, objectB);

                    if (distance < objectA.CollisionRadius + objectB.CollisionRadius)
                    {
                        double smallerRadius = Math.Min(objectA.CollisionRadius, objectB.CollisionRadius);
                        double largerRadius = Math.Max(objectA.CollisionRadius, objectB.CollisionRadius);
                        double intersection = smallerRadius - (distance - largerRadius);
                        double angle = AngleUtils.GetAngle(objectA, objectB);
                        Vector offset = VectorUtils.GetVector(intersection / 2.0, angle);
                        objectA.Position += offset;
                        objectB.Position -= offset;

                        Vector resultingVelocityA = GetResultingVelocityFromCollision(objectA, objectB);
                        Vector resultingVelocityB = GetResultingVelocityFromCollision(objectB, objectA);
                        objectA.Velocity = resultingVelocityA;
                        objectB.Velocity = resultingVelocityB;
                    }
                }
            }
        }

        private Vector GetResultingVelocityFromGravity(IMoveableObject objectA, IEnumerable<IMoveableObject> moveableObjects)
        {
            Vector gravity = new Vector();
            foreach (IMoveableObject objectB in moveableObjects.Where(objectB => objectB != objectA))
            {
                double distance = VectorUtils.GetDistance(objectA, objectB);
                double angle = (2 * Math.PI) - AngleUtils.ConvertToRadians(Vector.AngleBetween(new Vector(1, 0), objectB.Position - objectA.Position));
                double magnitude = (G * objectA.Mass * objectB.Mass) / (distance * distance);
                gravity += VectorUtils.GetVector(magnitude, angle);
            }
            return gravity;
        }

        private Vector GetResultingVelocityFromCollision(IMoveableObject objectA, IMoveableObject objectB)
        {
            double mass = (2 * objectB.Mass) / (objectA.Mass + objectB.Mass);
            Vector distance = objectA.Position - objectB.Position;
            double magnitude = Math.Pow(distance.Length, 2);
            double dotProduct = Vector.Multiply(objectA.Velocity - objectB.Velocity, distance);
            Vector productTerm2 = Vector.Multiply(mass * dotProduct / magnitude, distance);
            Vector result = objectA.Velocity - productTerm2;
            return result;
        }
    }
}