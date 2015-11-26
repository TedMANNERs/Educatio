using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TheNewEra
{
    public class Universe : IUniverse
    {
        private bool _isRunning = true;

        public Universe(KeyboardListener keyboardListener)
        {
            KeyboardListener = keyboardListener;

            MoveableObjects = new ObservableCollection<IMoveableObject>();
            MoveableObjects.Add(new Meteoroid(new Vector(800, 200), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 500 });
            MoveableObjects.Add(new Meteoroid(new Vector(600, 210), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 500 });
            MoveableObjects.Add(new Meteoroid(new Vector(500, 310), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 800 });
            MoveableObjects.Add(new Meteoroid(new Vector(500, 250), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 600 });
            MoveableObjects.Add(new Meteoroid(new Vector(400, 160), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 800 });
            MoveableObjects.Add(new Meteoroid(new Vector(700, 150), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 400 });
            MoveableObjects.Add(new Rocket(new Vector(200, 200), 50, 89) { Mass = 200 });

            Rocket = MoveableObjects.OfType<Rocket>().Single();
            Rocket.FuelTankSize = 5000;
            Rocket.RemainingFuel = 5000;

            KeyboardListener.Subscribers.Add(Key.W, Rocket.IncreaseThrust);
            KeyboardListener.Subscribers.Add(Key.A, Rocket.RotateLeft);
            KeyboardListener.Subscribers.Add(Key.S, Rocket.DecreaseThrust);
            KeyboardListener.Subscribers.Add(Key.D, Rocket.RotateRight);
            KeyboardListener.Subscribers.Add(Key.R, Rocket.PressedR);
            KeyboardListener.Subscribers.Add(Key.T, Rocket.PressedT);
            KeyboardListener.Start();

            Task task = new Task(Loop);
            task.Start();
        }

        public Universe()
        {
            Rocket = new Rocket(new Vector(200, 200), 50, 89);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;
        }

        public ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        public Rocket Rocket { get; set; }
        public KeyboardListener KeyboardListener { get; private set; }

        public void Stop()
        {
            _isRunning = false;
            KeyboardListener.Stop();
        }

        private void Loop()
        {
            while (_isRunning)
            {
                ApplyForces();
                HandleCollisions();
                UpdateObjects();

                Thread.Sleep(30);
            }
        }

        private void ApplyForces()
        {
            foreach (IMoveableObject moveableObject in MoveableObjects)
            {
                Vector xAxis = VectorUtils.GetVector(1, 0);
                Vector navigatorCenter = new Vector(50, 50);
                Vector rocketPosition = Vector.Subtract(new Vector(Rocket.Position.X, Rocket.Position.Y), navigatorCenter);
                Vector navigatorXAxis = Vector.Subtract(new Vector(100, 50), navigatorCenter);
                Rocket.PositionAngle = AngleUtils.ConvertToRadians(Vector.AngleBetween(rocketPosition, navigatorXAxis));

                moveableObject.ViewDirectionAngle += moveableObject.RotationSpeed;
                moveableObject.ThrustMovement = VectorUtils.GetVector(moveableObject.Thrust, moveableObject.ViewDirectionAngle);

                moveableObject.FlightDirectionAngle = 2 * Math.PI - AngleUtils.ConvertToRadians(Vector.AngleBetween(moveableObject.Velocity, xAxis));

                moveableObject.Velocity = Vector.Add(moveableObject.Velocity, moveableObject.ThrustMovement);
            }
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < MoveableObjects.Count; i++)
            {
                IMoveableObject objectA = MoveableObjects[i];
                IEnumerable<IMoveableObject> remainingObjects = MoveableObjects.Skip(i + 1);
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

        private Vector GetResultingVelocityFromCollision(IMoveableObject objectA, IMoveableObject objectB)
        {
            double mass = (2 * objectB.Mass) / (objectA.Mass + objectB.Mass);
            Vector distance = objectA.Position - objectB.Position;
            double magnitude = Math.Pow(distance.Length, 2);
            double dotProduct = Vector.Multiply(objectA.Velocity - objectB.Velocity,distance);
            Vector productTerm2 = Vector.Multiply(mass * dotProduct / magnitude, distance);
            Vector result = objectA.Velocity - productTerm2;
            return result;
        }

        private void UpdateObjects()
        {
            foreach (IMoveableObject moveableObject in MoveableObjects)
            {
                moveableObject.Update();
            }
        }
    }
}